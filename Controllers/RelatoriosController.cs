using CertiNet.Data;
using CertiNet.Models;
using CertiNet.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CertiNet.Controllers
{
    public class RelatoriosController : Controller
    {
        private readonly CertiNetContext _context;

        public RelatoriosController(CertiNetContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

       
        public async Task<IActionResult> CertificadosAVencer(int dias = 30)
        {
            var dataLimite = DateTime.Now.AddDays(dias);
            var certificados = await _context.CertificadoDigital
                .Include(c => c.Cliente)
                .Include(c => c.Produto)
                .Where(c => c.DataVencimento >= DateTime.Now && c.DataVencimento <= dataLimite)
                .OrderBy(c => c.DataVencimento)
                .ToListAsync();
            ViewBag.Dias = dias;
            return View(certificados);
        }

        

        public async Task<IActionResult> PerformanceAgentes()
        {
            
            var vendasCompletas = await _context.CertificadoDigital
                .Include(c => c.Produto)
                .Include(c => c.Agendamento)
                    .ThenInclude(a => a.Usuario)
                .Where(c => c.AgendamentoId != null)
                .ToListAsync(); 

            var relatorio = vendasCompletas
                .GroupBy(c => c.Agendamento.Usuario) 
                .Select(g => new PerformanceAgenteViewModel
                {
                    NomeAgente = g.Key.Nome,
                    ValorTotalVendido = g.Sum(c => c.Produto.Preco),
                    VendasRealizadas = g.Count()
                })
                .OrderByDescending(r => r.ValorTotalVendido)
                .ToList();

            return View(relatorio);
        }
    }
}