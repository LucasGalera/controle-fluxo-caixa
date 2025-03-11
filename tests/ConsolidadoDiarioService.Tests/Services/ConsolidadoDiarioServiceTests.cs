using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ConsolidadoDiarioService.Services;
using ConsolidadoDiarioService.Models;
using ConsolidadoDiarioService.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using StackExchange.Redis;
using RabbitMQ.Client;

namespace ConsolidadoDiarioService.Tests.Services
{
    public class ConsolidadoDiarioServiceTests
    {
        private readonly Mock<ConsolidadoDiarioDbContext> _mockContext;
        private readonly Mock<IConnectionMultiplexer> _mockRedis;
        private readonly Mock<IConnection> _mockRabbitConnection;
        private readonly ConsolidadoDiarioService _service;

        public ConsolidadoDiarioServiceTests()
        {
            _mockContext = new Mock<ConsolidadoDiarioDbContext>();
            _mockRedis = new Mock<IConnectionMultiplexer>();
            _mockRabbitConnection = new Mock<IConnection>();

            _service = new ConsolidadoDiarioService(
                _mockContext.Object,
                _mockRedis.Object,
                _mockRabbitConnection.Object
            );
        }

        [Fact]
        public async Task GetByDateAsync_ReturnsConsolidado_WhenExists()
        {
            // Arrange
            var testDate = DateTime.Now.Date;
            var expectedConsolidado = new ConsolidadoDiario(); // Add proper initialization
            var mockSet = new Mock<DbSet<ConsolidadoDiario>>();
            
            _mockContext.Setup(c => c.ConsolidadosDiarios)
                       .Returns(mockSet.Object);
            mockSet.Setup(s => s.FindAsync(testDate))
                  .ReturnsAsync(expectedConsolidado);

            // Act
            var result = await _service.GetByDateAsync(testDate);

            // Assert
            Assert.Equal(expectedConsolidado, result);
        }

        [Fact]
        public async Task GetByPeriodAsync_ReturnsConsolidados_WhenExists()
        {
            // Arrange
            var inicio = DateTime.Now.AddDays(-7);
            var fim = DateTime.Now;
            var expectedConsolidados = new List<ConsolidadoDiario>(); // Add proper initialization
            var mockSet = new Mock<DbSet<ConsolidadoDiario>>();

            _mockContext.Setup(c => c.ConsolidadosDiarios)
                       .Returns(mockSet.Object);
            // Setup async enumerable for the query

            // Act
            var result = await _service.GetByPeriodAsync(inicio, fim);

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task ProcessarConsolidadoDiarioAsync_ReturnsNewConsolidado_WhenSuccess()
        {
            // Arrange
            var testDate = DateTime.Now.Date;
            var mockSet = new Mock<DbSet<ConsolidadoDiario>>();
            
            _mockContext.Setup(c => c.ConsolidadosDiarios)
                       .Returns(mockSet.Object);

            // Act
            var result = await _service.ProcessarConsolidadoDiarioAsync(testDate);

            // Assert
            Assert.NotNull(result);
            mockSet.Verify(m => m.AddAsync(It.IsAny<ConsolidadoDiario>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}