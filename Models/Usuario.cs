using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CertiNet.Models
{
    public class Usuario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do usuário é obrigatório.")]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O login é obrigatório.")]
        [StringLength(50)]
        [DisplayName("Login")]
        public string UsuarioLogin { get; set; }

        [Required]
        [StringLength(255)] // O hash pode ser longo dependendo do algoritmo
        public string SenhaHash { get; set; } // Importante: Guarde sempre a senha como hash, nunca em texto puro!

        [Required]
        public PerfilUsuario Perfil { get; set; }

        public virtual ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
    }

    public enum PerfilUsuario
    {
        Admin,
        AgenteDeRegistro
    }

}
