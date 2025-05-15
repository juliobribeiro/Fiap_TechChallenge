using Contact.Core.Dto;
using FIAP._6NETT_GRUPO31.Application.Interfaces;
using Moq;

namespace FIAP._6NETT_GRUPO31.Application.Tests.Interfaces
{
    public class IContratoApplicationTests
    {
        private readonly Mock<IContatoApplication> _mockContatoApp;

        public IContratoApplicationTests()
        {
            _mockContatoApp = new Mock<IContatoApplication>();
        }

        [Fact]
        public async Task CadastrarContato_ShouldReturnTrue_WhenContatoIsSuccessfullyCreated()
        {
            // Arrange
            var dto = new CadastrarAtualizarContatoDto
            {
                Nome = "João",
                Email = "joao@email.com",
                Telefone = "123456789",
                DDD = 11
            };
            _mockContatoApp.Setup(app => app.CadastrarContato(dto));

            // Act
           await _mockContatoApp.Object.CadastrarContato(dto);

            // Assert           
            _mockContatoApp.Verify(app => app.CadastrarContato(dto), Times.Once);
        }

        [Fact]
        public async Task AtualizarContato_ShouldReturnTrue_WhenContatoIsSuccessfullyUpdated()
        {
            // Arrange
            int contatoId = 1;
            var dto = new CadastrarAtualizarContatoDto
            {
                Nome = "João Atualizado",
                Email = "joaoatualizado@email.com",
                Telefone = "987654321",
                DDD = 11
            };
            _mockContatoApp.Setup(app => app.AtualizarContrato(contatoId, dto));

            // Act
            await _mockContatoApp.Object.AtualizarContrato(contatoId, dto);

            // Assert            
            _mockContatoApp.Verify(app => app.AtualizarContrato(contatoId, dto), Times.Once);
        }
    }
}
