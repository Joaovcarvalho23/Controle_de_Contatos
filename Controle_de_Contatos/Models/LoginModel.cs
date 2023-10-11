using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Controle_de_Contatos.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Por favor, informe o login")]
        public string? Login {  get; set; }

        [Required(ErrorMessage = "Por favor, informe a senha")]
        public string? Senha { get; set; }
    }
}
