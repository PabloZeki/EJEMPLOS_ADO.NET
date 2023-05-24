using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dominio
{
   public  class Pokemon
    {
        public int idPokemon { get; set; }    // traigo el id pokemon para evaluar si modificamos o agregamos un pokemon nuevo ( alta pokemon)
        
        [DisplayName("Número")]     // para corregir alguna falta de ortografia,acentos,separacion de palabras de en este caso columnas de la DB
        public int Numero { get; set; }
        public string Nombre { get; set; }
        [DisplayName("Descripción")]
        public string Descripcion { get; set; }
        [DisplayName("Url Imagen")]
        public string UrlImagen { get; set; }
        public Elementos Tipo { get; set; }
        public Elementos Debilidad  { get; set; }
     
    }
}
