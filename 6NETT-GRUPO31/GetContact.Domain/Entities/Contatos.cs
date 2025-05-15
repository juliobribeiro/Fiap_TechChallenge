using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GetContact.Domain.Entities
{
    public class Contatos
    {
        public int IdContato { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public int DDD { get; set; }

        public Contatos()
        {

        }

        public Contatos(int idContato, string nome, string email, string telefone, int DDD)
        {
            IdContato = idContato;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            this.DDD = DDD;
        }
    }
}
