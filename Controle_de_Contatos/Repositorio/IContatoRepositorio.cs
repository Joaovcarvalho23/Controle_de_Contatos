using Controle_de_Contatos.Models;

namespace Controle_de_Contatos.Repositorio
{
    public interface IContatoRepositorio
    {
        List<ContatoModel> BuscarTodosContatos();

        ContatoModel Adicionar(ContatoModel contato);
    }
}
