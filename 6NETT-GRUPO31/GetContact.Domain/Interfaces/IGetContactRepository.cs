using GetContact.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetContact.Domain.Interfaces
{
    public interface IGetContactRepository
    {    
        Task<IEnumerable<Contatos>> ConsultaContatos(int ddd);  
        Task<Contatos> ConsultarContatoPorId(int IdContato);
        Task<Contatos> ConsultarContatoPorEmail(string email);
    }
}
