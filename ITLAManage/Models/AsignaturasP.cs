using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITLAManage.Models
{
    public class AsignaturasP
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Este campo es requerido")]
        public string Nombre { get; set; }

        public Nullable<System.DateTime> FechaRegistro { get; set; }
    }

    [MetadataType(typeof(Asignaturas))]
    public partial class Asignaturas{}
}