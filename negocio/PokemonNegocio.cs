using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;                             // agregar esta libreria
using dominio;
using System.Diagnostics.Contracts;

namespace negocio
{
    public class PokemonNegocio
    {
        public List<Pokemon> listar()                          // abrimos una coleccion.. ( leemos la db)
        {
            List<Pokemon> lista = new List<Pokemon>();
            SqlConnection conexion = new SqlConnection();      // aca creo el constructor vacio 
            SqlCommand comando = new SqlCommand();            // instancia de objeto para conectar a base de datos
            SqlDataReader lector;                            // esto devuelve como resultado de la consulta a la base de datos un objeto de este tipo


            try
            {                                                                                                          // este ultimo es el tipo de conexion windows autentication,si usaramos usuario y contra pondriamos el integrated security en false ; y luego usuario y contraseña     
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=POKEDEX_DB; integrated security=true";     // ponemos el nombre del server local de nuestra compu (sql); aca le asigno la propiedad al constructor
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select P.Id , P.Numero , P.Nombre , P.Descripcion , P.Urlimagen , T.Descripcion Tipo , D.Descripcion Debilidad,P.IdTipo,P.IdDebilidad from POKEMONS P, ELEMENTOS T,ELEMENTOS D where P.IdTipo = T.Id and P.IdDebilidad = D.Id";  // esta es la consulta que hacemos a la base de ddatos,traer la info de esas 3 columnas
                comando.Connection = conexion;

                conexion.Open();                              // abro la conexion
               lector = comando.ExecuteReader();             // ejecuto la lectura y me devuleve la consulta en forma de objeto (lector)

                while (lector.Read()) 
                {
                    Pokemon aux = new Pokemon();                     // esto es para recorrer como si fuera un puntero cada linea de las columnas y extrayendo la info pedida
                 
                    aux.Numero = lector.GetInt32(0);          // indice 0 porque hacemos referncia a la pimera columna,en este caso son 3 

                    aux.idPokemon = (int)lector["Id"];        // traemos el id de pokemon para poder validar si estamos modificando o agregando un pokemon nuevo ( alta pokemon)

                    aux.Nombre = (string)lector["Nombre"];     // forma mas rapida,mimso que en la linea anterior

                    if (!(lector["Descripcion"]is DBNull))

                    aux.Descripcion = (string)lector["Descripcion"]; // idem 

                    if (!(lector["UrlImagen"] is DBNull))            // validamos si no es db null la siguiente linea que se ejecute,sino que pase de largo
                    
                    aux.UrlImagen = (string)lector["UrlImagen"];


                    aux.Tipo = new Elementos();                  // creo instancia del objeto elementos,es una asociacion un objeto tipo elementos
  
                    aux.Tipo.Id = (int)lector["idTipo"];         // para que funcione el valuemember y me muestre en el desplegable el item seleccionado a la hora de modificar un pokemon

                    aux.Tipo.Descripcion = (string)lector["Tipo"];

                    aux.Debilidad = new Elementos();             // creo nuevamente instancia del objeto elementos,una propiedad debilidad de tipo elementos

                    aux.Debilidad.Id = (int)lector["idDebilidad"];   // 

                    aux.Debilidad.Descripcion = (string)lector["Debilidad"];  // para que funcione el valuemember y me muestre en el desplegable el item seleccionado a la hora de modificar un pokemon


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


        public void agregar(Pokemon nuevo)                   // funcion para agregar un pokemon nuevo
        {
            Acceso_a_datos datos = new Acceso_a_datos();      // iinstanciamos para poder conectar a la base de datos

            try
            {
                datos.setearConsulta("insert into POKEMONS (Numero , Nombre,Descripcion,UrlImagen,Activo,idTipo,idDebilidad)values (@Numero,@Nombre,@Descripcion ,@UrlImagen,1,@idTipo,@idDebilidad)");    // mandamos como parametro la consulta a la DB en este caso (insert) hacemos la modificaciones en la cadena luego de values reemplazando por las variables de consulta 
                datos.setearParametro("@Numero", nuevo.Numero);
                datos.setearParametro("@Nombre", nuevo.Nombre);
                datos.setearParametro("@Descripcion",nuevo.Descripcion);
                datos.setearParametro("@UrlImagen", nuevo.UrlImagen);
                datos.setearParametro("@idTipo", nuevo.Tipo.Id);
                datos.setearParametro("@idDebilidad", nuevo.Debilidad.Id);
                datos.ejecutarAccion();                            // llamamos a la funcion para ejecutar el insert en la base de datos ( no es igual que reader)
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();                        // llamamos a la funcion cerrar conexion
            }
        }

        public void modificar(Pokemon poke)
        {
            Acceso_a_datos datos = new Acceso_a_datos();
            try
            {
                datos.setearConsulta("update POKEMONS set Numero = @numero,Nombre = @nombre,Descripcion = @descripcion , UrlImagen = @urlimagen , IdTipo = @idtipo,IdDebilidad = @iddebilidad where id = @id ");   // consulta de modificar (update) desde la DB
                datos.setearParametro("@numero", poke.Numero);
                datos.setearParametro("@nombre", poke.Nombre);
                datos.setearParametro("@descripcion", poke.Descripcion);
                datos.setearParametro("@urlimagen", poke.UrlImagen);             // los datos necesarios para modificar todos los campos de nuestro pokemon en caso que se requiera dicha accion
                datos.setearParametro("@idtipo", poke.Tipo.Id);
                datos.setearParametro("@iddebilidad", poke.Debilidad.Id);
                datos.setearParametro("@id ", poke.idPokemon);
                datos.ejecutarAccion();

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
