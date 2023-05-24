using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using dominio;
using negocio;


namespace winform_app
{
    public partial class frmPokemons : Form
    {
       // private List<Elementos> listaElementos;
        private List<Pokemon> listaPokemon;          // me guardo lo que me devuelve la base de datos,en un atributo privado list o colection usando una variable llamada listapokemon
        public frmPokemons()
        {
            InitializeComponent();
        }

        private void frmPokemons_Load(object sender, EventArgs e)
        {
            // ElementoNegocio elemento = new ElementoNegocio();
            //listaElementos = elemento.listar();
            // dgvPokemons.DataSource = listaElementos;

            cargar();

        }

        private void dgvPokemons_SelectionChanged(object sender, EventArgs e)        // evento del data grid view
        {
            Pokemon seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem; // guardamos en la variable seleccionado el pokemon enlazado a la lista de objetos 
          cargarimagen(seleccionado.UrlImagen);                     // caundo carga la primera ves ya va a mostrar la imagen del primero elemento o pokemon de la lista
                                                                    // y al bajar con la flecha o con el mouse por las filas tmb va cambiando la imagen del pokemon
                                                       

        }
        private void cargar()                             // metodo cargar en este caso la tabla de pokemons
        {
            PokemonNegocio negocio = new PokemonNegocio();
            try
            {
                listaPokemon = negocio.listar();
                dgvPokemons.DataSource = listaPokemon;  // con estas lineas de codigo traemos la data de las columnas solicitadas
                dgvPokemons.Columns["UrlImagen"].Visible = false; // para ocultar las direcciones url de la tabla sql
                dgvPokemons.Columns["idPokemon"].Visible = false;  // ocultamos la columna de id pokemon

                cargarimagen(listaPokemon[0].UrlImagen);  // mandamos la lista pokemon a la picturebox con referencia al primer elemento de la lista [0] y la direccion 


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

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

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            FrmAltaPokemon agregar = new FrmAltaPokemon();
            agregar.ShowDialog();
            cargar();                              // luego de cargar un pokemon nuevo llamo al metodo cargar para que me actualice en tiempo real la tabla de pokemons
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Pokemon seleccionado;
             seleccionado = (Pokemon)dgvPokemons.CurrentRow.DataBoundItem;      // aca estamos diciendo basicamente que lo que seleccionamos de la grid se guarde en la variable seleccionado perteneciente al dominio de la clase Pokemon
         

            FrmAltaPokemon agregar = new FrmAltaPokemon(seleccionado);        // le mandamos por parametro el contenido de la variable selecionado al constructor
            agregar.ShowDialog();
            cargar();
           

        }
    }
}
