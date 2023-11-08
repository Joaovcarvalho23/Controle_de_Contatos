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

        public virtual List<ContatoModel>? Contatos { get; set; }  //criamos a coluna "UsuarioId". Ou seja, um contato é amarrado a um usuário. Porém, a partir do momento que temos um relacionamento com a tabela 'Contato', ela passa a ter uma lista. Um usuário tem x contatos. 

        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();//chamamos o método de GerarHash na senha em que o usuário está digitando, pois no banco de dados, a senha cadastrada se transformou em hash. Com isso, devemos transformar também a senha que o usuário está digitando em hash. Se o hash criado for o mesmo que está cadastrado no banco de dados, o login é efetivado
        }

        public void SenhaHash()
        {
            Senha = Senha.GerarHash(); //o this criado no método GerarHash serve justamente para nos deixar usar esse método. Se torna um método de extensão
        }

        public void SetNovaSenha(string novaSenha)
        {
            Senha = novaSenha.GerarHash();
        }

        public string GerarNovaSenha()
        {
            string novaSenha = Guid.NewGuid().ToString().Substring(0, 8); //pegamos apenas os 8 primeiros caracteres
            Senha = novaSenha.GerarHash();
            return novaSenha;
        }
    }
}
