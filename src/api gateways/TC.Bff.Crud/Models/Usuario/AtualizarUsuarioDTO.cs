using System.ComponentModel.DataAnnotations;

namespace TC.Bff.Crud.Models.Usuario
{
    public class AtualizarUsuarioDTO
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int Id { get; set; }
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
