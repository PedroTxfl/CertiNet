using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CertiNet1.Models
{
    public class CertificadoDigital
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A data de emissão é obrigatória.")]
        [DataType(DataType.Date)] 
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)] 
        [DisplayName("Data de Emissão")]
        public DateTime DataEmissao { get; set; }

        [Required(ErrorMessage = "A data de vencimento é obrigatória.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DisplayName("Data de Vencimento")]
        public DateTime DataVencimento { get; set; }

        [DisplayName("Ativo?")]
        public bool EstaAtivo { get; set; }


        [Required(ErrorMessage = "É obrigatório selecionar um cliente.")] 
        [DisplayName("Cliente")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Cliente? Cliente { get; set; }


        [Required(ErrorMessage = "É obrigatório selecionar um produto.")] 
        [DisplayName("Produto")]
        public int ProdutoId { get; set; }

        [ForeignKey("ProdutoId")]
        public virtual Produto? Produto { get; set; }

        [DisplayName("Agendamento de Origem")]
        public int? AgendamentoId { get; set; }

        [ForeignKey("AgendamentoId")]
        public virtual Agendamento? Agendamento { get; set; }
    }
}