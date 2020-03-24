using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITLAManage.Models
{
    public class EstudiantesP
    {

        [Required(ErrorMessage="Este campo es requerido.")]
        [Display(Name = "Nombre(s)")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Apellido(s)")]
        public string Apellido { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        [Display(Name = "Fecha de Nacimiento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
        public Nullable<System.DateTime> FechaNacimiento
        {
            get { return FechaNacimiento; }
            set { FechaNacimiento = value; }
        }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Sexo { get; set; }
    }

    [MetadataType(typeof(EstudiantesP))]
    public partial class Estudiantes
    {
    }
}