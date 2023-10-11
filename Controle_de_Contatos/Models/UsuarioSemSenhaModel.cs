using Controle_de_Contatos.Enums;
using System.ComponentModel.DataAnnotations;

namespace Controle_de_Contatos.Models
{
    public class UsuarioSemSenhaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do usuário")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Digite o e-mail do usuário")]
        [EmailAddress(ErrorMessage = "O formato de e-mail informado não é válido!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Digite o login do usuário")]
        public string? Login { get; set;}

        [Required(ErrorMessage = "Informe o perfil do usuário!")]
        public PerfilEnum? Perfil { get; set; }
    }
}
