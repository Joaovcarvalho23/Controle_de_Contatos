using Controle_de_Contatos.Data;
using Controle_de_Contatos.Models;

namespace Controle_de_Contatos.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoDeDadosContext _bancoDeDadosContext; //atributo
        
        public UsuarioRepositorio(BancoDeDadosContext bancoDeDadosContext) //construtor para fazermos a injeção 
        {
            this._bancoDeDadosContext = bancoDeDadosContext;
        }


        //Métodos
        public UsuarioModel BuscarPorLogin(string login)
        {
            return _bancoDeDadosContext.TabelaUsuarios.FirstOrDefault(o => o.Login.ToUpper() == login.ToUpper());
        }


        public UsuarioModel BuscarPorEmailELogin(string email, string login)
        {
            return _bancoDeDadosContext.TabelaUsuarios.FirstOrDefault(
                o => o.Email.ToUpper() == email.ToUpper() && o.Login.ToUpper() == login.ToUpper());
        }


        public List<UsuarioModel> BuscarTodosUsuarios()
        {
            return _bancoDeDadosContext.TabelaUsuarios.ToList();
        }


        public UsuarioModel BuscarPorId(int id)
        {
            return _bancoDeDadosContext.TabelaUsuarios.FirstOrDefault(o => o.Id == id);
        }

        
        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataDeCadastro = DateTime.Now;
            usuario.SenhaHash();
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

        public UsuarioModel AlterarSenha(AlterarSenhaModel alterarSenhaModel)
        {
            UsuarioModel usuarioDB = BuscarPorId(alterarSenhaModel.Id);

            if (usuarioDB == null) //caso o usuário não seja encontrado
                throw new Exception("Houve um erro na atualização da senha. Usuário não encontrado!");

            if (!usuarioDB.SenhaValida(alterarSenhaModel.SenhaAtual)) //caso a senha atual está inválida
                throw new Exception("Senha atual não confere");

            if (usuarioDB.SenhaValida(alterarSenhaModel.NovaSenha)) //caso coloque a nova senha igual a senha atual
                throw new Exception("Nova senha deve ser diferente da atual");

            usuarioDB.SetNovaSenha(alterarSenhaModel.NovaSenha);
            usuarioDB.DataAtualizacao = DateTime.Now;

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
    }
}