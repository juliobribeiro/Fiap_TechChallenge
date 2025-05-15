using AddContact.Application.Interfaces;
using AddContact.Application.Services;
using Contact.Core.Dto;
using Contact.Core.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AddContact.Application.Tests.Interfaces
{
    public class IAddContactApplicationTests
    {
        private readonly Mock<IBus> mockBus;
        private readonly AddContactApplication _addContactApplication;
        private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;

        public IAddContactApplicationTests()
        {
            _httpMessageHandlerMock = new Mock<HttpMessageHandler>();            

            var httpClient = new HttpClient(_httpMessageHandlerMock.Object);
            httpClient.BaseAddress = new Uri("https://localhost/teste");

            mockBus = new Mock<IBus>();

            _addContactApplication = new AddContactApplication(mockBus.Object, httpClient);
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

            _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.EndsWith($"contatos/email/{dto.Email}")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.NoContent
            });

            mockBus.Setup(bus => bus.Publish<AddContactEvent>(It.IsAny<object>(), It.IsAny<CancellationToken>()));

            // Act
            var result = await _addContactApplication.CadastrarContato(dto);

            // Assert
            Assert.True(result);
        }


        [Fact]
        public async Task CadastrarContato_ShouldReturnTrue_WhenContatoIsFail()
        {

            // Arrange
            var dto = new CadastrarAtualizarContatoDto
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
                ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.EndsWith($"contatos/email/{dto.Email}")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK                
            });

            mockBus.Setup(bus => bus.Publish<AddContactEvent>(It.IsAny<object>(), It.IsAny<CancellationToken>()));

            // Act
            await Assert.ThrowsAsync<Exception>(async () => await _addContactApplication.CadastrarContato(dto));
        }



    }
}
