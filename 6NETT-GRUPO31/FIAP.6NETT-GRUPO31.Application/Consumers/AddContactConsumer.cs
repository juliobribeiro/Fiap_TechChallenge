
using Contact.Core.Events;
using MassTransit;
using FIAP._6NETT_GRUPO31.Application.Interfaces;

namespace FIAP._6NETT_GRUPO31.Application.Consumers
{
    public class AddContactConsumer : IConsumer<AddContactEvent>
    {
        private readonly IContatoApplication _contatoApplication;

        public AddContactConsumer(IContatoApplication contatoApplication)
        {
            _contatoApplication = contatoApplication;
        }

        
        public async Task Consume(ConsumeContext<AddContactEvent> context)
        {
            try
            {
              
;
                await _contatoApplication.CadastrarContato(Utils.Utils.MappingContatoEventToContatoDto(context.Message));

            }
            catch (Exception)
            {

                throw;
            }
            
        }
       
    }
}
