using Contact.Core.Dto;
using FIAP._6NETT_GRUPO31.Application.Interfaces;
using FIAP._6NETT_GRUPO31.Domain.Entities;
using FIAP._6NETT_GRUPO31.Domain.Interfaces;

namespace FIAP._6NETT_GRUPO31.Application.Services
{
    public class ContatoApplication : IContatoApplication
    {
        private readonly IContatoRepository _contatoRepository;

        public ContatoApplication(IContatoRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }

        public async Task CadastrarContato(CadastrarAtualizarContatoDto dto)
        {
            var resultado = false;            

            if (dto != null)
            {                
                resultado = await _contatoRepository.AdicionaContato(MappingContatoDtoToContato(dto));
            }            

        }

        public async Task AtualizarContrato(int contatoId,CadastrarAtualizarContatoDto dto)
        {
            try
            {
                var contatoUpdate = await _contatoRepository.ConsultarContatoPorId(contatoId);                            

                contatoUpdate.Email = dto.Email;
                contatoUpdate.Telefone = dto.Telefone;
                contatoUpdate.Nome = dto.Nome;
                contatoUpdate.DDD = dto.DDD;

                await _contatoRepository.AtualizaContato(contatoUpdate);
              
            }
            catch (Exception ex)
            {
                throw;
            }
        }
      
        public async Task DeletarContato(int contatoId)
        {
            var contatoDelete = await _contatoRepository.ConsultarContatoPorId(contatoId);            

            await _contatoRepository.DeletarContato(contatoDelete);            
        }

        private Contatos MappingContatoDtoToContato(CadastrarAtualizarContatoDto dto)
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

        private async Task<bool> ExisteEmailCadastrado(string email)
        {
            var contato = await _contatoRepository.ConsultarContatoPorEmail(email);

            return contato != null;

        }

      
    }
}
