using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapaDatos.DA;
using CapaDatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp;

namespace WebApp.Controllers
{
    public class DetalleXFacturasController : Controller
    {
        private readonly FacturasController _facturasController;
        private readonly SupervisoresController _supervisoresController;

        public DetalleXFacturasController(FacturasController facturasController , SupervisoresController supervisoresController)
        {
            _facturasController = facturasController;
            _supervisoresController = supervisoresController;
        }

        // GET: DetalleXFacturas
        public async Task<IActionResult> Index()
        {
            return View(await ListaDetalles());
        }

        public async Task<IEnumerable<DetalleXFactura>> ListaDetalles()
        {
            IEnumerable<DetalleXFactura>? detalleXFacturas = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/detalleXFacturas/");
                //HTTP GET
                var responseTask = client.GetAsync("index");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IList<DetalleXFactura>>();
                    readTask.Wait();
                    detalleXFacturas = readTask.Result;
                }
                else
                {
                    detalleXFacturas = Enumerable.Empty<DetalleXFactura>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return detalleXFacturas;
        }

        // GET: DetalleXFacturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            DetalleXFactura? detalleXFactura = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/detalleXFacturas/");
                //HTTP GET
                var responseTask = client.GetAsync($"Details?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<DetalleXFactura>();
                    readTask.Wait();
                    detalleXFactura = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }

            if (detalleXFactura == null)
            {
                return NotFound();
            }

            return View(detalleXFactura);
        }

        // GET: DetalleXFacturas/Create
        public IActionResult Create()
        {
            FillCombo();
            return View();
        }

        // POST: DetalleXFacturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdDetalleXFactura,NroFactura,IdSupervisor,Plato,Valor")] DetalleXFactura detalleXFactura)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7137/api/detalleXFacturas/");
                    //HTTP POST
                    var responseTask = client.PostAsJsonAsync("Create", detalleXFactura);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (!result.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return new EmptyResult();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            FillCombo();
            return View(detalleXFactura);
        }

        // GET: DetalleXFacturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DetalleXFactura detalleXFactura = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/detalleXFacturas/");
                //HTTP GET
                var responseTask = client.GetAsync($"Edit?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<DetalleXFactura>();
                    readTask.Wait();
                    detalleXFactura = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }

            if (detalleXFactura == null)
            {
                return NotFound();
            }
            FillCombo();
            return View(detalleXFactura);
        }

        // POST: DetalleXFacturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdDetalleXFactura,NroFactura,IdSupervisor,Plato,Valor")] DetalleXFactura detalleXFactura)
        {
            if (id != detalleXFactura.IdDetalleXFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7137/api/detalleXFacturas/");
                    //HTTP POST
                    var responseTask = client.PostAsJsonAsync("Edit", detalleXFactura);
                    responseTask.Wait();

                    var result = responseTask.Result;
                    if (!result.IsSuccessStatusCode)
                    {
                        ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                        return new EmptyResult();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            FillCombo();
            return View(detalleXFactura);
        }

        // GET: DetalleXFacturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            DetalleXFactura detalleXFactura = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/detalleXFacturas/");
                //HTTP GET
                var responseTask = client.GetAsync($"Delete?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<DetalleXFactura>();
                    readTask.Wait();
                    detalleXFactura = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }

            if (detalleXFactura == null)
            {
                return NotFound();
            }

            return View(detalleXFactura);
        }

        // POST: DetalleXFacturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/detalleXFacturas/");
                //HTTP GET
                var responseTask = client.PostAsJsonAsync("Delete", id);
                responseTask.Wait();
                var result = responseTask.Result;

                if (!result.IsSuccessStatusCode)
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }

            return RedirectToAction(nameof(Index));
        }

        private async void FillCombo()
        {
            IEnumerable<Factura>? facturas = await _facturasController.ListaFacturas();
            IEnumerable<Supervisor>? supervisores = await _supervisoresController.ListaSupervisores();

            ViewData["NroFactura"] = new SelectList(facturas, "NroFactura", "NroFactura");
            ViewData["IdSupervisor"] = new SelectList(supervisores, "IdSupervisor", "Apellidos");
        }
    }
}
