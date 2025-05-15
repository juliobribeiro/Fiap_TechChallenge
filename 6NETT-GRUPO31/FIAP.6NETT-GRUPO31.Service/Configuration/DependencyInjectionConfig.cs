using FIAP._6NETT_GRUPO31.Application.Interfaces;
using FIAP._6NETT_GRUPO31.Application.Services;
using FIAP._6NETT_GRUPO31.Domain.Interfaces;
using FIAP._6NETT_GRUPO31.Infra.Data.Context;
using FIAP._6NETT_GRUPO31.Infra.Data.Repository;
using System.ComponentModel.DataAnnotations;

namespace FIAP._6NETT_GRUPO31.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {

            #region Context
            services.AddScoped<FIAPContext>();
            #endregion

            #region Repository
            //repository
            services.AddScoped<IContatoRepository, ContatoRepository>();            
            #endregion


            #region Application
            services.AddScoped<IContatoApplication, ContatoApplication>();            
            #endregion
        }
    }
}
