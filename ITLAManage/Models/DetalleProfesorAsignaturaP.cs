using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITLAManage.Models
{
    public class DetalleProfesorAsignaturaP
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        public int IDAsignatura { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public int IDProfesor { get; set; }
       
        public System.DateTime FechaAsignacion { get; set; }

        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Cuatrimestre { get; set; }
    }

    [MetadataType(typeof(DetalleProfesorAsignatura))]
    public partial class DetalleProfesorAsignatura { }
}