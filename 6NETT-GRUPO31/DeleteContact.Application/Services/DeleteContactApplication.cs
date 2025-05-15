using Contact.Core.Dto;
using Contact.Core.Events;
using DeleteContact.Application.Interfaces;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteContact.Application.Services
{
    public class DeleteContactApplication : IDeleteContactApplication
    {
        private readonly IBus _bus;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;

        public DeleteContactApplication(IBus bus, HttpClient client)
        {
            _bus = bus;            
            _client = client;
        }

        public async Task<bool> DeletarContato(int contatoId)
        {
            var contatoDelete = await GetContatoPorId(contatoId);

            if (contatoDelete is null) throw new Exception($"Contato com id:{contatoId} não encontrado");

            DeleteContactEvent deleteContactEvent = new DeleteContactEvent();
            deleteContactEvent.Id = contatoId;
            await _bus.Publish(deleteContactEvent);

            return true;
        }

        private async Task<ContatoDto?> GetContatoPorId(int id)
        {            
            var httpResponseMessage = await _client.GetAsync($"contatos/id/{id}");

            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<ContatoDto>(await httpResponseMessage.Content.ReadAsStringAsync());

            return null;
        }
    }
}
