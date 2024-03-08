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
    public class ClientesController : Controller
    {
        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            return View(await ListaClientes());
        }

        public async Task<IEnumerable<Cliente>> ListaClientes()
        {
            IEnumerable<Cliente>? clientes = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/clientes/");
                //HTTP GET
                var responseTask = client.GetAsync("index");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IList<Cliente>>();
                    readTask.Wait();
                    clientes = readTask.Result;
                }
                else
                {
                    clientes = Enumerable.Empty<Cliente>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return clientes;
        }

        // GET: Clientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cliente? cliente = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/clientes/");
                //HTTP GET
                var responseTask = client.GetAsync($"Details?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Cliente>();
                    readTask.Wait();
                    cliente = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }

            return View(cliente);
        }

        // GET: Clientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clientes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCliente,Nombres,Apellidos,Direccion,Telefono")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7137/api/clientes/");
                    //HTTP POST
                    var responseTask = client.PostAsJsonAsync("Create", cliente);
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
            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cliente cliente = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/clientes/");
                //HTTP GET
                var responseTask = client.GetAsync($"Edit?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Cliente>();
                    readTask.Wait();
                    cliente = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdCliente,Nombres,Apellidos,Direccion,Telefono")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7137/api/clientes/");
                    //HTTP POST
                    var responseTask = client.PostAsJsonAsync("Edit", cliente);
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
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Cliente cliente = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/clientes/");
                //HTTP GET
                var responseTask = client.GetAsync($"Delete?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Cliente>();
                    readTask.Wait();
                    cliente = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/clientes/");
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
    }
}
