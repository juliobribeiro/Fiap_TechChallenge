using Contact.Core.Dto;
using Contact.Core.Events;
using DeleteContact.Application.Interfaces;
using DeleteContact.Application.Services;
using MassTransit;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;

namespace DeleteContact.Application.Tests.Interfaces
{
    public class DeleteContactApplicationTests
    {
        private readonly Mock<IBus> mockBus;
        private readonly IDeleteContactApplication _deleteContactApplication;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;

        public DeleteContactApplicationTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();

            var httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            httpClient.BaseAddress = new Uri("https://localhost/teste");

            mockBus = new Mock<IBus>();

            _deleteContactApplication = new DeleteContactApplication(mockBus.Object, httpClient);
        }
        [Fact]
        public async Task DeletarContato_ShouldReturnTrue_WhenContatoIsSuccess()
        {

            // Arrange
            int idContato = 1;

            var dto = new ContatoDto
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
                Content = new StringContent(JsonConvert.SerializeObject(dto))
            });

            mockBus.Setup(bus => bus.Publish<AddContactEvent>(It.IsAny<object>(), It.IsAny<CancellationToken>()));

            // Act
            var result = await _deleteContactApplication.DeletarContato(idContato);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task DeletarContato_ShouldReturnTrue_WhenContatoNotExits()
        {

            // Arrange
            int idContato = 1;

            var dto = new ContatoDto
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
                StatusCode = System.Net.HttpStatusCode.NoContent                
            });

            mockBus.Setup(bus => bus.Publish<AddContactEvent>(It.IsAny<object>(), It.IsAny<CancellationToken>()));

            // Act            
            await Assert.ThrowsAsync<Exception>(async () => await _deleteContactApplication.DeletarContato(idContato));


        }
    }
}