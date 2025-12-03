using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoologico_Modelo
{
    public class Animal
    {
        [Key] public int Id {  get; set; }
        public string Name { get; set; }

        public int EspecieId {  get; set; }
        public int RazaId {  get; set; }
        public Raza? IdRaza { get; set; }
        public Especie? IdEspecie { get; set; }


    }
}
