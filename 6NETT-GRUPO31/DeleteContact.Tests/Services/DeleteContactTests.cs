using Contact.Core.Dto;
using Contact.Core.Events;
using DeleteContact.Application.Interfaces;
using DeleteContact.Application.Services;
using DeleteContact.Tests.Infra;
using MassTransit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DeleteContact.Tests.Services
{
    public class DeleteContactTests : MyWebApplicationFactory<Program, DbContext>
    {
        private readonly Mock<IBus> mockBus;
        public DeleteContactTests(WebApplicationFactory<Program> factory) : base(factory)
        {
            mockBus = new Mock<IBus>();
        }

        [Fact]
        public async Task DeletarContato_Success()
        {

            int idContato = 1;

            var dto = new ContatoDto
            {
                Nome = "João",
                Email = "joao@email.com",
                Telefone = "123456789",
                DDD = 11
            };

            int id = 1;

            WebApplicationFactory<Program> factory = SetNewContactApplication(id, new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(dto))

            });

            var Client = factory.CreateClient();

            var result = await Client.DeleteAsync($"/contato/{id}");

            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);

        }

        [Fact]
        public async Task DeletarContato_Erro_Id_Nao_Existe()
        {

            int idContato = 1;

            var dto = new ContatoDto
            {
                Nome = "João",
                Email = "joao@email.com",
                Telefone = "123456789",
                DDD = 11
            };

            int id = 1;

            WebApplicationFactory<Program> factory = SetNewContactApplication(id, new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.NoContent                

            });

            var Client = factory.CreateClient();


            var result = await Client.DeleteAsync($"/contato/{id}");
            var content = await result.Content.ReadAsStringAsync();

            Assert.Contains($"Contato com id:{idContato} não encontrado", content);

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);

        }

        private WebApplicationFactory<Program> SetNewContactApplication(int id, HttpResponseMessage message)
        {
            var mockHandler = new Mock<HttpMessageHandler>();

            mockBus.Setup(bus => bus.Publish<AddContactEvent>(It.IsAny<object>(), It.IsAny<CancellationToken>()));

            mockHandler
             .Protected()
             .Setup<Task<HttpResponseMessage>>(
                 "SendAsync",
                 ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.EndsWith($"contatos/id/{id}")),
                 ItExpr.IsAny<CancellationToken>())
             .ReturnsAsync(message);

            var factory = Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    // Remove a implementação real do serviço
                    services.RemoveAll<IDeleteContactApplication>();

                    // Cria um HttpClient com o handler mockado
                    var httpClient = new HttpClient(mockHandler.Object)
                    {
                        BaseAddress = new Uri("https://teste.com")
                    };

                    // Registra o mock de IAddContactApplication
                    services.AddSingleton<IDeleteContactApplication>(sp =>
                    {
                        return new DeleteContactApplication(mockBus.Object, httpClient);
                    });
                });
            });

            return factory;

        }
    }
}
