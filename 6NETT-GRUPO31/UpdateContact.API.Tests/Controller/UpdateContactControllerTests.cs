using Contact.Core.Dto;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UpdateContact.API.Controllers;
using UpdateContact.Application.Interfaces;

namespace UpdateContact.API.Tests.Controller
{
    public class UpdateContactControllerTests
    {
        private readonly UpdateContactController _updateContactController;
        private readonly Mock<IUpdateContactApplication> mock;


        public UpdateContactControllerTests()
        {
            mock = new Mock<IUpdateContactApplication>();
            _updateContactController = new UpdateContactController(mock.Object);
        }

        [Fact]
        public async Task AtualizarContato_ShouldReturnNoContentResult_WhenModelIsValid()
        {
            // Arrange
            var id = 1;
            var contatoModel = new CadastrarAtualizarContatoDto
            {
                Nome = "João",
                Email = "joao@email.com",
                Telefone = "123456789",
                DDD = 11
            };

            mock.Setup(x => x.AtualizarContrato(id, It.IsAny<CadastrarAtualizarContatoDto>())).ReturnsAsync(true);

            // Act
            var result = await _updateContactController.AtualizarContato(id, contatoModel);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }


    }
}
