using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Patitas_Felices.Models
{
    [Table("VISITAS")]
    public class VISITAS
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }    
         public DateTime FechaYHora { get; set; }         

        public CLIENTE  CLIENTE{ get; set; }

        public MASCOTAS MASCOTAS{ get; set; }


    }
}