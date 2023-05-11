using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;                             // agregar esta libreria
using dominio;

namespace negocio
{
    public class PokemonNegocio
    {
        public List<Pokemon> listar()                          // abrimos una coleccion
        {
            List<Pokemon> lista = new List<Pokemon>();
            SqlConnection conexion = new SqlConnection();       // instancia de objeto para conectar a base de datos
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;                            // esto devuelve como resultado de la consulta a la base de datos un objeto de este tipo


            try
            {                                                                                                          // este ultimo es el tipo de conexion windows autentication,si usaramos usuario y contra pondriamos el integrated security en false ; y luego usuario y contraseña     
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=POKEDEX_DB; integrated security=true";     // ponemos el nombre del server local de nuestra compu (sql)
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select Numero , P.Nombre , P.Descripcion , P.Urlimagen , T.Descripcion Tipo , D.Descripcion Debilidad from POKEMONS P, ELEMENTOS T,ELEMENTOS D where P.IdTipo = T.Id and P.IdDebilidad = D.Id";  // esta es la consulta que hacemos a la base de ddatos,traer la info de esas 3 columnas
                comando.Connection = conexion;

                conexion.Open();                                       // abro la conexion
               lector = comando.ExecuteReader();                               // ejecuto la lectura y me devuleve la consulta en forma de objeto (lector)

                while (lector.Read()) 
                {
                    Pokemon aux = new Pokemon();                     // esto es para recorrer como si fuera un puntero cada linea de las columnas y extrayendo la info pedida
                 
                    aux.Numero = lector.GetInt32(0);          // indice 0 porque hacemos referncia a la pimera columna,en este caso son 3 

                    aux.Nombre = (string)lector["Nombre"];     // forma mas rapida,mimso que en la linea anterior

                    aux.Descripcion = (string)lector["Descripcion"]; // idem 

                    aux.UrlImagen = (string)lector["UrlImagen"];

                    aux.Tipo = new Elementos();                  // creo instancia del objeto elementos,es una asociacion un objeto tipo elementos

                    aux.Tipo.Descripcion = (string)lector["Tipo"];

                    aux.Debilidad = new Elementos();             // creo nuevamente instancia del objeto elementos,una propiedad debilidad de tipo elementos

                    aux.Debilidad.Descripcion = (string)lector["Debilidad"];
                    

                    lista.Add(aux);                            // finalmete agrego a la lista,con cada vuelta de while recorre las 3 columnas y agrega datos a la lista si encuentra 
                }

                conexion.Close();                           // cerramos conexion

                return lista;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
           
        }


    }
}
