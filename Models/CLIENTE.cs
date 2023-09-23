using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Patitas_Felices.Models
{
     [Table("Cliente")]
    public class CLIENTE
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int id { get; set; }
        public string nombres { get; set; }
        public string apellidos { get; set; }
        public string apodo { get; set; }
        public string domicilio { get; set; }
        public int celular { get; set; }
        public int dni { get; set; }
        public IdentityUser User { get; set; }
    }
}