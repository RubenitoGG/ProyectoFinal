﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorAeropuerto.Model
{
    [Table(name: "Aviones")]
    public class Avion
    {
        public int AvionId { get; set; }

        [Required(ErrorMessage = "Modelo obligatorio")]
        public string Modelo { get; set; }

        [Required(ErrorMessage = "Fabricante obligatorio")]
        public string Fabricante { get; set; }

        [Required(ErrorMessage = "Capacidad obligatoria")]
        public int Capacidad { get; set; }

        public virtual Vuelo Vuelo { get; set; }
    }
}
