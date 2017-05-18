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

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros.SingleOrDefaultAsync(
                m => m.ID == id);
            if (livro == null)
            {
                return NotFound();
            }
            ViewData["GeneroId"] = new SelectList(_context.Generos,
                "ID", "Nome", livro.GeneroId);
            return View(livro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("ID,Titulo,GeneroId")] Livro livro)
        {
            if (id != livro.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Livros.Any(e => e.ID == id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["GeneroId"] = new SelectList(_context.Generos,
                "ID", "Nome", livro.GeneroId);
            return View(livro);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = await _context.Livros
                .Include(l => l.Genero)
                .SingleOrDefaultAsync(m => m.ID == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var livro = await _context.Livros.SingleOrDefaultAsync(m => m.ID == id);
            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}



