using GetContact.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace GetContact.FNC
{
    public class GetContactByEmailFunction
    {
        private readonly ILogger<GetContactByEmailFunction> _logger;
        private readonly IGetContactApplication _getContactApplication;

        public GetContactByEmailFunction(ILogger<GetContactByEmailFunction> logger, IGetContactApplication getContactApplication)
        {
            _logger = logger;
            _getContactApplication = getContactApplication;
        }

        [Function("GetContactByEmailFunction")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get",Route ="contatos/email/{email}")] HttpRequest req, string email)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult(await _getContactApplication.ConsultarContatosPorEmail(email));
        }
    }
}
