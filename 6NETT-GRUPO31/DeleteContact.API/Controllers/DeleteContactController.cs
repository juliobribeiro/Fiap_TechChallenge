using Contact.WebApi.Core.Controllers;
using DeleteContact.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DeleteContact.API.Controllers
{
    public class DeleteContactController : MainController
    {
        private readonly IDeleteContactApplication _deleteContactApplication;

        public DeleteContactController(IDeleteContactApplication deleteContactApplication)
        {
            _deleteContactApplication = deleteContactApplication;
        }

        [HttpDelete("/contato/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeletarContato(int id)
        {
            try
            {
                await _deleteContactApplication.DeletarContato(id);
                return NoContent();
            }
            catch (Exception ex)
            {

                AdicionarErroProcessamento("Erro ao deletar o contato");
                AddExptionErrorMessage(ex);
                return CustomResponse(); ;
            }
        }
    }
}
