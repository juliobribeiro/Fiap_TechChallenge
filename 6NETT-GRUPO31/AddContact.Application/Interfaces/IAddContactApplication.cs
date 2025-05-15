using Contact.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddContact.Application.Interfaces
{
    public interface IAddContactApplication
    {
        Task<Boolean> CadastrarContato(CadastrarAtualizarContatoDto dto);
    }
}
