using Contact.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIAP._6NETT_GRUPO31.Application.Utils;
using FIAP._6NETT_GRUPO31.Domain.Entities;
using Contact.Core.Dto;

namespace FIAP._6NETT_GRUPO31.Application.Tests.Utils
{
    public class UtilsTests
    {

        [Fact]
        public void MappingContatoEventToContatoTeste()
        {
            AddContactEvent addContactEvent = new AddContactEvent()
            {
                DDD = 11,
                Email = "rodrigomahlow",
                Nome = "rodrigo",
                Telefone = "9889999"
            };


            var result = FIAP._6NETT_GRUPO31.Application.Utils.Utils.MappingContatoEventToContato(addContactEvent);
            Assert.IsType<Contatos>(result);
            Assert.Equal(addContactEvent.Email, result.Email);
            Assert.Equal(addContactEvent.Nome, result.Nome);
            Assert.Equal(addContactEvent.DDD, result.DDD);
            Assert.Equal(addContactEvent.Telefone, result.Telefone);            
        }


        [Fact]
        public void MappingContatoEventAddToContatoDto()
        {
            AddContactEvent addContactEvent = new AddContactEvent()
            {
                DDD = 11,
                Email = "rodrigomahlow",
                Nome = "rodrigo",
                Telefone = "9889999"
            };


            var result = FIAP._6NETT_GRUPO31.Application.Utils.Utils.MappingContatoEventToContatoDto(addContactEvent);
            Assert.IsType<CadastrarAtualizarContatoDto>(result);
            Assert.Equal(addContactEvent.Email, result.Email);
            Assert.Equal(addContactEvent.Nome, result.Nome);
            Assert.Equal(addContactEvent.DDD, result.DDD);
            Assert.Equal(addContactEvent.Telefone, result.Telefone);

        }

        [Fact]
        public void MappingContatoEventUpdateToContatoDto()
        {
            UpdateContactEvent addContactEvent = new UpdateContactEvent()
            {
                DDD = 11,
                Email = "rodrigomahlow",
                Nome = "rodrigo",
                Telefone = "9889999"
            };


            var result = FIAP._6NETT_GRUPO31.Application.Utils.Utils.MappingContatoEventToContatoDto(addContactEvent);
            Assert.IsType<CadastrarAtualizarContatoDto>(result);
            Assert.Equal(addContactEvent.Email, result.Email);
            Assert.Equal(addContactEvent.Nome, result.Nome);
            Assert.Equal(addContactEvent.DDD, result.DDD);
            Assert.Equal(addContactEvent.Telefone, result.Telefone);

        }
    }
}
