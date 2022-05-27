namespace TC.Bff.Crud.Models.Usuario
{
    public class UsuarioDTO
    {
        public int Id { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string Email { get; set; }
        public string DataNascimento { get; set; }
        public string Escolaridade { get; set; }
    }
}
