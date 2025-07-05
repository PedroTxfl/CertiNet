using CertiNet1.Data;
using CertiNet1.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CertiNet1.Controllers
{
    [Authorize(Roles = "Admin, AgenteDeRegistro")]

    public class RelatoriosController : Controller
    {
        private readonly CertiNet1Context _context;

        public RelatoriosController(CertiNet1Context context)
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
            var certificados = await _context.CertificadosDigitais
                .Include(c => c.Cliente)
                .Include(c => c.Produto)
                .Where(c => c.DataVencimento >= DateTime.Now && c.DataVencimento <= dataLimite)
                .OrderBy(c => c.DataVencimento)
                .ToListAsync();
            ViewBag.Dias = dias;
            return View(certificados);
        }



        public async Task<IActionResult> PerformanceAgentes(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var vendasCompletas = await _context.CertificadosDigitais
                .Include(c => c.Produto)
                .Include(c => c.Agendamento)
                    .ThenInclude(a => a.Usuario)
                .Where(c => c.AgendamentoId != null)
                .ToListAsync();

            var relatorioQuery = vendasCompletas
                .GroupBy(c => c.Agendamento.Usuario)
                .Select(g => new PerformanceAgenteViewModel
                {
                    NomeAgente = g.Key.Nome,
                    ValorTotalVendido = g.Sum(c => c.Produto.Preco),
                    VendasRealizadas = g.Count()
                });

            if (!String.IsNullOrEmpty(searchString))
            {
                relatorioQuery = relatorioQuery.Where(r => r.NomeAgente.Contains(searchString, StringComparison.OrdinalIgnoreCase));
            }

            var relatorioFinal = relatorioQuery
                .OrderByDescending(r => r.ValorTotalVendido)
                .ToList();

            return View(relatorioFinal);
        }
    }
}