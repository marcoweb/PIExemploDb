using System.Threading.Tasks;           // Necessário para "Task"
using Microsoft.AspNetCore.Mvc;         // Necessário para "ToListAssync"
using Microsoft.EntityFrameworkCore;    // Necessário para "Controller"
using PIExemploDb.Models;               // Necessário para "LivrosDbContext"
using System.Linq;                      // Necessário para "Any"

namespace PIExemploDb.Controllers
{
    public class GenerosController : Controller
    {
        private readonly LivrosDbContext _context = new LivrosDbContext();

        public async Task<IActionResult> Index()
        {
            return View(await _context.Generos.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Nome")] Genero genero)
        {
            if (ModelState.IsValid)
            {
                _context.Add(genero);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(genero);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await _context.Generos.SingleOrDefaultAsync(m => m.ID == id);
            if (genero == null)
            {
                return NotFound();
            }
            return View(genero);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Nome")] Genero genero)
        {
            if (id != genero.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(genero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_context.Generos.Any(e => e.ID == genero.ID))
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
            return View(genero);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var genero = await _context.Generos
                .SingleOrDefaultAsync(m => m.ID == id);
            if (genero == null)
            {
                return NotFound();
            }

            return View(genero);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var genero = await _context.Generos.SingleOrDefaultAsync(m => m.ID == id);
            _context.Generos.Remove(genero);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}

