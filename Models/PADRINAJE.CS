using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Patitas_Felices.Models
{
    [Table("PADRINAJE")]
    public class PADRINAJE
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        public int monto { get; set; }

        public CLIENTE  CLIENTE{ get; set; }

        public MASCOTAS MASCOTAS{ get; set; }


    }
}