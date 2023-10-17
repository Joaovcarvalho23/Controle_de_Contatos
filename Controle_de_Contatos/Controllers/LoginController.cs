using Controle_de_Contatos.Helper;
using Controle_de_Contatos.Models;
using Controle_de_Contatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Contatos.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio; //fazendo a injeção de independência
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            //Se o usuáruio estiver logado, redirecionar para a home
            if (_sessao.BuscarSessaoUsuario() != null)
                return RedirectToAction("Index", "Home");


            return View();
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();

            return RedirectToAction("Index", "Login");
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
                            _sessao.CriarSessaoUsuario(usuario);
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
