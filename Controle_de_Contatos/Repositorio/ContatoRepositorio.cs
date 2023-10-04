using Controle_de_Contatos.Data;
using Controle_de_Contatos.Models;

namespace Controle_de_Contatos.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {
        private readonly BancoDeDadosContext _bancoDeDadosContext; //atributo para poder fazer a injeção

        public ContatoRepositorio(BancoDeDadosContext bancoDeDadosContext) //construtor
        {
            _bancoDeDadosContext = bancoDeDadosContext;
        }

        public List<ContatoModel> BuscarTodosContatos()
        {
            return _bancoDeDadosContext.TabelaContatos.ToList();
        }


        public ContatoModel Adicionar(ContatoModel contato) //gravar no banco de dados. Porém, quem vai gravar propriamente dito é o contexto, que está dentor de Data. Então, esse contexto deve ser injetado para dentro do ContatoRepositorio 
        {
            _bancoDeDadosContext.TabelaContatos.Add(contato);
            _bancoDeDadosContext.SaveChanges();

            return contato;
        }
    }
}
