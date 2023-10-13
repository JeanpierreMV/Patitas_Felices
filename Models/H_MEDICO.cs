using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Patitas_Felices.Models
{
    [Table("H_MEDICO")]
    public class H_MEDICO
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }              
        public string Tip_Sangre { get; set; }
        public string Fecha_reg { get; set; }
        public byte[] Adj_img { get; set; }
        public byte[] D_medicinas { get; set; }
        public byte[] D_tratamientos { get; set; }
        public string observaciones { get; set; }

        public MASCOTAS MASCOTAS{ get; set; }

         [NotMapped]
        public IFormFile? fileMedicinas{ get; set; }

          [NotMapped]
        public IFormFile? fileTratamientos{ get; set; }

         [NotMapped]
        public IFormFile? ImagenUp{ get; set; }
      


    }
}