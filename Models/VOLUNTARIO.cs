using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Patitas_Felices.Models
{
    [Table("VOLUNTARIO")]
    public class VOLUNTARIO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        public  DateTime FechaYHora { get; set; }
        public CLIENTE  CLIENTE{ get; set; }
         public ICollection<TAREA> TareasRealizadas { get; set; } 

    }
}