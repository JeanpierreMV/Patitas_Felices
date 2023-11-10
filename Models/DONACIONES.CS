using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Patitas_Felices.Models
{
     [Table("Donaciones")]
    public class DONACIONES
     {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        [Column(TypeName = "timestamp with time zone")]
        public DateTime FechaHora { get; set; }

        public decimal Monto { get; set; }
        public CLIENTE  CLIENTE{ get; set; }
    }
}