using DeleteContact.API.Controllers;
using DeleteContact.Application.Interfaces;
using DeleteContact.Application.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteContact.API.Tests.Controller
{


    public class DeleteContactControllerTests
    {

        private readonly Mock<IDeleteContactApplication> _deleteContactApplication;
        private readonly DeleteContactController _controller;

        public DeleteContactControllerTests()
        {
            _deleteContactApplication = new Mock<IDeleteContactApplication>();
            _controller = new DeleteContactController(_deleteContactApplication.Object);
        }

        [Fact]
        public async Task DeletarContato_ShouldReturnNoContentResult_WhenDeletionIsSuccessful()
        {
            // Arrange
            var id = 1;
            _deleteContactApplication.Setup(x => x.DeletarContato(id)).ReturnsAsync(true);

            // Act
            var result = await _controller.DeletarContato(id);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

    }
}
