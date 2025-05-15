using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FIAP._6NETT_GRUPO31.Domain.Entities;

namespace FIAP._6NETT_GRUPO31.Domain.Interfaces
{
    public interface IContatoRepository
    {
        Task<bool> AdicionaContato(Contatos contato);
        Task<IEnumerable<Contatos>> ConsultaContatos(int ddd);
        Task AtualizaContato(Contatos contato);
        Task<Contatos> ConsultarContatoPorId(int IdContato);
        Task<bool> DeletarContato(Contatos contato);
        Task<Contatos> ConsultarContatoPorEmail(string email);
    }
}
