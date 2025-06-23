using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CertiNet.Models;

namespace CertiNet.Data
{
    public class CertiNetContext : DbContext
    {
        public CertiNetContext (DbContextOptions<CertiNetContext> options)
            : base(options)
        {
        }

        public DbSet<CertiNet.Models.Usuario> Usuario { get; set; } = default!;

        public DbSet<CertiNet.Models.Agendamento>? Agendamento { get; set; }

        public DbSet<CertiNet.Models.Cliente>? Cliente { get; set; }

        public DbSet<CertiNet.Models.CertificadoDigital>? CertificadoDigital { get; set; }

        public DbSet<CertiNet.Models.Produto>? Produto { get; set; }
    }
}
