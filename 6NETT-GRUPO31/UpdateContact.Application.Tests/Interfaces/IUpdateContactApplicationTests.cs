using Castle.Core.Configuration;
using Contact.Core.Dto;
using Contact.Core.Events;
using MassTransit;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using UpdateContact.Application.Interfaces;
using UpdateContact.Application.Services;

namespace UpdateContact.Application.Tests.Interfaces
{
    public class IUpdateContactApplicationTests
    {
        private readonly Mock<IBus> mockBus;
        private readonly IUpdateContactApplication _updateContactApplication;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;

        public IUpdateContactApplicationTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            var httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            httpClient.BaseAddress = new Uri("https://localhost/teste");

            mockBus = new Mock<IBus>();

            _updateContactApplication = new UpdateContactApplication(mockBus.Object, httpClient);
        }

        [Fact]
        public async Task AtualizarContato_ShouldReturnTrue_WhenContatoIsSuccess()
        {

            // Arrange
            int idContato = 1;

            var dtonew = new CadastrarAtualizarContatoDto
            {
                Nome = "João",
                Email = "joao1@email.com",
                Telefone = "123456789",
                DDD = 11
            };

            var dtoOld = new CadastrarAtualizarContatoDto
            {
                Nome = "João",
                Email = "joao@email.com",
                Telefone = "123456789",
                DDD = 11
            };


            _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.EndsWith($"contatos/id/{idContato}")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(dtoOld))
            });



            _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.EndsWith($"contatos/email/{dtonew.Email}")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.NoContent
            });


            mockBus.Setup(bus => bus.Publish<AddContactEvent>(It.IsAny<object>(), It.IsAny<CancellationToken>()));

            // Act
            var result = await _updateContactApplication.AtualizarContrato(idContato, dtonew);

            // Assert
            Assert.True(result);
        }
    }
}