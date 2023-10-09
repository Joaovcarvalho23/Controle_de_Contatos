using Controle_de_Contatos.Enums;

namespace Controle_de_Contatos.Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Login { get; set;}
        public PerfilEnum Perfil { get; set; }
    }
}
