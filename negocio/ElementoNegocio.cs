using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dominio;
namespace negocio
{
    public class ElementoNegocio
    {
        public List<Elementos> listar()                // si queremos traer las columnas de elementos en la base de datos
        {
            List<Elementos> lista = new List<Elementos>();   // creamos una instancia de elementos 
            Acceso_a_datos datos = new Acceso_a_datos();     // agregamos la clase acceso a datos
            try
            {
                datos.setearConsulta("select id,Descripcion from ELEMENTOS");
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Elementos aux = new Elementos();
                    aux.Id = (int)datos.Lector["id"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    lista.Add(aux);
                }

                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }




        }

            
    }
}
