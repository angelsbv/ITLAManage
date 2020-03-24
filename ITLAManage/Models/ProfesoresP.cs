using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web;

namespace ITLAManage.Models
{
    public class ProfesoresP
    {

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Nombre(s)")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Apellido(s)")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Sexo")]
        public string Sexo { get; set; }
    }

    [MetadataType(typeof(ProfesoresP))]
    public partial class Profesores {

        [Required(ErrorMessage = "Este campo es requerido.")]
        public int IDAsignatura { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Cuatrimestre { get; set; }

        [Required(ErrorMessage ="Este campo es requerido.")]
        public DateTime FechaAsignacion { get; set; }

    }
}