using FIAP._6NETT_GRUPO31.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAP._6NETT_GRUPO31.API.Tests.Model
{
    public class ContatoModelTests
    {
        [Fact]
        public void ContatoModel_Properties_ShouldSetAndGetValuesCorrectly()
        {
            // Arrange
            int expectedIdContato = 1;
            string expectedNome = "João";
            string expectedEmail = "joao@email.com";
            string expectedTelefone = "123456789";
            int expectedDDD = 11;

            // Act
            var contatoModel = new ContatoModel
            {
                IdContato = expectedIdContato,
                Nome = expectedNome,
                Email = expectedEmail,
                Telefone = expectedTelefone,
                DDD = expectedDDD
            };

            // Assert
            Assert.Equal(expectedIdContato, contatoModel.IdContato);
            Assert.Equal(expectedNome, contatoModel.Nome);
            Assert.Equal(expectedEmail, contatoModel.Email);
            Assert.Equal(expectedTelefone, contatoModel.Telefone);
            Assert.Equal(expectedDDD, contatoModel.DDD);
        }

        [Fact]
        public void ContatoModel_DefaultConstructor_ShouldInitializePropertiesWithDefaultValues()
        {
            // Act
            var contatoModel = new ContatoModel();

            // Assert
            Assert.Equal(0, contatoModel.IdContato);  
            Assert.Null(contatoModel.Nome);          
            Assert.Null(contatoModel.Email);
            Assert.Null(contatoModel.Telefone);
            Assert.Equal(0, contatoModel.DDD);        
        }
    }
}
