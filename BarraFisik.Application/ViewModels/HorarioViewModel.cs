using System;
using System.ComponentModel.DataAnnotations;
using BarraFisik.Domain.Entities;

namespace BarraFisik.Application.ViewModels
{
    public class HorarioViewModel
    {
        public HorarioViewModel()
        {
            HorarioId = Guid.NewGuid();
        }

        [Key]
        public Guid HorarioId { get; set; }

        public bool Segunda { get; set; }
        public string HSegunda { get; set; }

        public bool Terca { get; set; }
        public string HTerca { get; set; }

        public bool Quarta { get; set; }
        public string HQuarta { get; set; }

        public bool Quinta { get; set; }
        public string HQuinta { get; set; }

        public bool Sexta { get; set; }
        public string HSexta { get; set; }

        public Guid? ClienteId { get; set; }
        public Cliente Cliente { get; set; }
    }
}