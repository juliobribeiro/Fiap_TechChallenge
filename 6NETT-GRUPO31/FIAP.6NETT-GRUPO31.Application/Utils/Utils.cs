using Contact.Core.Dto;
using Contact.Core.Events;
using FIAP._6NETT_GRUPO31.Domain.Entities;

namespace FIAP._6NETT_GRUPO31.Application.Utils
{
    public static class Utils
    {
        public static Contatos MappingContatoEventToContato(AddContactEvent dto)
        {
            var entidadeContato = new Contatos()
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Telefone = dto.Telefone,
                DDD = dto.DDD
            };

            return entidadeContato;
        }

        public static CadastrarAtualizarContatoDto MappingContatoEventToContatoDto(AddContactEvent dto)
        {
            var entidadeContato = new CadastrarAtualizarContatoDto()
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Telefone = dto.Telefone,
                DDD = dto.DDD
            };

            return entidadeContato;
        }

        public static CadastrarAtualizarContatoDto MappingContatoEventToContatoDto(UpdateContactEvent dto)
        {
            var entidadeContato = new CadastrarAtualizarContatoDto()
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Telefone = dto.Telefone,
                DDD = dto.DDD
            };

            return entidadeContato;
        }
    }
}
