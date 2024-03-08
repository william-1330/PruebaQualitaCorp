using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapaDatos;
using CapaDatos.DA;
using CapaDatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp;

namespace WebApp.Controllers
{
    public class FacturasController : Controller
    {
        private readonly ClientesController _clientesController;
        private readonly MeserosController _meserosController;
        private readonly MesasController _mesasController;

        public FacturasController(ClientesController clientesController, MeserosController meserosController, MesasController mesasController)
        {
            _clientesController = clientesController;
            _meserosController = meserosController;
            _mesasController = mesasController;
        }
        // GET: Facturas
        public async Task<IActionResult> Index()
        {
            return View(await ListaFacturas());
        }

        public async Task<IEnumerable<Factura>> ListaFacturas()
        {
            IEnumerable<Factura>? facturas = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/facturas/");
                //HTTP GET
                var responseTask = client.GetAsync("index");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IList<Factura>>();
                    readTask.Wait();
                    facturas = readTask.Result;
                }
                else
                {
                    facturas = Enumerable.Empty<Factura>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return facturas;
        }

        // GET: Facturas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Factura? factura = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/facturas/");
                //HTTP GET
                var responseTask = client.GetAsync($"Details?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Factura>();
                    readTask.Wait();
                    factura = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }
            return View(factura);
        }

        // GET: Facturas/Create
        public IActionResult Create()
        {
            FillCombo();
            return View();
        }

        // POST: Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NroFactura,IdCliente,NroMesa,IdMesero,Fecha")] Factura factura)
        {
            ModelState.Remove("Cliente");
            ModelState.Remove("Mesa");
            ModelState.Remove("Mesero");
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7137/api/facturas/");
                    //HTTP POST
                    var responseTask = client.PostAsJsonAsync("Create", factura);
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

            return View(factura);
        }

        // GET: Facturas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Factura factura = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/facturas/");
                //HTTP GET
                var responseTask = client.GetAsync($"Edit?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Factura>();
                    readTask.Wait();
                    factura = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }

            if (factura == null)
            {
                return NotFound();
            }

            FillCombo();

            return View(factura);
        }

        // POST: Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("NroFactura,IdCliente,NroMesa,IdMesero,Fecha")] Factura factura)
        {
            ModelState.Remove("Cliente");
            ModelState.Remove("Mesa");
            ModelState.Remove("Mesero");
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    //factura.Cliente = new Cliente();
                    //factura.Mesa = new Mesa();
                    //factura.Mesero = new Mesero();

                    client.BaseAddress = new Uri("https://localhost:7137/api/facturas/");
                    //HTTP POST
                    var responseTask = client.PostAsJsonAsync("Edit", factura);
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

            return View(factura);
        }

        // GET: Facturas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Factura factura = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/facturas/");
                //HTTP GET
                var responseTask = client.GetAsync($"Delete?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Factura>();
                    readTask.Wait();
                    factura = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }

            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/facturas/");
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
            IEnumerable<Cliente>? clientes = await _clientesController.ListaClientes();
            IEnumerable<Mesero>? meseros = await _meserosController.ListaMeseros();
            IEnumerable<Mesa>? mesas = await _mesasController.ListaMesas();

            ViewData["IdCliente"] = new SelectList(clientes, "IdCliente", "Apellidos");
            ViewData["NroMesa"] = new SelectList(mesas, "NroMesa", "Nombre");
            ViewData["IdMesero"] = new SelectList(meseros, "IdMesero", "Apellidos");
        }
    }
}
