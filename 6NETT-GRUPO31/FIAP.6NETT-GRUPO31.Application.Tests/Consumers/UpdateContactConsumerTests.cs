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
    public class UpdateContactConsumerTests
    {
        private readonly Mock<IContatoApplication> mock;
        private readonly UpdateContactConsumer _updateContactConsumer;


        public UpdateContactConsumerTests()
        {
            mock = new Mock<IContatoApplication>();
            _updateContactConsumer = new UpdateContactConsumer(mock.Object);
        }


        [Fact]

        public async Task Consume_Should_Call_AtualizarContrato_With_Correct_Parameters()
        {
            var consumeContextMock = new Mock<ConsumeContext<UpdateContactEvent>>();

            int fakeId = 1;

            UpdateContactEvent fakeEvent = new UpdateContactEvent()
            {
                Id = 1,
                DDD = 11,
                Email = "rodrigomahlow",
                Nome = "rodrigo",
                Telefone = "9889999"
            };
            // Configurar o Mock do contexto para retornar a mensagem fake
            consumeContextMock.Setup(x => x.Message).Returns(fakeEvent);

            //act
            await _updateContactConsumer.Consume(consumeContextMock.Object);

            // Assert
            mock.Verify(x => x.AtualizarContrato(fakeId,
                It.Is<CadastrarAtualizarContatoDto>(c => c.Nome == fakeEvent.Nome && c.Email == fakeEvent.Email)),
                Times.Once);
        }
    }
}
