using Controle_de_Contatos.Models;
using Controle_de_Contatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Contatos.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio; //fazendo injeção de dependêcia
        }


        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodosUsuarios();

            return View(usuarios);
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
            {

            }
        }
    }
}
