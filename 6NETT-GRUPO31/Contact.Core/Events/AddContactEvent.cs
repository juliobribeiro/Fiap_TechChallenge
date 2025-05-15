using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Events
{
    public class AddContactEvent
    {      
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int DDD { get; set; }
    }
}
