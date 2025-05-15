using AddContact.Application.Interfaces;
using AddContact.Application.Services;
using AddContact.Tests.Infra;
using Contact.Core.Dto;
using Contact.Core.Events;
using MassTransit;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Text;


namespace AddContact.Tests.Services
{
    public class AddContactTests : MyWebApplicationFactory<Program, DbContext>
    {

        private readonly Mock<IBus> mockBus;
        public AddContactTests(WebApplicationFactory<Program> factory) : base(factory)
        {
            mockBus = new Mock<IBus>();
        }

        [Fact]
        public async Task AdicionarContato_Sucess()
        {

            var data = CreateDto();
            var queryString = CreateBodyPost(data);

            WebApplicationFactory<Program> factory = SetNewContactApplication(data, new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.NoContent
            });

            var Client = factory.CreateClient();

            var result = await Client.PostAsync("/contato", queryString);

            Assert.Equal(HttpStatusCode.Created, result.StatusCode);

        }


        [Fact]
        public async Task AdicionarContato_Error_Email_Cadastrado()
        {
            var data = CreateDto();
            var queryString = CreateBodyPost(data);

            WebApplicationFactory<Program> factory = SetNewContactApplication(data, new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK
            });

            var Client = factory.CreateClient();


            var result = await Client.PostAsync("/contato", queryString);

            var content = await result.Content.ReadAsStringAsync();

            Assert.Contains($"O email {data.Email} já está sendo usando para outro contato", content);

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);

        }

        private static CadastrarAtualizarContatoDto CreateDto()
        {
            var data = new CadastrarAtualizarContatoDto();
            data.Email = "teste@teste.com.br";
            data.Telefone = "119888888";
            data.Nome = "rodrigo";
            data.DDD = 11;
            return data;
        }

        private static StringContent CreateBodyPost(CadastrarAtualizarContatoDto data)
        {
            return new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        }

        private WebApplicationFactory<Program> SetNewContactApplication(CadastrarAtualizarContatoDto data, HttpResponseMessage message)
        {
            var mockHandler = new Mock<HttpMessageHandler>();

            mockBus.Setup(bus => bus.Publish<AddContactEvent>(It.IsAny<object>(), It.IsAny<CancellationToken>()));

            mockHandler
             .Protected()
             .Setup<Task<HttpResponseMessage>>(
                 "SendAsync",
                 ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.EndsWith($"contatos/email/{data.Email}")),
                 ItExpr.IsAny<CancellationToken>())
             .ReturnsAsync(message);

            var factory = Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    // Remove a implementação real do serviço
                    services.RemoveAll<IAddContactApplication>();

                    // Cria um HttpClient com o handler mockado
                    var httpClient = new HttpClient(mockHandler.Object)
                    {
                        BaseAddress = new Uri("https://teste.com")
                    };

                    // Registra o mock de IAddContactApplication
                    services.AddSingleton<IAddContactApplication>(sp =>
                    {
                        return new AddContactApplication(mockBus.Object, httpClient);
                    });
                });
            });
            return factory;
        }
    }
}
