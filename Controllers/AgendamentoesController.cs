using CertiNet1.Data;
using CertiNet1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CertiNet1.Controllers
{
    [Authorize(Roles = "Admin, AgenteDeRegistro")]
    public class AgendamentoesController : Controller
    {
        private readonly CertiNet1Context _context;

        public AgendamentoesController(CertiNet1Context context)
        {
            _context = context;
        }

        // GET: Agendamentoes
        public async Task<IActionResult> Index(DateTime? dataFiltro)
        {
            if (dataFiltro.HasValue)
            {
                ViewData["CurrentDateFilter"] = dataFiltro.Value.ToString("yyyy-MM-dd");
            }

            var agendamentos = from a in _context.Agendamentos.Include(a => a.Cliente).Include(a => a.Usuario)
                               select a;

            if (dataFiltro.HasValue)
            {
                agendamentos = agendamentos.Where(a => a.DataHora.Date == dataFiltro.Value.Date);
            }

            agendamentos = agendamentos.OrderByDescending(a => a.DataHora);

            return View(await agendamentos.ToListAsync());
        }

        // GET: Agendamentoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Agendamentos == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamentos
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "CPF_CNPJ");
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Nome");
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "CPF_CNPJ", agendamento.ClienteId);
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", agendamento.UsuarioId);
            return View(agendamento);
        }

        // GET: Agendamentoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Agendamentos == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento == null)
            {
                return NotFound();
            }
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "CPF_CNPJ", agendamento.ClienteId);
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Nome", agendamento.UsuarioId);
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
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "CPF_CNPJ", agendamento.ClienteId);
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "Id", agendamento.UsuarioId);
            return View(agendamento);
        }

        // GET: Agendamentoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Agendamentos == null)
            {
                return NotFound();
            }

            var agendamento = await _context.Agendamentos
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
            if (_context.Agendamentos == null)
            {
                return Problem("Entity set 'CertiNet1Context.Agendamentos'  is null.");
            }
            var agendamento = await _context.Agendamentos.FindAsync(id);
            if (agendamento != null)
            {
                _context.Agendamentos.Remove(agendamento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AgendamentoExists(int id)
        {
          return (_context.Agendamentos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
