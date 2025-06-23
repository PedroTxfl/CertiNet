using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CertiNet.Models
{
    public class Produto
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do produto é obrigatório.")]
        [StringLength(100)] // Define o tamanho máximo do campo
        [DisplayName("Nome do Produto")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "A categoria é obrigatória.")]
        [StringLength(50)]
        public string Categoria { get; set; }

        [StringLength(255)]
        [DisplayName("Descrição")]
        public string? Descricao { get; set; } 

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Column(TypeName = "decimal(18,2)")] 
        [DisplayName("Preço")]
        public decimal Preco { get; set; }

        [Required(ErrorMessage = "A validade é obrigatória.")]
        [DisplayName("Validade (em meses)")]
        public int ValidadeMeses { get; set; }


        public virtual ICollection<CertificadoDigital> CertificadosDigitais { get; set; } = new List<CertificadoDigital>();
    }
}
