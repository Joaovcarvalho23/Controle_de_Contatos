using Controle_de_Contatos.Models;

namespace Controle_de_Contatos.Repositorio
{
    public interface IUsuarioRepositorio
    {
        UsuarioModel BuscarPorLogin(string login);

        UsuarioModel BuscarPorEmailELogin(string email, string login);

        List<UsuarioModel> BuscarTodosUsuarios();

        UsuarioModel BuscarPorId(int id);

        UsuarioModel Adicionar(UsuarioModel usuario);

        UsuarioModel Atualizar (UsuarioModel usuario);

        UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel);

        bool Deletar(int id);
    }
}
