using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace GetContact.FNC
{
    public class DeleteContactDLQ
    {
        private readonly ILogger _logger;

        public DeleteContactDLQ(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<DeleteContactDLQ>();
        }

        [Function("DeleteContactDLQ")]
        public void Run([RabbitMQTrigger("DeleteContact_error", ConnectionStringSetting = "RabbitMQConnection")] string myQueueItem)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
