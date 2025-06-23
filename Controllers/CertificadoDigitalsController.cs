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
    public class CertificadoDigitalsController : Controller
    {
        private readonly CertiNetContext _context;

        public CertificadoDigitalsController(CertiNetContext context)
        {
            _context = context;
        }

        // GET: CertificadoDigitals
        public async Task<IActionResult> Index()
        {
            var certiNetContext = _context.CertificadoDigital.Include(c => c.Cliente).Include(c => c.Produto);
            return View(await certiNetContext.ToListAsync());
        }

        // GET: CertificadoDigitals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.CertificadoDigital == null)
            {
                return NotFound();
            }

            var certificadoDigital = await _context.CertificadoDigital
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
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "CPF_CNPJ");
            ViewData["ProdutoId"] = new SelectList(_context.Set<Produto>(), "Id", "Categoria");
            return View();
        }

        // POST: CertificadoDigitals/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataEmissao,DataVencimento,EstaAtivo,ClienteId,ProdutoId")] CertificadoDigital certificadoDigital)
        {
            if (ModelState.IsValid)
            {
                _context.Add(certificadoDigital);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "CPF_CNPJ", certificadoDigital.ClienteId);
            ViewData["ProdutoId"] = new SelectList(_context.Set<Produto>(), "Id", "Categoria", certificadoDigital.ProdutoId);
            return View(certificadoDigital);
        }

        // GET: CertificadoDigitals/Edit/5
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
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "CPF_CNPJ", certificadoDigital.ClienteId);
            ViewData["ProdutoId"] = new SelectList(_context.Set<Produto>(), "Id", "Categoria", certificadoDigital.ProdutoId);
            return View(certificadoDigital);
        }

        // POST: CertificadoDigitals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataEmissao,DataVencimento,EstaAtivo,ClienteId,ProdutoId")] CertificadoDigital certificadoDigital)
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
            ViewData["ClienteId"] = new SelectList(_context.Cliente, "Id", "CPF_CNPJ", certificadoDigital.ClienteId);
            ViewData["ProdutoId"] = new SelectList(_context.Set<Produto>(), "Id", "Categoria", certificadoDigital.ProdutoId);
            return View(certificadoDigital);
        }

        // GET: CertificadoDigitals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.CertificadoDigital == null)
            {
                return NotFound();
            }

            var certificadoDigital = await _context.CertificadoDigital
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
