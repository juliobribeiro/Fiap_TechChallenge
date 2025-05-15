using Contact.Core.Dto;
namespace FIAP._6NETT_GRUPO31.Application.Interfaces
{
    public interface IContatoApplication
    {
        Task CadastrarContato(CadastrarAtualizarContatoDto dto);
        Task AtualizarContrato(int contatoId, CadastrarAtualizarContatoDto dto);
        Task DeletarContato(int contatoId);
    }
}
