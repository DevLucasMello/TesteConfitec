using AutoMapper;
using System;
using TC.Usuarios.Application.Commands;
using TC.Usuarios.Application.ViewModels;
using TC.Core.DomainObjects;

namespace TC.Usuarios.Application.AutoMapper
{
    public class AdicionarUsuarioCommandToViewModel : Profile
    {
        public AdicionarUsuarioCommandToViewModel()
        {
            CreateMap<AdicionarUsuarioCommand, AdicionarUsuarioViewModel>()
                .ForMember(n => n.PrimeiroNome, c => c.MapFrom(c => c.Nome.PrimeiroNome))
                .ForMember(n => n.UltimoNome, c => c.MapFrom(c => c.Nome.UltimoNome));
        }
    }

    public class ViewModelToAdicionarUsuarioCommand : Profile
    {
        public ViewModelToAdicionarUsuarioCommand()
        {
            CreateMap<AdicionarUsuarioViewModel, AdicionarUsuarioCommand>()
                .ConstructUsing(c => new AdicionarUsuarioCommand(new Nome(c.PrimeiroNome, c.UltimoNome), c.Email, Convert.ToDateTime(c.DataNascimento), 
                (Escolaridade)Enum.Parse(typeof(Escolaridade), c.Escolaridade)));
        }
    }
}