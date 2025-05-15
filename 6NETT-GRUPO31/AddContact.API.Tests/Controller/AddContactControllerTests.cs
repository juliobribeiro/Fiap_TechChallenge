using AddContact.API.Controllers;
using AddContact.Application.Interfaces;
using Contact.Core.Dto;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddContact.API.Tests.Controller
{
    public class AddContactControllerTests
    {
        private readonly Mock<IAddContactApplication> _addContactApplication;
        private readonly AddContactController _controller;

        public AddContactControllerTests()
        {
            _addContactApplication = new Mock<IAddContactApplication>();
            _controller = new AddContactController(_addContactApplication.Object);
        }


        [Fact]
        public async Task CadastrarContato_ShouldReturnCreatedResult_WhenModelIsValid()
        {
            // Arrange
            var contatoModel = new CadastrarAtualizarContatoDto
            {
                Nome = "João",
                Email = "joao@email.com",
                Telefone = "123456789",
                DDD = 11
            };

            _addContactApplication.Setup(x => x.CadastrarContato(It.IsAny<CadastrarAtualizarContatoDto>())).ReturnsAsync(true);

            // Act
            var result = await _controller.CadastrarContato(contatoModel);

            // Assert
            Assert.IsType<StatusCodeResult>(result);
        }


    }
}
