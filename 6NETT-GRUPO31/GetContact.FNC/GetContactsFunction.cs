using GetContact.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace GetContact.FNC
{
    public class GetContactsFunction
    {
        private readonly ILogger<GetContactsFunction> _logger;
        private readonly IGetContactApplication _getContactApplication;

        public GetContactsFunction(ILogger<GetContactsFunction> logger, IGetContactApplication getContactApplication)
        {
            _logger = logger;
            _getContactApplication = getContactApplication;
        }

        [Function("GetContactFunction")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route ="contatos")] HttpRequest req, int? ddd)
        {                      
            return new OkObjectResult(await _getContactApplication.ConsultarTodosContatos());            
        }       
    }
}
