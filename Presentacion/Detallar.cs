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

namespace Presentacion
{
    public partial class Detallar : Form
    {
        private Articulo articulo;
        public Detallar(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            
        }

        private void Detallar_Load(object sender, EventArgs e)
        {
            txtMarca.Text = articulo.Marca.ToString();
            txtNombre.Text = articulo.Nombre;
            txtCodigo.Text = articulo.Codigo;
            txtCategoria.Text = articulo.Categoria.ToString();
            lvwDescripcion.Items.Clear();
            lvwDescripcion.Items.Add(articulo.Descripcion);
            txtPrecio.Text = articulo.Precio.ToString();
            cargarImagen(articulo.ImagenUrl);
            


        }
        public void cargarImagen(string imagen)
        {
            try
            {
                pbxImagen.Load(imagen);
            }
            catch ( Exception ex)
            {

                pbxImagen.Load("https://w7.pngwing.com/pngs/22/842/png-transparent-picture-frame-blue-border-empty-blank-isolated-thumbnail.png");
            }
        }

        private void btnAtras_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    
    }
}
