using FIAP._6NETT_GRUPO31.Application.Interfaces;
using FIAP._6NETT_GRUPO31.Infra.Data.Context;
using FIAP._6NETT_GRUPO31.API.Model;
using FIAP._6NETT_GRUPO31.Tests.Infra;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP._6NETT_GRUPO31.Tests.Services
{
    [Collection("Test collection")]
    public class ContatoControllerTest
    {
        private readonly TextFixture _textFixture;        
        private readonly IContatoApplication _contatoApplication;       
        public ContatoControllerTest(TextFixture fixture)
        {
            _textFixture = fixture;

            var scope = _textFixture._servicesCollection.BuildServiceProvider().CreateScope();
            var _context = scope.ServiceProvider.GetRequiredService<FIAPContext>();
            _contatoApplication = scope.ServiceProvider.GetRequiredService<IContatoApplication>();        

            _textFixture.CreateContext(_context);
        }

        //[Fact]
        //public async Task ConsultarContatos_ShouldReturnOkResult_WithListOfContatos()
        //{
        //    var contatos = await _contatoController.ConsultarContatos();

        //    var okResult = Assert.IsType<OkObjectResult>(contatos);
        //    var returnedContatos = Assert.IsType<List<ContatoModel>>(okResult.Value);
        //}

        //[Theory]
        //[InlineData(11)]
        //[InlineData(12)]
        //public async Task ConsultarContatosPorDDD_ShouldReturnOkResult_WithListOfContatos(int ddd)
        //{
        //    var contatos = await _contatoController.ConsultarContatosPorDDD(ddd);

        //    var okResult = Assert.IsType<OkObjectResult>(contatos);
        //    var returnedContatos = Assert.IsType<List<ContatoModel>>(okResult.Value);
        //}

        //[Fact]
        //public async Task CadastrarContato_ShouldReturnCreatedResult_WhenModelIsValid()
        //{
        //    // Arrange
        //    var contatoModel = new CadastrarAtualizarContatoModel
        //    {
        //        Nome = "João",
        //        Email = "joao@email.com",
        //        Telefone = "123456789",
        //        DDD = 11
        //    };

        //    var result = await _contatoController.CadastrarContato(contatoModel);            

        //    // Assert
        //    Assert.IsType<CreatedResult>(result);
        //}

        //[Fact]
        //public async Task CadastrarContato_ShouldReturnBadRequest_WhenEmailExist()
        //{
        //    // Arrange
        //    var contatoModel = new CadastrarAtualizarContatoModel
        //    {
        //        Nome = "João",
        //        Email = "rodrigo@email.com",
        //        Telefone = "123456789",
        //        DDD = 11
        //    };

        //    var result = await _contatoController.CadastrarContato(contatoModel);

        //    // Assert
        //    var badRequest = Assert.IsType<BadRequestObjectResult>(result);
        //    var returnedContatos = Assert.IsType<ValidationProblemDetails>(badRequest.Value);
        //    Assert.NotNull(returnedContatos.Errors.FirstOrDefault(x => x.Value.Equals("O email rodrigo@email.com já está sendo usando para outro contato")));

        //}


        //[Theory]
        //[InlineData(1)]
        //[InlineData(2)]
        //public async Task DeletarContato_ShouldReturnNoContentResult_WhenDeletionIsSuccessful(int id)
        //{
        //    // Arrange         
        //    // Act
        //    var result = await _contatoController.DeletarContato(id);

        //    // Assert
        //    Assert.IsType<NoContentResult>(result);
        //}

        //[Fact]
        //public async Task AtualizarContato_ShouldReturnNoContentResult_WhenModelIsValid()
        //{
        //    // Arrange
        //    var id = 1;
        //    var contatoModel = new CadastrarAtualizarContatoModel
        //    {
        //        Nome = "João",
        //        Email = "joao@email.com",
        //        Telefone = "123456789",
        //        DDD = 11
        //    };           

        //    // Act
        //    var result = await _contatoController.AtualizarContato(id, contatoModel);

        //    // Assert
        //    Assert.IsType<NoContentResult>(result);
        //}





    }
}
