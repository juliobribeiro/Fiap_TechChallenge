using Contact.Core.Dto;


namespace UpdateContact.Application.Interfaces
{
   public interface IUpdateContactApplication
    {
        Task<Boolean> AtualizarContrato(int contatoId, CadastrarAtualizarContatoDto dto);
    }
}
