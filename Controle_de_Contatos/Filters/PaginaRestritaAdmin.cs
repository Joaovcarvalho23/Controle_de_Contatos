using Controle_de_Contatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Text.Json;

namespace Controle_de_Contatos.Filters
{
    public class PaginaRestritaAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            string sessaoUsuario = context.HttpContext.Session.GetString("sessaoUsuarioLogado");

            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                //se a sessão do usuário for nula ou vazia, chama o resultado do nosso contexto vai ser um redirect para a rota da controller Login e da action Index
            }
            else
            {
                UsuarioModel usuario = JsonSerializer.Deserialize<UsuarioModel>(sessaoUsuario);

                //se por algum motivo ele não conseguiu serializar o objeto para o UsuarioModel
                if(usuario == null)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Login" }, { "action", "Index" } });
                }

                if(usuario.Perfil != Enums.PerfilEnum.Admin)
                {
                    context.Result = new RedirectToRouteResult(new RouteValueDictionary { { "controller", "Restrito" }, { "action", "Index" } });
                }
            }

            base.OnActionExecuting(context);
        }
    }
}
