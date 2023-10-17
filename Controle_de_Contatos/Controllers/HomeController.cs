using Controle_de_Contatos.Filters;
using Controle_de_Contatos.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Controle_de_Contatos.Controllers
{
    [PaginaUsuarioLogado] //adicionando o filtro que criamos dentro da nossa controller HomeController
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}