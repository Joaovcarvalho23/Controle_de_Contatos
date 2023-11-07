using System.ComponentModel.DataAnnotations;

namespace Controle_de_Contatos.Models
{
    public class AlterarSenhaModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite a senha atual do usuário")]
        public string? SenhaAtual { get; set; }

        [Required(ErrorMessage = "Digite a nova senha do usuário")]
        public string? NovaSenha {  get; set; }

        [Required(ErrorMessage = "Confirmar nova senha do usuário")]
        [Compare("NovaSenha", ErrorMessage = "Senha não confere com a nova senha")]
        public string? ConfirmarNovaSenha { get; set; }
    }
}
