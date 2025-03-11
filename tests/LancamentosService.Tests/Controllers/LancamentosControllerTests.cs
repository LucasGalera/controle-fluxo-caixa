using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LancamentosService.Controllers;
using LancamentosService.Services;
using LancamentosService.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace LancamentosService.Tests.Controllers
{
    public class LancamentosControllerTests
    {
        private readonly Mock<ILancamentoService> _mockService;
        private readonly LancamentosController _controller;

        public LancamentosControllerTests()
        {
            _mockService = new Mock<ILancamentoService>();
            _controller = new LancamentosController(_mockService.Object);
        }

        [Fact]
        public async Task GetAll_ReturnsOkResult_WithLancamentos()
        {
            // Arrange
            var lancamentos = new List<Lancamento> { new Lancamento() };
            _mockService.Setup(s => s.GetAllAsync())
                       .ReturnsAsync(lancamentos);

            // Act
            var result = await _controller.GetAll();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(lancamentos, okResult.Value);
        }

        [Fact]
        public async Task Get_ReturnsOkResult_WhenLancamentoExists()
        {
            // Arrange
            var id = 1;
            var lancamento = new Lancamento { Id = id };
            _mockService.Setup(s => s.GetByIdAsync(id))
                       .ReturnsAsync(lancamento);

            // Act
            var result = await _controller.Get(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(lancamento, okResult.Value);
        }

        [Fact]
        public async Task Get_ReturnsNotFound_WhenLancamentoDoesNotExist()
        {
            // Arrange
            var id = 1;
            _mockService.Setup(s => s.GetByIdAsync(id))
                       .ReturnsAsync((Lancamento)null);

            // Act
            var result = await _controller.Get(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_ReturnsCreatedAtAction_WhenValidLancamento()
        {
            // Arrange
            var lancamento = new Lancamento();
            _mockService.Setup(s => s.CreateAsync(It.IsAny<Lancamento>()))
                       .ReturnsAsync(lancamento);

            // Act
            var result = await _controller.Create(lancamento);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(LancamentosController.Get), createdAtActionResult.ActionName);
            Assert.Equal(lancamento, createdAtActionResult.Value);
        }

        [Fact]
        public async Task Update_ReturnsOkResult_WhenValidUpdate()
        {
            // Arrange
            var id = 1;
            var lancamento = new Lancamento { Id = id };
            _mockService.Setup(s => s.UpdateAsync(It.IsAny<Lancamento>()))
                       .ReturnsAsync(lancamento);

            // Act
            var result = await _controller.Update(id, lancamento);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(lancamento, okResult.Value);
        }

        [Fact]
        public async Task Delete_ReturnsNoContent_WhenSuccessfulDelete()
        {
            // Arrange
            var id = 1;
            _mockService.Setup(s => s.DeleteAsync(id))
                       .ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Delete_ReturnsNotFound_WhenLancamentoDoesNotExist()
        {
            // Arrange
            var id = 1;
            _mockService.Setup(s => s.DeleteAsync(id))
                       .ReturnsAsync(false);

            // Act
            var result = await _controller.Delete(id);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }
    }
}