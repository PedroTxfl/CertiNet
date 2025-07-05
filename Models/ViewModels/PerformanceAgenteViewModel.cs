using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CertiNet1.Models.ViewModels
{
    public class PerformanceAgenteViewModel
    {
        [DisplayName("Nome do Agente")]
        public string NomeAgente { get; set; }

        [DisplayName("Vendas Realizadas")]
        public int VendasRealizadas { get; set; }

        [DisplayName("Valor Total Vendido")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal ValorTotalVendido { get; set; }
    }
}
