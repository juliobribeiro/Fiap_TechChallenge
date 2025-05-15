using GetContact.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace GetContact.FNC
{
    public class GetContactById
    {
        private readonly ILogger<GetContactById> _logger;
        private readonly IGetContactApplication _getContactApplication;

        public GetContactById(ILogger<GetContactById> logger, IGetContactApplication getContactApplication)
        {
            _logger = logger;
            _getContactApplication = getContactApplication;
        }

        [Function("GetContactById")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", Route = "contatos/id/{id:int}")] HttpRequest req,int id)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult(await _getContactApplication.ConsultarContatosPorId(id));
        }
    }
}
