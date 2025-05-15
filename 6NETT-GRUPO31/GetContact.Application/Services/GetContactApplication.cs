using Contact.Core.Dto;
using GetContact.Application.Interfaces;
using GetContact.Domain.Entities;
using GetContact.Domain.Interfaces;


namespace GetContact.Application.Services
{
    public class GetContactApplication : IGetContactApplication
    {
        private readonly IGetContactRepository _contatoRepository;

        public GetContactApplication(IGetContactRepository contatoRepository)
        {
            _contatoRepository = contatoRepository;
        }


             
        public async Task<List<ContatoDto>> ConsultarContatosPorDDD(int ddd)
        {
            var listaContatoDto = new List<ContatoDto>();

            var listEntidade = await _contatoRepository.ConsultaContatos(ddd);

            return MappingContatosToContatoDto(listEntidade.ToList());
        }

        public async Task<List<ContatoDto>> ConsultarTodosContatos()
        {

            var listEntidade = await _contatoRepository.ConsultaContatos(0);

            return MappingContatosToContatoDto(listEntidade.ToList());
        }
  

        private List<ContatoDto> MappingContatosToContatoDto(List<Contatos> contatos)
        {
            var listaContatoDto = new List<ContatoDto>();

            foreach (var item in contatos)
            {                
                listaContatoDto.Add(MappingContatoToContatoDto(item));
            }

            return listaContatoDto;
        }

        private ContatoDto MappingContatoToContatoDto(Contatos contatos)
        {
            
                ContatoDto dto = new ContatoDto()
                {
                    IdContato = contatos.IdContato,
                    Nome = contatos.Nome,
                    Email = contatos.Email,
                    Telefone = contatos.Telefone,
                    DDD = contatos.DDD
                };

            return dto ;
        }     

        public async Task<ContatoDto> ConsultarContatosPorEmail(string email)
        {
            var contato = await _contatoRepository.ConsultarContatoPorEmail(email);
            if(contato != null) return MappingContatoToContatoDto(contato);

            return null;
            
        }

        public async Task<ContatoDto> ConsultarContatosPorId(int id)
        {
            var contato = await _contatoRepository.ConsultarContatoPorId(id);
            if(contato != null) return MappingContatoToContatoDto(contato);
            return null;
        }
    }
}
