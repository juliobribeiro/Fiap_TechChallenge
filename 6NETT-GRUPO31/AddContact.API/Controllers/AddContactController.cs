using AddContact.Application.Interfaces;
using Contact.Core.Dto;
using Contact.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace AddContact.API.Controllers
{
    public class AddContactController : MainController
    {
        private readonly IAddContactApplication _addContactApplication;

        public AddContactController(IAddContactApplication addContactApplication)
        {
            _addContactApplication = addContactApplication;
        }

        [HttpPost("/contato")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CadastrarContato(
            [FromBody] CadastrarAtualizarContatoDto contato)
        {
            try
            {
                if (!ModelState.IsValid) return CustomResponse(ModelState);

                var dto = new CadastrarAtualizarContatoDto()
                {
                    Nome = contato.Nome,
                    Email = contato.Email,
                    Telefone = contato.Telefone,
                    DDD = contato.DDD
                };
                await _addContactApplication.CadastrarContato(dto);                

                return StatusCode(201);
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento("Erro ao cadastrar o contato");
                AddExptionErrorMessage(ex);
                return CustomResponse();

            }
        }
    }
}
