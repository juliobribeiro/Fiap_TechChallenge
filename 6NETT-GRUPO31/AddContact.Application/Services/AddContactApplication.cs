using AddContact.Application.Interfaces;
using Contact.Core.Dto;
using Contact.Core.Events;
using FIAP._6NETT_GRUPO31.Domain.Entities;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddContact.Application.Services
{
    public class AddContactApplication : IAddContactApplication
    {
        private readonly IBus _bus;        
        private readonly HttpClient _httpClient;

        public AddContactApplication(IBus bus, HttpClient httpClient)
        {
            _bus = bus;            
            _httpClient = httpClient;
        }

        public async Task<bool> CadastrarContato(CadastrarAtualizarContatoDto dto)
        {            
            if (await ExisteEmailCadastrado(dto.Email)) throw new Exception($"O email {dto.Email} já está sendo usando para outro contato");
            {
                AddContactEvent contactEvent = new AddContactEvent();
                contactEvent.Nome = dto.Nome;
                contactEvent.Email = dto.Email;
                contactEvent.Telefone = dto.Telefone;
                contactEvent.DDD = dto.DDD;
                await _bus.Publish(contactEvent);
            }

            return true;
        }

        private async Task<bool> ExisteEmailCadastrado(string email)
        {            
            var httpResponseMessage = await _httpClient.GetAsync($"contatos/email/{email}");

            if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.NoContent)
                return false;

            return true;

        }



    }
}
