using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winform_app
{
    internal class Elementos
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public override string ToString()
        {
            return Descripcion;                // sobreescribimos el tostring para modificar la visibilidad de la columna tipo
        }
    }

    
}
