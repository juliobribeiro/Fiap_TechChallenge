using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace GetContact.FNC
{
    public class AddContactDLQ
    {
        private readonly ILogger _logger;

        public AddContactDLQ(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<AddContactDLQ>();
        }

        [Function("AddContactDQL")]
        public void Run([RabbitMQTrigger("AddContact_error", ConnectionStringSetting = "RabbitMQConnection")] string myQueueItem)
        {
            _logger.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
