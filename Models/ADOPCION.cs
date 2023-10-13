using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Patitas_Felices.Models
{
    [Table("ADOPCION")]
    public class ADOPCION
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }              
        public byte[] Fot_DNI { get; set; }     
        public byte[] R_luz { get; set; }
        public byte[] R_agua { get; set; }
        public int Q_familiares { get; set; }
        public int Q_niños { get; set; }
        public string Desc_Domicilio { get; set; }
        public string Razon { get; set; }
        public CLIENTE  CLIENTE{ get; set; }

        public MASCOTAS MASCOTAS{ get; set; }
      


    }
}