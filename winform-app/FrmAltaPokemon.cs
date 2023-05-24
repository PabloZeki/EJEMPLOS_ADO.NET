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
    public partial class FrmAltaPokemon : Form
    {
        private Pokemon pokemon = null;
        public FrmAltaPokemon()
        {
            InitializeComponent();                 // constructor
        }
        public FrmAltaPokemon(Pokemon pokemon)          // constructor que recibe un pokemon 
        {
            
            InitializeComponent();                 // constructor
            this.pokemon = pokemon;
            Text = "Modificar Pokemon";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();                           // btn cancelar para cerrar la ventana de alta pokemon
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
           // Pokemon pokemon = new Pokemon();       // instancia del objeto pokemon, para capturar las prop ingresadas por la ventana alta pokemon
            PokemonNegocio negocio = new PokemonNegocio();   // instanciamos pokemon negocio para llamar a la funcion agregar 
      
            try
            {
                if (pokemon == null)                  // para poder usar el atributo privado con el nombre pokemon = null,con el validamos que si sigue siendo null es que queremos agregar un pokemon nuevo
                    pokemon = new Pokemon();            // si el if es verdadero creamos la instancia de pokemon nuevo;
                pokemon.Numero = int.Parse(txtboxNumero.Text);        // int.Parse para castear de string a int y que no chille
                pokemon.Nombre = txtboxNombre.Text;
                pokemon.Descripcion = txtboxDescripcion.Text;
                pokemon.UrlImagen = txtBoxUrlimagen.Text;
                pokemon.Tipo = (Elementos) cboxTipo.SelectedItem;        // nos tira el error que no se puede convertir un objeto en elementos,para eso especificamos entre (elementos) estamos serguros que ahi dentro esta el tipo y debilidad
                pokemon.Debilidad = (Elementos)cboxDebilidad.SelectedItem;

                if (pokemon.idPokemon != 0)
                {
                    negocio.modificar(pokemon);                          // preguntamos si id pokemon es distinto de 0 es que ya se creo un pokemon asi que es una modificacion,sino por el else agregamos pokemon nuevo

                    MessageBox.Show("Modificado correctamente");
                }
                else
                {
                    negocio.agregar(pokemon);                             // funcion agregar le mandamos el objeto pokemon nuevo 
                    MessageBox.Show("Agregado correctamente");


                }





               

                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());   // con esto en caso que salte la excepcion nos muestra el error por pantalla
            }
        }

        private void FrmAltaPokemon_Load(object sender, EventArgs e)
        {
            ElementoNegocio elementonegocio = new ElementoNegocio();     // instancia de elemento negocio que tiene el id y descripcion de los elementos de tipo y debilidad

            try
            { 
                
                cboxTipo.DataSource = elementonegocio.listar();              // cargamos el combo box ( lista desplegable) con los elementos que contiene la base de datos ( agua,fuego)
                cboxTipo.ValueMember = "Id";                                // clave ( con esto le digo al desplegable cual quiero que sea su clave y su value,para indicar luego su preseleccion a la hora de cargar la ventana modificar
                cboxTipo.DisplayMember = "Descripcion";                     // Value
                cboxDebilidad.DataSource = elementonegocio.listar();            // idem
                cboxDebilidad.ValueMember = "Id";
                cboxDebilidad.DisplayMember = "Descripcion";

                if(pokemon != null)                             // si pokemon es distinto de null,o si hay un pokemon ya precargado que se ejecute la carga de datos a los txt box
                {
                    txtboxNumero.Text = pokemon.Numero.ToString();
                    txtboxNombre.Text = pokemon.Nombre;
                    txtboxDescripcion.Text = pokemon.Descripcion;
                    txtBoxUrlimagen.Text = pokemon.UrlImagen;
                    cargarimagen(pokemon.UrlImagen);

                    cboxTipo.SelectedValue = pokemon.Tipo.Id;               // aca traemos lo que eleguimos arriba como valuemember(clave) en este caso el ID para mostrar como precarga en los combo box de la ventana modificar,un id para tipo y otro id para debilidad
                    cboxDebilidad.SelectedValue = pokemon.Debilidad.Id;
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

           
            
        }
        private void txtBoxUrlimagen_Leave(object sender, EventArgs e)
        {
            cargarimagen(txtBoxUrlimagen.Text);

        }


        private void cargarimagen(string imagen)                // funcion cargar imagen por parametro de string hace referencia a seleccionado.urlimagen
        {
            try
            {
                pbxPokemon.Load(imagen);                       // por si no habria ninguna imagen de pokemon para mostrar se romperia la app,entonces usamos una exception


            }
            catch (Exception ex)
            {
                pbxPokemon.Load("https://uning.es/wp-content/uploads/2016/08/ef3-placeholder-image.jpg");   // si falla va a mostrar una imagen gris sin fondo ni color

            }




        }

       
    }
}
