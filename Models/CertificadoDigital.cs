using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CertiNet.Models
{
    public class CertificadoDigital
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A data de emissão é obrigatória.")]
        [DisplayName("Data de Emissão")]
        public DateTime DataEmissao { get; set; }

        [Required(ErrorMessage = "A data de vencimento é obrigatória.")]
        [DisplayName("Data de Vencimento")]
        public DateTime DataVencimento { get; set; }

        [DisplayName("Ativo?")]
        public bool EstaAtivo { get; set; }



        [DisplayName("Cliente")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Cliente? Cliente { get; set; }


        [DisplayName("Produto")]
        public int ProdutoId { get; set; }
        
        [ForeignKey("ProdutoId")]
        public virtual Produto? Produto { get; set; }

        public int? AgendamentoId { get; set; }

        [ForeignKey("AgendamentoId")]
        public virtual Agendamento? Agendamento { get; set; }
    }
}
