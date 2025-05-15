using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.Core.Dto
{
    public class RabbitMqConnection
    {
        public string HostName { get; set; }
        public string Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
