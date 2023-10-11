using Controle_de_Contatos.Models;
using Controle_de_Contatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Contatos.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public LoginController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio; //fazendo a injeção de independência
        }

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
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorLogin(loginModel.Login);

                    if(usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        TempData["MensagemFalha"] = $"Senha incorreta. Por favor, tente novamente.";

                    }
                        

                    TempData["MensagemFalha"] = $"Usuário e/ou senha inválido(s). Por favor, tente novamente.";
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
