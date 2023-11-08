using Controle_de_Contatos.Models;

namespace Controle_de_Contatos.Repositorio
{
    public interface IContatoRepositorio
    {
        ContatoModel BuscarPorId(int id);

        List<ContatoModel> BuscarTodosContatos(int usuarioId);

        ContatoModel Adicionar(ContatoModel contato);

        ContatoModel Atualizar(ContatoModel contato);

        bool Deletar(int id);
    }
}
