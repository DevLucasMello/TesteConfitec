using AutoMapper;
using System;
using TC.Usuarios.Application.Commands;
using TC.Usuarios.Application.ViewModels;
using TC.Core.DomainObjects;

namespace TC.Usuarios.Application.AutoMapper
{
    public class AtualizarUsuarioCommandToViewModel : Profile
    {
        public AtualizarUsuarioCommandToViewModel()
        {
            CreateMap<AtualizarUsuarioCommand, AtualizarUsuarioViewModel>()
                .ForMember(n => n.PrimeiroNome, c => c.MapFrom(c => c.Nome.PrimeiroNome))
                .ForMember(n => n.UltimoNome, c => c.MapFrom(c => c.Nome.UltimoNome));
        }
    }

    public class ViewModelToAtualizarUsuarioCommand : Profile
    {
        public ViewModelToAtualizarUsuarioCommand()
        {
            CreateMap<AtualizarUsuarioViewModel, AtualizarUsuarioCommand>()
                .ConstructUsing(c => new AtualizarUsuarioCommand(c.Id, new Nome(c.PrimeiroNome, c.UltimoNome), c.Email, Convert.ToDateTime(c.DataNascimento), 
                (Escolaridade)Enum.Parse(typeof(Escolaridade), c.Escolaridade)));
        }
    }
}
