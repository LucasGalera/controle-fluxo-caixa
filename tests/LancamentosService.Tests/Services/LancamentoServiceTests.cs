using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LancamentosService.Services;
using LancamentosService.Models;
using LancamentosService.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using StackExchange.Redis;

namespace LancamentosService.Tests.Services
{
    public class LancamentoServiceTests
    {
        private readonly Mock<LancamentosDbContext> _mockContext;
        private readonly Mock<IConnectionMultiplexer> _mockRedis;
        private readonly LancamentoService _service;

        public LancamentoServiceTests()
        {
            _mockContext = new Mock<LancamentosDbContext>();
            _mockRedis = new Mock<IConnectionMultiplexer>();
            _service = new LancamentoService(_mockContext.Object, _mockRedis.Object);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllLancamentos()
        {
            // Arrange
            var lancamentos = new List<Lancamento> { new Lancamento() };
            var mockSet = new Mock<DbSet<Lancamento>>();
            
            _mockContext.Setup(c => c.Lancamentos)
                       .Returns(mockSet.Object);
            // Setup async enumerable

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsLancamento_WhenExists()
        {
            // Arrange
            var id = 1;
            var lancamento = new Lancamento { Id = id };
            var mockSet = new Mock<DbSet<Lancamento>>();

            _mockContext.Setup(c => c.Lancamentos)
                       .Returns(mockSet.Object);
            mockSet.Setup(s => s.FindAsync(id))
                  .ReturnsAsync(lancamento);

            // Act
            var result = await _service.GetByIdAsync(id);

            // Assert
            Assert.Equal(lancamento, result);
        }

        [Fact]
        public async Task CreateAsync_ReturnsCreatedLancamento()
        {
            // Arrange
            var lancamento = new Lancamento();
            var mockSet = new Mock<DbSet<Lancamento>>();

            _mockContext.Setup(c => c.Lancamentos)
                       .Returns(mockSet.Object);

            // Act
            var result = await _service.CreateAsync(lancamento);

            // Assert
            Assert.NotNull(result);
            mockSet.Verify(m => m.AddAsync(It.IsAny<Lancamento>(), It.IsAny<CancellationToken>()), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ReturnsUpdatedLancamento_WhenExists()
        {
            // Arrange
            var lancamento = new Lancamento { Id = 1 };
            var mockSet = new Mock<DbSet<Lancamento>>();

            _mockContext.Setup(c => c.Lancamentos)
                       .Returns(mockSet.Object);
            mockSet.Setup(s => s.FindAsync(lancamento.Id))
                  .ReturnsAsync(lancamento);

            // Act
            var result = await _service.UpdateAsync(lancamento);

            // Assert
            Assert.NotNull(result);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrue_WhenSuccessfulDelete()
        {
            // Arrange
            var id = 1;
            var lancamento = new Lancamento { Id = id };
            var mockSet = new Mock<DbSet<Lancamento>>();

            _mockContext.Setup(c => c.Lancamentos)
                       .Returns(mockSet.Object);
            mockSet.Setup(s => s.FindAsync(id))
                  .ReturnsAsync(lancamento);

            // Act
            var result = await _service.DeleteAsync(id);

            // Assert
            Assert.True(result);
            mockSet.Verify(m => m.Remove(lancamento), Times.Once);
            _mockContext.Verify(m => m.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}