using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CertiNet.Models
{
    public class Cliente
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O Nome ou Razão Social é obrigatório.")]
        [StringLength(150)]
        [DisplayName("Nome / Razão Social")]
        public string NomeRazaoSocial { get; set; }

        [Required(ErrorMessage = "O CPF ou CNPJ é obrigatório.")]
        [StringLength(18)]
        [DisplayName("CPF/CNPJ")]
        public string CPF_CNPJ { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [StringLength(100)]
        [EmailAddress(ErrorMessage = "Formato de e-mail inválido.")]
        [DisplayName("E-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        [StringLength(20)]
        [DisplayName("Telefone")]
        public string Telefone { get; set; }

        [DisplayName("Data de Cadastro")]
        public DateTime DataCadastro { get; set; }

        public virtual ICollection<CertificadoDigital> CertificadosDigitais { get; set; } = new List<CertificadoDigital>();
        public virtual ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();

    }
}
