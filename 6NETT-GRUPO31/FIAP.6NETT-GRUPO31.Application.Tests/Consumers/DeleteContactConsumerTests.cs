using Contact.Core.Events;
using FIAP._6NETT_GRUPO31.Application.Consumers;
using FIAP._6NETT_GRUPO31.Application.Interfaces;
using MassTransit;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP._6NETT_GRUPO31.Application.Tests.Consumers
{
    public class DeleteContactConsumerTests
    {

        private readonly Mock<IContatoApplication> mock;
        private readonly DeleteContactConsumer _deleteContactConsumer;


        public DeleteContactConsumerTests()
        {
            mock = new Mock<IContatoApplication>();
            _deleteContactConsumer = new DeleteContactConsumer(mock.Object);
        }


        [Fact]

        public async Task Consume_Should_Call_DeletarContato_With_Correct_Id()
        {
            var consumeContextMock = new Mock<ConsumeContext<DeleteContactEvent>>();

            DeleteContactEvent fakeEvent = new DeleteContactEvent()
            {
               Id = 1
            };
            // Configurar o Mock do contexto para retornar a mensagem fake
            consumeContextMock.Setup(x => x.Message).Returns(fakeEvent);

            //act
            await _deleteContactConsumer.Consume(consumeContextMock.Object);

            // Assert
            mock.Verify(x => x.DeletarContato(fakeEvent.Id), Times.Once);
        }
    }
}
