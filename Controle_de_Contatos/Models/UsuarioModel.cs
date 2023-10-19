using Controle_de_Contatos.Enums;
using Controle_de_Contatos.Helper;
using System.ComponentModel.DataAnnotations;

namespace Controle_de_Contatos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Digite o nome do usuário")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Digite o e-mail do usuário")]
        [EmailAddress(ErrorMessage = "O formato de e-mail informado não é válido!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Digite o e-mail do usuário")]
        public string? Login { get; set;}

        [Required(ErrorMessage = "Informe o perfil do usuário!")]
        public PerfilEnum Perfil { get; set; }

        [Required(ErrorMessage = "Digite a senha do usuário")]
        public string? Senha { get; set;}
        public DateTime DataDeCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha;
        }

        public void SenhaHash()
        {
            Senha = Senha.GerarHash(); //o this criado no método GerarHash serve justamente para nos deixar usar esse método. Se torna um método de extensão
        }
    }
}
