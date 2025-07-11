﻿using CertiNet1.Areas.Identity.Data;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CertiNet1.Models
{
    public class Agendamento
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A data e hora são obrigatórias.")]
        [DisplayName("Data e Hora")]
        public DateTime DataHora { get; set; }

        [Required]
        public ModalidadeAgendamento Modalidade { get; set; }

        [Required]
        public StatusAgendamento Status { get; set; }



        [DisplayName("Cliente")]
        public int ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public virtual Cliente? Cliente { get; set; }


        [DisplayName("Agente de Registro")]
        public string UsuarioId { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual UserModel? Usuario { get; set; }
    }

    public enum ModalidadeAgendamento
    {
        Online,
        Presencial
    }

    public enum StatusAgendamento
    {
        Agendado,
        Concluido,
        Cancelado
    }
}
