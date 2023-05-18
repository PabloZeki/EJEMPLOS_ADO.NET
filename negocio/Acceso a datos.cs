using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace negocio
{
    public  class Acceso_a_datos

    {
        private SqlConnection conexion;           // declaramos las variables que necesitamos para establecer la conexion a DB,asi poder usarlas 
        private SqlCommand comando;
        private SqlDataReader lector;

        public SqlDataReader Lector
        {
            get { return lector; }              // de esta manera voy a tener la posibilidad de leer la variable lector desde afuera
        }

        public Acceso_a_datos()
        {
            conexion = new SqlConnection("server=.\\SQLEXPRESS; database=POKEDEX_DB; integrated security=true");                                     // creamos el constructor y le paso la cadena de conexion como parametro string
            comando = new SqlCommand();
        }

        public void setearConsulta(string consulta)
        {
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = consulta;
        }
        public void ejecutarLectura()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                lector = comando.ExecuteReader();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void ejecutarAccion()
        {
            comando.Connection = conexion;
            try
            {
                conexion.Open();
                comando.ExecuteNonQuery();             // esto es para hacer la conexion a la base de datos pero para un insert

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void cerrarConexion()

        {
            if(lector != null)
            {
                lector.Close();
            }
            conexion.Close();
        }


    }
}
