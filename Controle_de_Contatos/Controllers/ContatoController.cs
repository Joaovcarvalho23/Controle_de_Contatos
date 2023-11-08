using Controle_de_Contatos.Filters;
using Controle_de_Contatos.Helper;
using Controle_de_Contatos.Models;
using Controle_de_Contatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Contatos.Controllers
{
    [PaginaUsuarioLogado] //adicionando o filtro que criamos dentro da nossa controller ContatoController
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;
        private readonly ISessao _sessao;


        public ContatoController(IContatoRepositorio contatoRepositorio, ISessao sessao)
        {
            _contatoRepositorio = contatoRepositorio; //fazendo injeção de dependêcia
            _sessao = sessao;
        }


        public IActionResult Index()
        {
            UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodosContatos(usuarioLogado.Id);

            return View(contatos);
        }


        public IActionResult CriarContato()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CriarContato(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;

                    _contatoRepositorio.Adicionar(contato);
                    TempData["MensagemSucesso"] = "Contato cadastrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(contato);
            }
            catch (Exception ex)
            {
                TempData["MensagemFalha"] = $"Erro ao cadastrar contato. Detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }



        public IActionResult EditarContato(int id)
        {
            ContatoModel contato = _contatoRepositorio.BuscarPorId(id);
            return View(contato);
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UsuarioModel usuarioLogado = _sessao.BuscarSessaoUsuario();
                    contato.UsuarioId = usuarioLogado.Id;

                    contato = _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato aletrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(contato);
            }
            catch (Exception ex)
            {
                TempData["MensagemFalha"] = $"Ocorreu um erro ao editar o contato :( Detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }



        public IActionResult ExcluirContatoConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.BuscarPorId(id);
            return View(contato);
        }

        public IActionResult ExcluirContato(int id)
        {
            try
            {
                _contatoRepositorio.Deletar(id);
                TempData["MensagemSucesso"] = "Contato excluído com sucesso!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["MensagemFalha"] = $"Ocorreu um erro ao deletar o contato. Detalhe do erro: {ex.Message}";
                return RedirectToAction("Index");
            }
        }
    }
}
