using AutoMapper;
using System;
using TC.Usuarios.Application.ViewModels;
using TC.Usuarios.Domain;
using TC.Core.DomainObjects;

namespace TC.Usuarios.Application.AutoMapper
{
    public class ExibirUsuarioQuerieToViewModel : Profile
    {
        public ExibirUsuarioQuerieToViewModel()
        {
            CreateMap<Usuario, ExibirUsuarioViewModel>()
                .ForMember(n => n.PrimeiroNome, c => c.MapFrom(c => c.Nome.PrimeiroNome))
                .ForMember(n => n.UltimoNome, c => c.MapFrom(c => c.Nome.UltimoNome));
        }
    }

    public class ViewModelToExibirUsuarioQuerie : Profile
    {
        public ViewModelToExibirUsuarioQuerie()
        {
            CreateMap<ExibirUsuarioViewModel, Usuario>()
                .ConstructUsing(c => new Usuario(new Nome(c.PrimeiroNome, c.UltimoNome), 
                c.Email, Convert.ToDateTime(c.DataNascimento), (Escolaridade)Enum.Parse(typeof(Escolaridade), c.Escolaridade)));
        }
    }
}
