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
    public class CertificadoDigitalsController : Controller
    {
        private readonly CertiNet1Context _context;

        public CertificadoDigitalsController(CertiNet1Context context)
        {
            _context = context;
        }

        // GET: CertificadoDigitals
        public async Task<IActionResult> Index()
        {
            var certiNet1Context = _context.CertificadosDigitais.Include(c => c.Agendamento).Include(c => c.Cliente).Include(c => c.Produto);
            return View(await certiNet1Context.ToListAsync());
        }

        // GET: CertificadoDigitals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CertificadosDigitais == null)
            {
                return NotFound();
            }

            var certificadoDigital = await _context.CertificadosDigitais
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

        // GET: CertificadoDigitals/Create
        public IActionResult Create()
        {
            ViewData["AgendamentoId"] = new SelectList(_context.Agendamentos, "Id", "Id");
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "CPF_CNPJ");
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Categoria");
            return View();
        }

        // POST: CertificadoDigitals/Create
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
            ViewData["AgendamentoId"] = new SelectList(_context.Agendamentos, "Id", "Id", certificadoDigital.AgendamentoId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "CPF_CNPJ", certificadoDigital.ClienteId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Categoria", certificadoDigital.ProdutoId);
            return View(certificadoDigital);
        }

        // GET: CertificadoDigitals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.CertificadosDigitais == null)
            {
                return NotFound();
            }

            var certificadoDigital = await _context.CertificadosDigitais.FindAsync(id);
            if (certificadoDigital == null)
            {
                return NotFound();
            }
            ViewData["AgendamentoId"] = new SelectList(_context.Agendamentos, "Id", "Id", certificadoDigital.AgendamentoId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "CPF_CNPJ", certificadoDigital.ClienteId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Categoria", certificadoDigital.ProdutoId);
            return View(certificadoDigital);
        }

        // POST: CertificadoDigitals/Edit/5
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
            ViewData["AgendamentoId"] = new SelectList(_context.Agendamentos, "Id", "Id", certificadoDigital.AgendamentoId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "Id", "CPF_CNPJ", certificadoDigital.ClienteId);
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Categoria", certificadoDigital.ProdutoId);
            return View(certificadoDigital);
        }

        // GET: CertificadoDigitals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CertificadosDigitais == null)
            {
                return NotFound();
            }

            var certificadoDigital = await _context.CertificadosDigitais
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

        // POST: CertificadoDigitals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.CertificadosDigitais == null)
            {
                return Problem("Entity set 'CertiNet1Context.CertificadosDigitais'  is null.");
            }
            var certificadoDigital = await _context.CertificadosDigitais.FindAsync(id);
            if (certificadoDigital != null)
            {
                _context.CertificadosDigitais.Remove(certificadoDigital);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CertificadoDigitalExists(int id)
        {
          return (_context.CertificadosDigitais?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
