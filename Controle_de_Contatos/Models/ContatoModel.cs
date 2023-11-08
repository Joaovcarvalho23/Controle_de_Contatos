using System.ComponentModel.DataAnnotations;

namespace Controle_de_Contatos.Models
{
    public class ContatoModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Por favor, informe o nome do contato")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Por favor, informe o e-mail do contato")]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido!")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Por favor, informe o celular do contato")]
        [Phone(ErrorMessage = "Formato do celular está incorreto!")]
        public string? Celular { get; set;}

        public int? UsuarioId { get; set; }
    }
}