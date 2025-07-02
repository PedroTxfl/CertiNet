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

        // Relatório 1: Certificados a Vencer (Sem alterações, já estava correto)
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

        

        // Relatório 3: Performance Financeira de Agentes (CORRIGIDO)
        public async Task<IActionResult> PerformanceAgentes()
        {
            // ETAPA 1: Buscar os dados do banco para a memória
            var vendasCompletas = await _context.CertificadoDigital
                .Include(c => c.Produto)
                .Include(c => c.Agendamento)
                    .ThenInclude(a => a.Usuario)
                .Where(c => c.AgendamentoId != null)
                .ToListAsync(); // Executa a consulta SQL aqui!

            // ETAPA 2: Processar a lista em memória
            var relatorio = vendasCompletas
                .GroupBy(c => c.Agendamento.Usuario) // Agora o GroupBy funciona
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