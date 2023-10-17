using Controle_de_Contatos.Models;

namespace Controle_de_Contatos.Helper
{
    public interface ISessao
    {
        //Criando contratos
        void CriarSessaoUsuario(UsuarioModel usuario);
        void RemoverSessaoUsuario();
        UsuarioModel BuscarSessaoUsuario();

    }
}
