using Contact.Core.Dto;
using Contact.Core.Events;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UpdateContact.Application.Interfaces;
using UpdateContact.Application.Services;
using UpdateContact.Tests.Infra;

namespace UpdateContact.Tests.Services
{
    public class UpdateContactTests : MyWebApplicationFactory<Program, DbContext>
    {
        private readonly Mock<IBus> mockBus;
        public UpdateContactTests(WebApplicationFactory<Program> factory) : base(factory)
        {
            mockBus = new Mock<IBus>();
        }

        [Fact]
        public async Task AtualizaContato_DeveRetornarNoContent_QuandoAtualizacaoBemSucedida()
        {
            int idContato = 1;

            var dtonew = NewDto();

            var dtoOld = OldDto();

            var factory = SetNewContactApplication(idContato, dtonew.Email,
                new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(dtoOld))
                },
            new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.NoContent
            });

            var Client = factory.CreateClient();

            var result = await Client.PutAsync($"/contato/{idContato}", CreateBodyPost(dtonew));

            Assert.Equal(HttpStatusCode.NoContent, result.StatusCode);

        }

        [Fact]
        public async Task AtualizaContato_DeveRetornarBadRequest_QuandoIdNaoExiste()
        {
            int idContato = 1;

            var dtonew = NewDto();

            var dtoOld = OldDto();

            var factory = SetNewContactApplication(idContato, dtonew.Email,
                new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.NoContent
                },
            new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.NoContent
            });

            var Client = factory.CreateClient();

            var result = await Client.PutAsync($"/contato/{idContato}", CreateBodyPost(dtonew));

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);

        }

        [Fact]
        public async Task AtualizaContato_DeveRetornarBadRequest_QuandoEmailDuplicado()
        {
            int idContato = 1;

            var dtonew = NewDto();

            var dtoOld = OldDto();

            var factory = SetNewContactApplication(idContato, dtonew.Email,
                new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.OK,
                    Content = new StringContent(JsonConvert.SerializeObject(dtoOld))
                },
                new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.OK
                });

            var Client = factory.CreateClient();

            var result = await Client.PutAsync($"/contato/{idContato}", CreateBodyPost(dtonew));

            Assert.Equal(HttpStatusCode.BadRequest, result.StatusCode);

        }

        private CadastrarAtualizarContatoDto NewDto()
        {
            return new CadastrarAtualizarContatoDto
            {
                Nome = "João",
                Email = "joao1@email.com",
                Telefone = "123456789",
                DDD = 11
            };
        }

        private CadastrarAtualizarContatoDto OldDto()
        {
            return new CadastrarAtualizarContatoDto
            {
                Nome = "João",
                Email = "joao@email.com",
                Telefone = "123456789",
                DDD = 11
            };
        }

        private static StringContent CreateBodyPost(CadastrarAtualizarContatoDto data)
        {
            return new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
        }

        private WebApplicationFactory<Program> SetNewContactApplication(int id, string newEmail, HttpResponseMessage messageId, HttpResponseMessage messageEmail)
        {
            var mockHandler = new Mock<HttpMessageHandler>();

            mockBus.Setup(bus => bus.Publish<AddContactEvent>(It.IsAny<object>(), It.IsAny<CancellationToken>()));

            mockHandler
             .Protected()
             .Setup<Task<HttpResponseMessage>>(
                 "SendAsync",
                 ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.EndsWith($"contatos/id/{id}")),
                 ItExpr.IsAny<CancellationToken>())
             .ReturnsAsync(messageId);

            mockHandler
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.Is<HttpRequestMessage>(x => x.RequestUri.AbsoluteUri.EndsWith($"contatos/email/{newEmail}")),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(messageEmail);

            var factory = Factory.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    // Remove a implementação real do serviço
                    services.RemoveAll<IUpdateContactApplication>();

                    // Cria um HttpClient com o handler mockado
                    var httpClient = new HttpClient(mockHandler.Object)
                    {
                        BaseAddress = new Uri("https://teste.com")
                    };

                    // Registra o mock de IAddContactApplication
                    services.AddSingleton<IUpdateContactApplication>(sp =>
                    {
                        return new UpdateContactApplication(mockBus.Object, httpClient);
                    });
                });
            });
            return factory;
        }
    }
}
