using Contact.Core.Events;
using FIAP._6NETT_GRUPO31.Application.Interfaces;
using MassTransit;


namespace FIAP._6NETT_GRUPO31.Application.Consumers
{
    public class DeleteContactConsumer : IConsumer<DeleteContactEvent>
    {
        public readonly IContatoApplication _contatoApplication;

        public DeleteContactConsumer(IContatoApplication contatoApplication)
        {
            _contatoApplication = contatoApplication;
        }

        public async Task Consume(ConsumeContext<DeleteContactEvent> context)
        {
            try
            {
                await _contatoApplication.DeletarContato(context.Message.Id);
            }
            catch (Exception)
            {

                throw;
            }
                            
        }
    }
}
