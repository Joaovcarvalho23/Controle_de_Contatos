using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Contatos.Controllers
{
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CriarContato()
        {
            return View();
        }

        public IActionResult EditarContato()
        {
            return View();
        }

        public IActionResult ExcluirContatoConfirmacao()
        {
            return View();
        }
    }
}
