using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TC.Usuarios.Domain;
using TC.Usuarios.Infra.Data;
using TC.Usuarios.Infra.Data.Repository;
using TC.Core.Mediator;

namespace TC.Usuarios.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            // Config Context
            services.AddDbContext<UsuariosContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // API
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            
            // Application
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            // Data
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<UsuariosContext>();            
        }
    }
}