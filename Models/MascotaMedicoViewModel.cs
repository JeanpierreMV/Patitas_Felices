using System.Collections.Generic;
using Patitas_Felices.Models;

namespace Patitas_Felices.Models
{
    public class MascotaMedicoViewModel
    {
        public H_MEDICO Medico { get; set; }
        public List<MASCOTAS> Mascotas { get; set; }

        public MascotaMedicoViewModel()
        {
            Medico = new H_MEDICO();
            Mascotas = new List<MASCOTAS>();
        }
    }
}
