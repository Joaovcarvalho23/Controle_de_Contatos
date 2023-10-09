using Controle_de_Contatos.Models;
using Microsoft.EntityFrameworkCore;

namespace Controle_de_Contatos.Data
{
    public class BancoDeDadosContext : DbContext
    {
        public BancoDeDadosContext(DbContextOptions<BancoDeDadosContext> options) 
            : base(options) //o base() vai servir para passar a informação que vai vir do options para dentro do DbContext, passando base(options)
        {
        }

        public DbSet<ContatoModel> TabelaContatos { get; set; }

        public DbSet<UsuarioModel> TabelaUsuarios {  get; set; }
    }
}
