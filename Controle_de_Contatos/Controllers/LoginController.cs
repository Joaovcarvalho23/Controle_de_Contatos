using Controle_de_Contatos.Helper;
using Controle_de_Contatos.Models;
using Controle_de_Contatos.Repositorio;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;

namespace Controle_de_Contatos.Controllers
{
    public class LoginController : Controller
    {

        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;
        private readonly IEmail _email;

        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao, IEmail email)
        {
            _usuarioRepositorio = usuarioRepositorio; //fazendo a injeção de independência
            _sessao = sessao;
            _email = email;
        }

        public IActionResult Index()
        {
            //Se o usuáruio estiver logado, redirecionar para a home
            if (_sessao.BuscarSessaoUsuario() != null)
                return RedirectToAction("Index", "Home");


            return View();
        }

        public IActionResult RedefinirSenha()
        {
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

        public IActionResult CriarUsuario()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CriarUsuario(UsuarioModel usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    usuario = _usuarioRepositorio.Adicionar(usuario);

                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(usuario);

            }
            catch (Exception erro)
            {
                TempData["MensagemFalha"] = $"Ocorreu um erro ao criar este usuário. Detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult EnviarLinkRedefinirSenha(RedefinirSenhaModel redefinirSenhaModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuario = _usuarioRepositorio.BuscarPorEmailELogin(redefinirSenhaModel.Email, redefinirSenhaModel.Login);

                    if (usuario != null)
                    {
                        string novaSenha = usuario.GerarNovaSenha();
                        string mensagem = $"Sua nova senha é: {novaSenha}";

                        bool emailEnviado = _email.Enviar(usuario.Email, "Sistema de Contatos - Nova Senha", mensagem);

                        if (emailEnviado)
                        {
                            _usuarioRepositorio.Atualizar(usuario);
                            TempData["MensagemSucesso"] = $"Enviamos para o seu e-mail de cadastro uma nova senha.";
                        }
                        else
                        {
                            TempData["MensagemFalha"] = $"Não conseguimos enviar o e-mail. Por favor, tente novamente.";
                        }


                        return RedirectToAction("Index", "Login");
                    }


                    TempData["MensagemFalha"] = $"Não conseguimos redefinir sua senha. Por favor, verifique os dados informados.";
                }

                return View("Index");

            }
            catch (SmtpException smtpEx)
            {
                // Trate as exceções específicas de SMTP aqui, registre informações detalhadas para depuração.
                TempData["MensagemFalha"] = $"Erro SMTP: {smtpEx.Message}";
                return RedirectToAction("Index");
            }

            catch (Exception ex)
            {
                TempData["MensagemFalha"] = $"Houve um erro ao tentar redefinir sua senha. Descrição do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }

    }
}
