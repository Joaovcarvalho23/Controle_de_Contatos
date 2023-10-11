using Controle_de_Contatos.Data;
using Controle_de_Contatos.Models;

namespace Controle_de_Contatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoDeDadosContext _bancoDeDadosContext; //atributo
        
        public UsuarioRepositorio(BancoDeDadosContext bancoDeDadosContext) //construtor para fazermos a injeção 
        {
            _bancoDeDadosContext = bancoDeDadosContext;
        }


        //métodos
        public UsuarioModel BuscarPorId(int id)
        {
            return _bancoDeDadosContext.TabelaUsuarios.FirstOrDefault(o => o.Id == id);
        }



        public List<UsuarioModel> BuscarTodosUsuarios()
        {
            return _bancoDeDadosContext.TabelaUsuarios.ToList();
        }


        
        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataDeCadastro = DateTime.Now;
            _bancoDeDadosContext.TabelaUsuarios.Add(usuario);
            _bancoDeDadosContext.SaveChanges();

            return usuario;
        }



        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDB = BuscarPorId(usuario.Id);

            if(usuarioDB == null) { throw new Exception("Ocorreu um erro ao atualizar o usuário"); }

            usuarioDB.Name = usuario.Name;
            usuarioDB.Login = usuario.Login;
            usuarioDB.Email = usuario.Email;
            usuarioDB.DataAtualizacao = DateTime.Now;//pegamos a data atual e jogamos para dentro da coluna
            usuarioDB.Perfil = usuario.Perfil;

            _bancoDeDadosContext.TabelaUsuarios.Update(usuarioDB);
            _bancoDeDadosContext.SaveChanges();

            return usuarioDB;

        }


        
        public bool Deletar(int id)
        {
            UsuarioModel usuarioDB = BuscarPorId(id);

            if (usuarioDB == null)
                throw new Exception("Ocorreu um erro na exclusão do usuário");

            _bancoDeDadosContext.TabelaUsuarios.Remove(usuarioDB);
            _bancoDeDadosContext.SaveChanges();

            return true;
        }

        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoDeDadosContext.TabelaUsuarios.FirstOrDefault(
                o => o.Login.ToUpper() == login.ToUpper());
        }
    }
}
