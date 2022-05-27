using System.ComponentModel.DataAnnotations;

namespace TC.Usuarios.Application.ViewModels
{
    public class AdicionarUsuarioViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string PrimeiroNome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string UltimoNome { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]        
        public string Email { get; set; }        
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string DataNascimento { get; set; }
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Escolaridade { get; set; }
    }
}