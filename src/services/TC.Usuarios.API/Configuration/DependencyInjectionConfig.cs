using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TC.Usuarios.Domain;
using TC.Usuarios.Infra.Data;
using TC.Usuarios.Infra.Data.Repository;
using TC.Core.Mediator;
using TC.Usuarios.Application.Commands;
using FluentValidation.Results;
using MediatR;
using TC.Usuarios.Application.Events;
using TC.Usuarios.Application.Queries;
using TC.Usuarios.Application.AutoMapper;

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

            // Commands
            services.AddScoped<IRequestHandler<AdicionarUsuarioCommand, ValidationResult>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarUsuarioCommand, ValidationResult>, UsuarioCommandHandler>();
            services.AddScoped<IRequestHandler<ExcluirUsuarioCommand, ValidationResult>, UsuarioCommandHandler>();

            // Events
            services.AddScoped<INotificationHandler<UsuarioRegistradoEvent>, UsuarioEventHandler>();

            // Application
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IUsuarioQueries, UsuarioQueries>();

            // Data
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<UsuariosContext>();

            // AutoMapper
            services.AddAutoMapper(typeof(AdicionarUsuarioCommandToViewModel), typeof(ViewModelToAdicionarUsuarioCommand));
            services.AddAutoMapper(typeof(AtualizarUsuarioCommandToViewModel), typeof(ViewModelToAtualizarUsuarioCommand));
            services.AddAutoMapper(typeof(ExibirUsuarioQuerieToViewModel), typeof(ViewModelToExibirUsuarioQuerie));
        }
    }
}