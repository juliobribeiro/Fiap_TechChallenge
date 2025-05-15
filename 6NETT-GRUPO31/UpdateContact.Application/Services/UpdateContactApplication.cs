using Contact.Core.Dto;
using Contact.Core.Events;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using UpdateContact.Application.Interfaces;

namespace UpdateContact.Application.Services
{
    public class UpdateContactApplication : IUpdateContactApplication
    {
        private readonly IBus _bus;        
        private readonly HttpClient _httpClient;

        public UpdateContactApplication(IBus bus, HttpClient httpClient)
        {
            _bus = bus;            
            _httpClient = httpClient;
        }

        public async Task<bool> AtualizarContrato(int contatoId, CadastrarAtualizarContatoDto dto)
        {
            var contatoUpdate = await GetContatoPorId(contatoId);

            if (contatoUpdate is null) throw new Exception($"Contato com  id:{contatoId} não encontrado");

            if (contatoUpdate.Email != dto.Email)
            {
                if (await ExisteEmailCadastrado(dto.Email)) throw new Exception($"O email {dto.Email} já está sendo usando para outro contato");
            }

            UpdateContactEvent updateContactEvent = new UpdateContactEvent();
            updateContactEvent.Id = contatoId;
            updateContactEvent.Email = dto.Email;
            updateContactEvent.Telefone = dto.Telefone;
            updateContactEvent.Nome = dto.Nome;
            updateContactEvent.DDD = dto.DDD;
            await _bus.Publish(updateContactEvent);

            return true;
        }


        private async Task<bool> ExisteEmailCadastrado(string email)
        {            
            var httpResponseMessage = await _httpClient.GetAsync($"contatos/email/{email}");

            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                return false;

            return true;

        }

        private async Task<ContatoDto?> GetContatoPorId(int id)
        {                        
            var httpResponseMessage = await _httpClient.GetAsync($"contatos/id/{id}");

            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                return JsonConvert.DeserializeObject<ContatoDto>(await httpResponseMessage.Content.ReadAsStringAsync());

            return null;
        }
    }
}
