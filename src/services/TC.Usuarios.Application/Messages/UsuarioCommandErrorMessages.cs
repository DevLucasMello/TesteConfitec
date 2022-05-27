namespace TC.Usuarios.Application.Messages
{
    public static class UsuarioCommandErrorMessages
    {
        public static string IdNuloErroMsg => "O Id não foi informado";
        public static string PrimeiroNomeNuloErroMsg => "O primeiro nome não foi informado";
        public static string UltimoNomeNuloErroMsg => "O último nome não foi informado";
        public static string EmailNuloErroMsg => "O email não foi informado";
        public static string DataNascimentoNuloErroMsg => "A data de nascimento deve ser informada";
        public static string EscolaridadeNuloErroMsg => "A escolaridade deve ser informada";
        public static string PrimeiroNomeQtdErroMsg => "O campo nome deve ter entre 3 e 150 caracteres";
        public static string UltimoNomeQtdErroMsg => "O campo nome deve ter entre 3 e 150 caracteres";
        public static string EmailInvalidoErroMsg => "endereço de email inválido";
        public static string DataNascimentoMaiorDataAtualErroMsg => "A data de nascimento deve ser inferior a data atual";
        public static string UsuarioNaoEncontradoErroMsg => "Usuário não encontrado.";
        public static string EscolaridadeNaoPermitidaErroMsg => "Escolaridade não permitida";
    }
}