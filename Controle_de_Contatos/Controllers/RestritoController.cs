using Controle_de_Contatos.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Contatos.Controllers
{
    [PaginaUsuarioLogado]
    public class RestritoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
