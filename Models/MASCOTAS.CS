using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Patitas_Felices.Models
{
    [Table("Mascotas")]
    public class MASCOTAS
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }   
           
        public string nombre { get; set; }
        public string raza { get; set; }
        public string genero { get; set; }
        public int edad { get; set; }
        public string tamaño { get; set; }
        public string caracter { get; set; }
        public byte[] Imagen { get; set; }
        public string  tipo{ get; set; }
        public string  Descripcion{ get; set; }
        public string  Status{ get; set; }
         [NotMapped]
        public IFormFile? ImagenUpload { get; set; }


    }
}