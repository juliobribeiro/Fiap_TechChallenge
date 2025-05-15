using FIAP._6NETT_GRUPO31.Domain.Entities;
using FIAP._6NETT_GRUPO31.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP._6NETT_GRUPO31.Domain.Tests.Interfaces
{
    public class IContatoRepositoryTests
    {
        private readonly Mock<IContatoRepository> _mockRepository;

        public IContatoRepositoryTests()
        {
            _mockRepository = new Mock<IContatoRepository>();
        }

        [Fact]
        public async Task AdicionaContato_ShouldReturnTrue_WhenContatoIsAdded()
        {
            // Arrange
            var contato = new Contatos { IdContato = 1, Nome = "João", Email = "joao@email.com", Telefone = "123456789", DDD = 11 };
            _mockRepository.Setup(repo => repo.AdicionaContato(contato)).ReturnsAsync(true);

            // Act
            var result = await _mockRepository.Object.AdicionaContato(contato);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(repo => repo.AdicionaContato(contato), Times.Once);
        }

        [Fact]
        public async Task ConsultaContatos_ShouldReturnListOfContatos_WhenDDDIsProvided()
        {
            // Arrange
            int ddd = 11;
            var expectedContatos = new List<Contatos>
        {
            new Contatos { IdContato = 1, Nome = "João", Email = "joao@email.com", Telefone = "123456789", DDD = 11 },
            new Contatos { IdContato = 2, Nome = "Maria", Email = "maria@email.com", Telefone = "987654321", DDD = 11 }
        };
            _mockRepository.Setup(repo => repo.ConsultaContatos(ddd)).ReturnsAsync(expectedContatos);

            // Act
            var result = await _mockRepository.Object.ConsultaContatos(ddd);

            // Assert
            Assert.Equal(expectedContatos.Count, result.ToList().Count);
            _mockRepository.Verify(repo => repo.ConsultaContatos(ddd), Times.Once);
        }

        [Fact]
        public async Task AtualizaContato_ShouldUpdateContato()
        {
            // Arrange
            var contato = new Contatos { IdContato = 1, Nome = "João Atualizado", Email = "joaoatualizado@email.com", Telefone = "987654321", DDD = 11 };
            _mockRepository.Setup(repo => repo.AtualizaContato(contato)).Returns(Task.CompletedTask);

            // Act
            await _mockRepository.Object.AtualizaContato(contato);

            // Assert
            _mockRepository.Verify(repo => repo.AtualizaContato(contato), Times.Once);
        }

        [Fact]
        public async Task ConsultarContatoPorId_ShouldReturnContato_WhenIdIsValid()
        {
            // Arrange
            int contatoId = 1;
            var expectedContato = new Contatos { IdContato = contatoId, Nome = "João", Email = "joao@email.com", Telefone = "123456789", DDD = 11 };
            _mockRepository.Setup(repo => repo.ConsultarContatoPorId(contatoId)).ReturnsAsync(expectedContato);

            // Act
            var result = await _mockRepository.Object.ConsultarContatoPorId(contatoId);

            // Assert
            Assert.Equal(expectedContato.IdContato, result.IdContato);
            _mockRepository.Verify(repo => repo.ConsultarContatoPorId(contatoId), Times.Once);
        }

        [Fact]
        public async Task DeletarContato_ShouldReturnTrue_WhenContatoIsDeleted()
        {
            // Arrange
            var contato = new Contatos { IdContato = 1, Nome = "João", Email = "joao@email.com", Telefone = "123456789", DDD = 11 };
            _mockRepository.Setup(repo => repo.DeletarContato(contato)).ReturnsAsync(true);

            // Act
            var result = await _mockRepository.Object.DeletarContato(contato);

            // Assert
            Assert.True(result);
            _mockRepository.Verify(repo => repo.DeletarContato(contato), Times.Once);
        }

        [Fact]
        public async Task ConsultarContatoPorEmail_ShouldReturnContato_WhenEmailIsValid()
        {
            // Arrange
            string email = "joao@email.com";
            var expectedContato = new Contatos { IdContato = 1, Nome = "João", Email = email, Telefone = "123456789", DDD = 11 };
            _mockRepository.Setup(repo => repo.ConsultarContatoPorEmail(email)).ReturnsAsync(expectedContato);

            // Act
            var result = await _mockRepository.Object.ConsultarContatoPorEmail(email);

            // Assert
            Assert.Equal(expectedContato.Email, result.Email);
            _mockRepository.Verify(repo => repo.ConsultarContatoPorEmail(email), Times.Once);
        }
    }
}
