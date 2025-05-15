using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteContact.Application.Interfaces
{
    public interface IDeleteContactApplication
    {
        Task<Boolean> DeletarContato(int contatoId);
    }
}
