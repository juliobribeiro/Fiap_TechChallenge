using Contact.Core.Dto;


namespace GetContact.Application.Interfaces
{
    public interface IGetContactApplication
    {

        Task<List<ContatoDto>> ConsultarContatosPorDDD(int ddd);
        Task<ContatoDto> ConsultarContatosPorEmail(string email);
        Task<ContatoDto> ConsultarContatosPorId(int id);
        Task<List<ContatoDto>> ConsultarTodosContatos();        
    }
}
