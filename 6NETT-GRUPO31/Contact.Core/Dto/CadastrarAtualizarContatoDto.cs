using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Dto
{
    public class CadastrarAtualizarContatoDto
    {       

        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo e-mail é obrigatório")]
        [EmailAddress(ErrorMessage ="E-mail inválido")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo telefone é obrigatório")]
        [MaxLength(9,ErrorMessage ="O campo telefone pode ter no máximo 9 caracteres")]
        public string Telefone { get; set; }
        
        [Required(ErrorMessage = "O campo DDD é obrigatório")]
        [Range(1, 99,ErrorMessage ="O DDD precisa estar entre 0 e 99")]
        public int DDD { get; set; }
    }


}
