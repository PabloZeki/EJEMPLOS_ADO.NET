using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace winform_app
{
    public partial class frmPokemons : Form
    {
        private List<Pokemon> listaPokemon;          // me guardo lo que me devuelve la base de datos,en un atributo privado list o colection usando una variable llamada listapokemon
        public frmPokemons()
        {
            InitializeComponent();
        }

        private void frmPokemons_Load(object sender, EventArgs e)
        {
            PokemonNegocio negocio = new PokemonNegocio();
            listaPokemon = negocio.listar();
            dgvPokemons.DataSource = listaPokemon;  // con estas lineas de codigo traemos la data de las columnas solicitadas
            dgvPokemons.Columns["UrlImagen"].Visible = false; // para ocultar las direcciones url de la tabla sql

           cargarimagen(listaPokemon[0].UrlImagen);  // mandamos la lista pokemon a la picturebox con referencia al primer elemento de la lista [0] y la direccion 

            
        }

        private void dgvPokemons_SelectionChanged(object sender, EventArgs e)
        {
            Pokemon seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem; // guardamos en la variable seleccionado el pokemon enlazado a la lista de objetos 
          cargarimagen(seleccionado.UrlImagen);                     // caundo carga la primera ves ya va a mostrar la imagen del primero elemento o pokemon de la lista
                                                                    // y al bajar con la flecha o con el mouse por las filas tmb va cambiando la imagen del pokemon
         

        }
        private void cargarimagen(string imagen)                // funcion cargar imagen por parametro de string hace referencia a seleccionado.urlimagen
        {
            try
            {  
                pbPokemon.Load(imagen);                       // por si no habria ninguna imagen de pokemon para mostrar se romperia la app,entonces usamos una exception


            }
            catch (Exception ex)
            {
                pbPokemon.Load("https://uning.es/wp-content/uploads/2016/08/ef3-placeholder-image.jpg");   // si falla va a mostrar una imagen gris sin fondo ni color
               
            }
          



        }
    }
}
