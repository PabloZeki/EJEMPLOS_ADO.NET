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
        public FrmAltaPokemon()
        {
            InitializeComponent();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Close();                           // btn cancelar para cerrar la ventana de alta pokemon
        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            Pokemon pokemon = new Pokemon();       // instancia del objeto pokemon, para capturar las prop ingresadas por la ventana alta pokemon
            PokemonNegocio negocio = new PokemonNegocio();   // instanciamos pokemon negocio para llamar a la funcion agregar 
            try
            {
                pokemon.Numero = int.Parse(txtboxNumero.Text);        // int.Parse para castear de string a int y que no chille
                pokemon.Nombre = txtboxNombre.Text;
                pokemon.Descripcion = txtboxDescripcion.Text;
                negocio.agregar(pokemon);                             // funcion agregar le mandamos el objeto pokemon nuevo 
                MessageBox.Show("Agregado correctamente");
                Close();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());   // con esto en caso que salte la excepcion nos muestra el error por pantalla
            }
        }
    }
}
