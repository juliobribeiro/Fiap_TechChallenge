using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace GetContact.FNC
{
    public class UpdateContactDLQ
    {
        private readonly ILogger _logger;

        public UpdateContactDLQ(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<UpdateContactDLQ>();
        }

        [Function("UpdateContactDLQ")]
        public void Run([RabbitMQTrigger("UpdateContact_error", ConnectionStringSetting = "RabbitMQConnection")] string myQueueItem)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
