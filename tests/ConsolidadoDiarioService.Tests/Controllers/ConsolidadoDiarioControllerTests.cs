using System;
using System.Threading.Tasks;
using ConsolidadoDiarioService.Controllers;
using ConsolidadoDiarioService.Services;
using ConsolidadoDiarioService.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ConsolidadoDiarioService.Tests.Controllers
{
    public class ConsolidadoDiarioControllerTests
    {
        private readonly Mock<IConsolidadoDiarioService> _mockService;
        private readonly ConsolidadoDiarioController _controller;

        public ConsolidadoDiarioControllerTests()
        {
            _mockService = new Mock<IConsolidadoDiarioService>();
            _controller = new ConsolidadoDiarioController(_mockService.Object);
        }

        [Fact]
        public async Task GetByDate_ReturnsOkResult_WhenDataExists()
        {
            // Arrange
            var testDate = DateTime.Now.Date;
            var consolidado = new ConsolidadoDiario(); // Add proper initialization
            _mockService.Setup(s => s.GetByDateAsync(testDate))
                       .ReturnsAsync(consolidado);

            // Act
            var result = await _controller.GetByDate(testDate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(consolidado, okResult.Value);
        }

        [Fact]
        public async Task GetByDate_ReturnsNotFound_WhenDataDoesNotExist()
        {
            // Arrange
            var testDate = DateTime.Now.Date;
            _mockService.Setup(s => s.GetByDateAsync(testDate))
                       .ReturnsAsync((ConsolidadoDiario)null);

            // Act
            var result = await _controller.GetByDate(testDate);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetByPeriod_ReturnsOkResult_WhenDataExists()
        {
            // Arrange
            var inicio = DateTime.Now.AddDays(-7);
            var fim = DateTime.Now;
            var consolidados = new[] { new ConsolidadoDiario() }; // Add proper initialization
            _mockService.Setup(s => s.GetByPeriodAsync(inicio, fim))
                       .ReturnsAsync(consolidados);

            // Act
            var result = await _controller.GetByPeriod(inicio, fim);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(consolidados, okResult.Value);
        }

        [Fact]
        public async Task ProcessarConsolidado_ReturnsOkResult_WhenProcessingSucceeds()
        {
            // Arrange
            var testDate = DateTime.Now.Date;
            var consolidado = new ConsolidadoDiario(); // Add proper initialization
            _mockService.Setup(s => s.ProcessarConsolidadoDiarioAsync(testDate))
                       .ReturnsAsync(consolidado);

            // Act
            var result = await _controller.ProcessarConsolidado(testDate);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(consolidado, okResult.Value);
        }
    }
}