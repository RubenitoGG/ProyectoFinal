﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorAeropuerto.Model
{
    [Table(name: "Cargos")]
    public class Cargo
    {
        public int CargoId { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio")]
        [StringLength(20, MinimumLength = 2)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Sueldo obligatorio")]
        public double Sueldo { get; set; }

        public ICollection<Empleado> Empleados { get; set; }
    }
}
