using Contact.Core.Dto;
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
    public class AddContactConsumerTests
    {
        private readonly Mock<IContatoApplication> mock;
        private readonly AddContactConsumer _addContactConsumer;


        public AddContactConsumerTests()
        {
            mock = new Mock<IContatoApplication>();
            _addContactConsumer = new AddContactConsumer(mock.Object);
        }


        [Fact]

        public async Task Consume_Should_Call_CadastrarContato()
        {
            var consumeContextMock = new Mock<ConsumeContext<AddContactEvent>>();

            AddContactEvent fakeEvent = new AddContactEvent()
            {
                DDD = 11,
                Email = "rodrigomahlow",
                Nome = "rodrigo",
                Telefone = "9889999"
            };
            // Configurar o Mock do contexto para retornar a mensagem fake
            consumeContextMock.Setup(x => x.Message).Returns(fakeEvent);

            //act
            await _addContactConsumer.Consume(consumeContextMock.Object);

            // Assert
            mock.Verify(x => x.CadastrarContato(
                It.Is<CadastrarAtualizarContatoDto>(c => c.Nome == fakeEvent.Nome && c.Email == fakeEvent.Email)),
                Times.Once);
        }


    }
}
