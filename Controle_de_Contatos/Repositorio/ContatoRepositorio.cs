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

        //Métodos
        public ContatoModel BuscarPorId(int id)
        {
            return _bancoDeDadosContext.TabelaContatos.FirstOrDefault(o => o.Id == id);
        }


        public List<ContatoModel> BuscarTodosContatos(int usuarioId)
        {
            return _bancoDeDadosContext.TabelaContatos.Where(o => o.UsuarioId == usuarioId).ToList();
        }


        public ContatoModel Adicionar(ContatoModel contato) //gravar no banco de dados. Porém, quem vai gravar propriamente dito é o contexto, que está dentor de Data. Então, esse contexto deve ser injetado para dentro do ContatoRepositorio 
        {
            _bancoDeDadosContext.TabelaContatos.Add(contato);
            _bancoDeDadosContext.SaveChanges();

            return contato;
        }

        public ContatoModel Atualizar(ContatoModel contato)
        {
            ContatoModel contatoDB = BuscarPorId(contato.Id);

            if (contatoDB == null)
                throw new Exception("Ocorreu um erro na atualização do contato");

            contatoDB.Name = contato.Name;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;

            _bancoDeDadosContext.TabelaContatos.Update(contatoDB);
            _bancoDeDadosContext.SaveChanges();

            return contatoDB;
        }

        public bool Deletar(int id)
        {
            ContatoModel contatoDB = BuscarPorId(id);

            if (contatoDB == null)
                throw new Exception("Ocorreu um erro na exclusão do contato");

            _bancoDeDadosContext.TabelaContatos.Remove(contatoDB);
            _bancoDeDadosContext.SaveChanges();

            return true;
        }
    }
}
