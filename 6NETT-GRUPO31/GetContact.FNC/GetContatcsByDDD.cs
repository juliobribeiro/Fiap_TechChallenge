using GetContact.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace GetContact.FNC
{
    public class GetContatcsByDDD
    {
        private readonly ILogger<GetContatcsByDDD> _logger;
        private readonly IGetContactApplication _getContactApplication;

        public GetContatcsByDDD(ILogger<GetContatcsByDDD> logger, IGetContactApplication getContactApplication)
        {
            _logger = logger;
            _getContactApplication = getContactApplication;
        }

        [Function("GetContatcsByDDD")]
        public async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "get", Route = "contatos/ddd/{ddd}")] HttpRequest req,string ddd)
        {
            return new OkObjectResult(await _getContactApplication.ConsultarContatosPorDDD(Convert.ToInt32(ddd)));
        }
    }
}
