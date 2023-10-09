using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Contatos.Controllers
{
    public class UsuarioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
