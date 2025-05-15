using Contact.Core.Dto;
using Contact.WebApi.Core.Controllers;
using Microsoft.AspNetCore.Mvc;
using UpdateContact.Application.Interfaces;

namespace UpdateContact.API.Controllers
{
    public class UpdateContactController : MainController
    {
        private readonly IUpdateContactApplication _updateContactApplication;

        public UpdateContactController(IUpdateContactApplication updateContactApplication)
        {
            _updateContactApplication = updateContactApplication;
        }

        [HttpPut("/contato/{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarContato(int id, [FromBody]CadastrarAtualizarContatoDto contato)
        {
            try
            {
                var dto = new CadastrarAtualizarContatoDto()
                {
                    Nome = contato.Nome,
                    Email = contato.Email,
                    Telefone = contato.Telefone,
                    DDD = contato.DDD
                };

                await _updateContactApplication.AtualizarContrato(id, dto);

                return NoContent();
            }
            catch (Exception ex)
            {
                AdicionarErroProcessamento("Erro ao atualizar o contatos");
                AddExptionErrorMessage(ex);
                return CustomResponse();

            }
        }
    }
}
