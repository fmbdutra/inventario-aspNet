using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HelloWorldC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HelloWorldC.Controllers
{
    public class InventarioController : Controller
    {
        public IActionResult Index()
        {
            using (PecaModel model = new PecaModel())
            {
                List<Peca> lista = model.Read();
                return View(lista);

            }
        }

        [HttpGet]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Cadastrar(IFormCollection form)
        {
            Peca peca = new Peca();

            peca.nome = form["nome"];
            peca.fabricante = form["fabricante"];

            peca.quantidade = int.Parse(form["quantidade"]);

            string preco = form["preco"].ToString().Replace(",", ".");

            NumberFormatInfo provider = new NumberFormatInfo();
            provider.NumberDecimalSeparator = ".";
            peca.preco = Convert.ToDouble(preco, provider);

            peca.date = DateTime.Now;

            using (PecaModel model = new PecaModel())
            {
                model.Create(peca);
                return RedirectToAction("Index");
            }

        }
    }
}