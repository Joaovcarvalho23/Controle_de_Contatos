using Controle_de_Contatos.Models;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Contatos.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    return RedirectToAction("Index", "Home");
                }

                return View("Index");

            }
            catch (Exception ex)
            {
                TempData["MensagemFalha"] = $"Houve um erro ao tentar efetuar o login. Descrição do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
