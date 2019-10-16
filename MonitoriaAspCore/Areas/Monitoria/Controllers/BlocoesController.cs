using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MonitoriaAspCore.Areas.Monitoria.Models;
using MonitoriaAspCore.Data;

namespace MonitoriaAspCore.Areas.Monitoria.Controllers
{
    [Area("Monitoria")]
    public class BlocoesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BlocoesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Monitoria/Blocoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bloco.ToListAsync());
        }

        // GET: Monitoria/Blocoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloco = await _context.Bloco
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bloco == null)
            {
                return NotFound();
            }

            return View(bloco);
        }

        // GET: Monitoria/Blocoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Monitoria/Blocoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome")] Bloco bloco)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bloco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bloco);
        }

        // GET: Monitoria/Blocoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloco = await _context.Bloco.FindAsync(id);
            if (bloco == null)
            {
                return NotFound();
            }
            return View(bloco);
        }

        // POST: Monitoria/Blocoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome")] Bloco bloco)
        {
            if (id != bloco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bloco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BlocoExists(bloco.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(bloco);
        }

        // GET: Monitoria/Blocoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloco = await _context.Bloco
                .FirstOrDefaultAsync(m => m.Id == id);
            if (bloco == null)
            {
                return NotFound();
            }

            return View(bloco);
        }

        // POST: Monitoria/Blocoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bloco = await _context.Bloco.FindAsync(id);
            _context.Bloco.Remove(bloco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BlocoExists(int id)
        {
            return _context.Bloco.Any(e => e.Id == id);
        }
    }
}
