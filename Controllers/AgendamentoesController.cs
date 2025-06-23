using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CertiNet.Data;
using CertiNet.Models;

namespace CertiNet.Controllers
{
    public class AgendamentoesController : Controller
    {
        private readonly CertiNetContext _context;

        public AgendamentoesController(CertiNetContext context)
        {
            _context = context;
        }

        // GET: Agendamentoes
        public async Task<IActionResult> Index()
        {
            var certiNetContext = _context.Agendamento.Include(a => a.Cliente).Include(a => a.Usuario);
            return View(await certiNetContext.ToListAsync());
        }

        // GET: Agendamentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Agendamento == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamento
                .Include(a => a.Cliente)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agendamento == null)
            {
                return NotFound();
            }

            return View(agendamento);
        }

        // GET: Agendamentoes/Create
        public IActionResult Create()
        {
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "Id", "CPF_CNPJ");
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Nome");
            return View();
        }

        // POST: Agendamentoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataHora,Modalidade,Status,ClienteId,UsuarioId")] Agendamento agendamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(agendamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "Id", "CPF_CNPJ", agendamento.ClienteId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Nome", agendamento.UsuarioId);
            return View(agendamento);
        }

        // GET: Agendamentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Agendamento == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamento.FindAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "Id", "CPF_CNPJ", agendamento.ClienteId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Nome", agendamento.UsuarioId);
            return View(agendamento);
        }

        // POST: Agendamentoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataHora,Modalidade,Status,ClienteId,UsuarioId")] Agendamento agendamento)
        {
            if (id != agendamento.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(agendamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AgendamentoExists(agendamento.Id))
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
            ViewData["ClienteId"] = new SelectList(_context.Set<Cliente>(), "Id", "CPF_CNPJ", agendamento.ClienteId);
            ViewData["UsuarioId"] = new SelectList(_context.Usuario, "Id", "Nome", agendamento.UsuarioId);
            return View(agendamento);
        }

        // GET: Agendamentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Agendamento == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamento
                .Include(a => a.Cliente)
                .Include(a => a.Usuario)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (agendamento == null)
            {
                return NotFound();
            }

            return View(agendamento);
        }

        // POST: Agendamentoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Agendamento == null)
            {
                return Problem("Entity set 'CertiNetContext.Agendamento'  is null.");
            }
            var agendamento = await _context.Agendamento.FindAsync(id);
            if (agendamento != null)
            {
                _context.Agendamento.Remove(agendamento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgendamentoExists(int id)
        {
          return (_context.Agendamento?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
