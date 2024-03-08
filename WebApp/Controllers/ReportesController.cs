using CapaDatos.DA;
using CapaDatos.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class ReportesController : Controller
    {
        public ActionResult Index()
        {
            return View("~/Views/Reportes/Index.cshtml");
        }

        [HttpGet]
        public async Task<IActionResult> TotalXMesero(int meses)
        {
            if (meses == 0)
                return View("~/Views/Reportes/TotalXMesero.cshtml");

            IEnumerable<TotalXMeseroViewModel> listaValores;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/reportes/");
                //HTTP GET
                var responseTask = client.GetAsync($"TotalXMesero?meses={meses}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IEnumerable<TotalXMeseroViewModel>>();
                    readTask.Wait();
                    listaValores = readTask.Result;
                    return View("~/Views/Reportes/TotalXMesero.cshtml", listaValores);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> CunsumoXCliente(int meses, double valor)
        {
            if (meses == 0)
                return View("~/Views/Reportes/ConsumoXCliente.cshtml");

            IEnumerable<ConsumoXClienteViewModel> listaValores;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/reportes/");
                //HTTP GET
                var responseTask = client.GetAsync($"CunsumoXCliente?meses={meses}&valor={valor}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IEnumerable<ConsumoXClienteViewModel>>();
                    readTask.Wait();
                    listaValores = readTask.Result;
                    return View("~/Views/Reportes/ConsumoXCliente.cshtml", listaValores);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }
        }

        [HttpGet]
        public async Task<IActionResult> PlatoMasVendido(int meses)
        {
            if (meses == 0)
                return View("~/Views/Reportes/PlatoMasVendido.cshtml");

            IEnumerable<PlatoMasVendidoViewModel> listaValores;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/reportes/");
                //HTTP GET
                var responseTask = client.GetAsync($"PlatoMasVendido?meses={meses}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IEnumerable<PlatoMasVendidoViewModel>>();
                    readTask.Wait();
                    listaValores = readTask.Result;
                    return View("~/Views/Reportes/PlatoMasVendido.cshtml", listaValores);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }
        }

    }
}
