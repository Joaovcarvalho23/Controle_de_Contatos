using Controle_de_Contatos.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Controle_de_Contatos.Helper
{
    public class Sessao : ISessao
    {
        //atributo para conseguir criar sessões do usuário
        private readonly IHttpContextAccessor _httpContext;

        //construtor para fazer a injeção de independência
        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public UsuarioModel BuscarSessaoUsuario()
        {
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario))
                return null;

            return JsonSerializer.Deserialize<UsuarioModel>(sessaoUsuario);
        }

        public void CriarSessaoUsuario(UsuarioModel usuario)
        {
            string valor = JsonSerializer.Serialize(usuario);//comando para transformar esse objeto em uma string serializada

            _httpContext.HttpContext.Session.SetString("sessaoUsuarioLogado", valor);//é do tipo chave-valor. A chave é "sessaoUsuarioLogado". E o valor é o valor que queremos guardar dentro dessa sessão/ os dados do usuário. Precisamos transformar o objeto 'usuario' numa String para converter num padrão json 
        }

        public void RemoverSessaoUsuario()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }
    }
}
