using Controle_de_Contatos.Models;
using Controle_de_Contatos.Repositorio;
using Microsoft.AspNetCore.Mvc;

namespace Controle_de_Contatos.Controllers
{
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;


        public ContatoController(IContatoRepositorio contatoRepositorio)
        {
            _contatoRepositorio = contatoRepositorio; //fazendo injeção de dependêcia
        }


        public IActionResult Index()
        {
            List<ContatoModel> contatos = _contatoRepositorio.BuscarTodosContatos();

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
                    _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato aletrado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View("EditarContato", contato);
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
