using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using CapaDatos;
using CapaDatos.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp;

namespace WebApp.Controllers
{
    public class MeserosController : Controller
    {
        // GET: Meseroes
        public async Task<IActionResult> Index()
        {
            return View(await ListaMeseros());
        }

        public async Task<IEnumerable<Mesero>> ListaMeseros()
        {
            IEnumerable<Mesero>? meseros = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/meseros/");
                //HTTP GET
                var responseTask = client.GetAsync("index");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IList<Mesero>>();
                    readTask.Wait();
                    meseros = readTask.Result;
                }
                else
                {
                    meseros = Enumerable.Empty<Mesero>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return meseros;
        }

        // GET: Meseroes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mesero? mesero = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/meseros/");
                //HTTP GET
                var responseTask = client.GetAsync($"Details?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Mesero>();
                    readTask.Wait();
                    mesero = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }

            if (mesero == null)
            {
                return NotFound();
            }

            return View(mesero);
        }

        // GET: Meseroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Meseroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdMesero,Nombres,Apellidos,Edad,Antiguedad")] Mesero mesero)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7137/api/meseros/");
                    //HTTP POST
                    var responseTask = client.PostAsJsonAsync("Create", mesero);
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
            return View(mesero);
        }

        // GET: Meseroes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mesero mesero = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/meseros/");
                //HTTP GET
                var responseTask = client.GetAsync($"Edit?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Mesero>();
                    readTask.Wait();
                    mesero = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }

            if (mesero == null)
            {
                return NotFound();
            }
            return View(mesero);
        }

        // POST: Meseroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdMesero,Nombres,Apellidos,Edad,Antiguedad")] Mesero mesero)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7137/api/meseros/");
                    //HTTP POST
                    var responseTask = client.PostAsJsonAsync("Edit", mesero);
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
            return View(mesero);
        }

        // GET: Meseroes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Mesero mesero = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/meseros/");
                //HTTP GET
                var responseTask = client.GetAsync($"Delete?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Mesero>();
                    readTask.Wait();
                    mesero = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }

            if (mesero == null)
            {
                return NotFound();
            }

            return View(mesero);
        }

        // POST: Meseroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/meseros/");
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
