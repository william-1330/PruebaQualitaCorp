using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
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
    public class MesasController : Controller
    {
        // GET: Mesas
        public async Task<IActionResult> Index()
        {
            return View(await ListaMesas());
        }

        public async Task<IEnumerable<Mesa>> ListaMesas()
        {
            IEnumerable<Mesa>? mesas = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/mesas/");
                //HTTP GET
                var responseTask = client.GetAsync("index");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IList<Mesa>>();
                    readTask.Wait();
                    mesas = readTask.Result;
                }
                else
                {
                    mesas = Enumerable.Empty<Mesa>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return mesas;
        }

        // GET: Mesas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mesa? mesa = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/mesas/");
                //HTTP GET
                var responseTask = client.GetAsync($"Details?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Mesa>();
                    readTask.Wait();
                    mesa = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }

            if (mesa == null)
            {
                return NotFound();
            }

            return View(mesa);
        }

        // GET: Mesas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Mesas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NroMesa,Nombre,Reservada,Puestos")] Mesa mesa)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7137/api/mesas/");
                    //HTTP POST
                    var responseTask = client.PostAsJsonAsync("Create", mesa);
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
            return View(mesa);
        }

        // GET: Mesas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mesa mesa = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/mesas/");
                //HTTP GET
                var responseTask = client.GetAsync($"Edit?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Mesa>();
                    readTask.Wait();
                    mesa = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }

            if (mesa == null)
            {
                return NotFound();
            }
            return View(mesa);
        }

        // POST: Mesas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("NroMesa,Nombre,Reservada,Puestos")] Mesa mesa)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7137/api/mesas/");
                    //HTTP POST
                    var responseTask = client.PostAsJsonAsync("Edit", mesa);
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
            return View(mesa);
        }

        // GET: Mesas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mesa mesa = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/mesas/");
                //HTTP GET
                var responseTask = client.GetAsync($"Delete?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Mesa>();
                    readTask.Wait();
                    mesa = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }

            if (mesa == null)
            {
                return NotFound();
            }

            return View(mesa);
        }

        // POST: Mesas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/mesas/");
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
