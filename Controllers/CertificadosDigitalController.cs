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
    public class CertificadosDigitalController : Controller
    {
        private readonly CertiNetContext _context;

        public CertificadosDigitalController(CertiNetContext context)
        {
            _context = context;
        }

        // GET: CertificadosDigital
        public async Task<IActionResult> Index()
        {
            var certiNetContext = _context.CertificadoDigital.Include(c => c.Agendamento).Include(c => c.Cliente).Include(c => c.Produto);
            return View(await certiNetContext.ToListAsync());
        }

        // GET: CertificadosDigital/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CertificadoDigital == null)
            {
                return NotFound();
            }

            var certificadoDigital = await _context.CertificadoDigital
                .Include(c => c.Agendamento)
                .Include(c => c.Cliente)
                .Include(c => c.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certificadoDigital == null)
            {
                return NotFound();
            }

            return View(certificadoDigital);
        }

        // GET: CertificadosDigital/Create
        public IActionResult Create()
        {
            ViewData["AgendamentoId"] = new SelectList(_context.Agendamento, "Id", "Id");
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "CPF_CNPJ");
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "Id", "Categoria");
            return View();
        }

        // POST: CertificadosDigital/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataEmissao,DataVencimento,EstaAtivo,ClienteId,ProdutoId,AgendamentoId")] CertificadoDigital certificadoDigital)
        {
            if (ModelState.IsValid)
            {
                _context.Add(certificadoDigital);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AgendamentoId"] = new SelectList(_context.Agendamento, "Id", "Id", certificadoDigital.AgendamentoId);
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "CPF_CNPJ", certificadoDigital.ClienteId);
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "Id", "Categoria", certificadoDigital.ProdutoId);
            return View(certificadoDigital);
        }

        // GET: CertificadosDigital/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CertificadoDigital == null)
            {
                return NotFound();
            }

            var certificadoDigital = await _context.CertificadoDigital.FindAsync(id);
            if (certificadoDigital == null)
            {
                return NotFound();
            }
            ViewData["AgendamentoId"] = new SelectList(_context.Agendamento, "Id", "Id", certificadoDigital.AgendamentoId);
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "CPF_CNPJ", certificadoDigital.ClienteId);
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "Id", "Categoria", certificadoDigital.ProdutoId);
            return View(certificadoDigital);
        }

        // POST: CertificadosDigital/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataEmissao,DataVencimento,EstaAtivo,ClienteId,ProdutoId,AgendamentoId")] CertificadoDigital certificadoDigital)
        {
            if (id != certificadoDigital.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(certificadoDigital);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CertificadoDigitalExists(certificadoDigital.Id))
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
            ViewData["AgendamentoId"] = new SelectList(_context.Agendamento, "Id", "Id", certificadoDigital.AgendamentoId);
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "CPF_CNPJ", certificadoDigital.ClienteId);
            ViewData["ProdutoId"] = new SelectList(_context.Produto, "Id", "Categoria", certificadoDigital.ProdutoId);
            return View(certificadoDigital);
        }

        // GET: CertificadosDigital/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CertificadoDigital == null)
            {
                return NotFound();
            }

            var certificadoDigital = await _context.CertificadoDigital
                .Include(c => c.Agendamento)
                .Include(c => c.Cliente)
                .Include(c => c.Produto)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certificadoDigital == null)
            {
                return NotFound();
            }

            return View(certificadoDigital);
        }

        // POST: CertificadosDigital/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CertificadoDigital == null)
            {
                return Problem("Entity set 'CertiNetContext.CertificadoDigital'  is null.");
            }
            var certificadoDigital = await _context.CertificadoDigital.FindAsync(id);
            if (certificadoDigital != null)
            {
                _context.CertificadoDigital.Remove(certificadoDigital);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CertificadoDigitalExists(int id)
        {
          return (_context.CertificadoDigital?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
