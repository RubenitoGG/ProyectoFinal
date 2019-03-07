﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestorAeropuerto.Model
{
    public class Empleado
    {
        public int EmpleadoId { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio")]
        [StringLength(20, MinimumLength = 2)]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Apellidos obligatorios")]
        [StringLength(50, MinimumLength = 2)]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "Nombre obligatorio")]
        [RegularExpression("Masculino|Femenino")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "Apellidos obligatorios")]
        [StringLength(50, MinimumLength = 2)]
        public string Pais { get; set; }
    }
}