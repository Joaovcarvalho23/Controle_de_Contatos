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
            if (ModelState.IsValid)
            {
                _contatoRepositorio.Adicionar(contato);
                return RedirectToAction("Index");
            }

            return View(contato);
        }



        public IActionResult EditarContato(int id)
        {
            ContatoModel contato = _contatoRepositorio.BuscarPorId(id);
            return View(contato);
        }

        [HttpPost]
        public IActionResult Alterar(ContatoModel contato)
        {
            _contatoRepositorio.Atualizar(contato);

            return RedirectToAction("Index");
        }



        public IActionResult ExcluirContatoConfirmacao(int id)
        {
            ContatoModel contato = _contatoRepositorio.BuscarPorId(id);
            return View(contato);
        }

        public IActionResult ExcluirContato(int id)
        {
            _contatoRepositorio.Deletar(id);

            return RedirectToAction("Index");
        }
    }
}
