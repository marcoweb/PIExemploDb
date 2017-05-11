using System.Threading.Tasks;               // Necessário para "Task"
using Microsoft.AspNetCore.Mvc;             // Necessário para "ToListAssync"
using Microsoft.EntityFrameworkCore;        // Necessário para "Controller"
using PIExemploDb.Models;                   // Necessário para "LivrosDbContext"
using System.Linq;                          // Necessário para "Any"
using Microsoft.AspNetCore.Mvc.Rendering;   // Necessário para "SelectList"

namespace PIExemploDb.Controllers
{
    public class LivrosController : Controller
    {
        private readonly LivrosDbContext _context = new LivrosDbContext();

        public async Task<IActionResult> Index()
        {
            var livrosDbContext = _context.Livros.Include(a => a.Genero);
            return View(await livrosDbContext.ToListAsync());
        }

        public IActionResult Create()
        {
            ViewData["GeneroId"] = new SelectList(_context.Generos, "ID", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ID,Titulo,GeneroId")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(livro);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["GeneroId"] = new SelectList(
                _context.Generos, "ID", "ID", livro.GeneroId);
            return View(livro);
        }
    }
}


