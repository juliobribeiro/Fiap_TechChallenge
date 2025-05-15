using FIAP._6NETT_GRUPO31.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP._6NETT_GRUPO31.Domain.Tests.Entities
{
    public class ContatosTests
    {
        [Fact]
        public void DevoCriarPropriedadesEConstrutor()
        {
            // Arrange & Act
            var contato = new Contatos();

            // Assert
            Assert.Equal(0, contato.IdContato); 
            Assert.Null(contato.Nome); 
            Assert.Null(contato.Email);
            Assert.Null(contato.Telefone);
            Assert.Equal(0, contato.DDD); 
        }

        [Fact]
        public void DevoCriarContatosComConstructorParametrizados()
        {
            // Arrange
            int expectedId = 1;
            string expectedNome = "João";
            string expectedEmail = "joao@email.com";
            string expectedTelefone = "123456789";
            int expectedDDD = 11;

            // Act
            var contato = new Contatos(expectedId, expectedNome, expectedEmail, expectedTelefone, expectedDDD);

            // Assert
            Assert.Equal(expectedId, contato.IdContato);
            Assert.Equal(expectedNome, contato.Nome);
            Assert.Equal(expectedEmail, contato.Email);
            Assert.Equal(expectedTelefone, contato.Telefone);
            Assert.Equal(expectedDDD, contato.DDD);
        }
                
    }
}
