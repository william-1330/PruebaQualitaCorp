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
    public class SupervisoresController : Controller
    {
        // GET: Supervisores
        public async Task<IActionResult> Index()
        {
            return View(await ListaSupervisores());
        }

        public async Task<IEnumerable<Supervisor>> ListaSupervisores()
        {
            IEnumerable<Supervisor>? supervisores = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/supervisores/");
                //HTTP GET
                var responseTask = client.GetAsync("index");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<IList<Supervisor>>();
                    readTask.Wait();
                    supervisores = readTask.Result;
                }
                else
                {
                    supervisores = Enumerable.Empty<Supervisor>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return supervisores;
        }

        // GET: Supervisores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Supervisor? supervisor = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/supervisores/");
                //HTTP GET
                var responseTask = client.GetAsync($"Details?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Supervisor>();
                    readTask.Wait();
                    supervisor = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }

            if (supervisor == null)
            {
                return NotFound();
            }

            return View(supervisor);
        }

        // GET: Supervisores/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Supervisores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSupervisor,Nombres,Apellidos,Edad,Antiguedad")] Supervisor supervisor)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7137/api/supervisores/");
                    //HTTP POST
                    var responseTask = client.PostAsJsonAsync("Create", supervisor);
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
            return View(supervisor);
        }

        // GET: Supervisores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Supervisor supervisor = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/supervisores/");
                //HTTP GET
                var responseTask = client.GetAsync($"Edit?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Supervisor>();
                    readTask.Wait();
                    supervisor = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }

            if (supervisor == null)
            {
                return NotFound();
            }

            return View(supervisor);
        }

        // POST: Supervisores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("IdSupervisor,Nombres,Apellidos,Edad,Antiguedad")] Supervisor supervisor)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7137/api/supervisores/");
                    //HTTP POST
                    var responseTask = client.PostAsJsonAsync("Edit", supervisor);
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
            return View(supervisor);
        }

        // GET: Supervisores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Supervisor supervisor = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/supervisores/");
                //HTTP GET
                var responseTask = client.GetAsync($"Delete?id={id}");
                responseTask.Wait();
                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadFromJsonAsync<Supervisor>();
                    readTask.Wait();
                    supervisor = readTask.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                    return new EmptyResult();
                }
            }

            if (supervisor == null)
            {
                return NotFound();
            }

            return View(supervisor);
        }

        // POST: Supervisores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7137/api/supervisores/");
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
