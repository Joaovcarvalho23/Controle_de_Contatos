using Controle_de_Contatos.Models;

namespace Controle_de_Contatos.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel BuscarPorId(int id);

        List<UsuarioModel> BuscarTodosUsuarios();

        UsuarioModel Adicionar(UsuarioModel usuario);

        UsuarioModel Atualizar (UsuarioModel usuario);

        bool Deletar(int id);

        UsuarioModel BuscarPorLogin(string login);
    }
}
