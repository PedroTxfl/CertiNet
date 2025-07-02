using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CertiNet.Models
{
    public class Usuario : IdentityUser
    {
        [PersonalData]
        public string Nome { get; set; }

        [PersonalData]
        public PerfilUsuario Perfil { get; set; }

        public virtual ICollection<Agendamento> Agendamentos { get; set; } = new List<Agendamento>();
    }

    public enum PerfilUsuario
    {
        Admin,
        AgenteDeRegistro
    }

}
