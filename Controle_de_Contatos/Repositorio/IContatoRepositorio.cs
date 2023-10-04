using Controle_de_Contatos.Models;

namespace Controle_de_Contatos.Repositorio
{
    public interface IContatoRepositorio
    {
        ContatoModel Adicionar(ContatoModel contato);
    }
}
