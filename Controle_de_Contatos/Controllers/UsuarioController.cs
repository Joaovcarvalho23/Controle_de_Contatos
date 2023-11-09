using Controle_de_Contatos.Filters;
using Controle_de_Contatos.Models;
using Controle_de_Contatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Contatos.Controllers
{
    [PaginaRestritaAdmin]
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly IContatoRepositorio _contatoRepositorio;

        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, IContatoRepositorio contatoRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio; //fazendo injeção de dependêcia
            _contatoRepositorio = contatoRepositorio;
        }


        public IActionResult Index()
        {
            List<UsuarioModel> usuarios = _usuarioRepositorio.BuscarTodosUsuarios();
            return View(usuarios);
        }



        public IActionResult ListarContatosPorUsuarioId(int id)
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodosContatos(id);

            return PartialView("_ContatosUsuario", contatos);
        } //quando a nossa requisição ajax bater nesse método, esse método vai buscar os contatos lá no banco de dados, vai retornar uma PartialView (um pequeno código html) e vai retornar para o nosso JS já com os dados preenchidos desse contato. Continua explicação lá no site.js...



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



        public IActionResult ExcluirUsuarioConfirmacao(int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
            return View(usuario);
        }

        public IActionResult ExcluirUsuario(int id)
        {
            try
            {
                _usuarioRepositorio.Deletar(id);
                TempData["MensagemSucesso"] = "Usuário excluído com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                TempData["MensagemFalha"] = $"Ocorreu um erro ao excluir o usuário. Detalhe do erro: {e.Message}";
                return RedirectToAction("Index");
            }
        }



        public IActionResult EditarUsuario (int id)
        {
            UsuarioModel usuario = _usuarioRepositorio.BuscarPorId(id);
            return View(usuario);
        }



        [HttpPost]
        public IActionResult EditarUsuario (UsuarioSemSenhaModel usuarioSemSenha)
        {
            try
            {
                UsuarioModel? usuario = null;

                if (ModelState.IsValid)
                {
                    usuario = new UsuarioModel()
                    {
                        Id = usuarioSemSenha.Id,
                        Name = usuarioSemSenha.Name,
                        Login = usuarioSemSenha.Login,
                        Email = usuarioSemSenha.Email,
                        Perfil = (Enums.PerfilEnum)usuarioSemSenha.Perfil
                    };


                    usuario = _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuário alterado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(usuario);

            }
            catch (Exception e)
            {
                TempData["MensagemFalha"] = $"Ocorreu um erro ao editar o usuário. Tente novamente. Detalhe do erro: {e.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
