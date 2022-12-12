using dominio;
using negocio;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Presentacion
{
    public partial class Alta_Articulo : Form
    {
        private Articulo articulo = null;
        
        public Alta_Articulo()
        {
            InitializeComponent();
        }

        public Alta_Articulo(Articulo articulo)
        {
            InitializeComponent();
            this.articulo = articulo;
            Text = "Modificar Artículo";
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Alta_Articulo_Load(object sender, EventArgs e)
        {
            
            MarcaNegocio marcaNegocio = new MarcaNegocio();
            CategoriaNegocio categoriaNegocio = new CategoriaNegocio();
       
            cboCategoria.DataSource = categoriaNegocio.listar();
            cboCategoria.ValueMember = "Id";
            cboCategoria.DisplayMember = "Descripcion";
            cboMarca.DataSource = marcaNegocio.listar();
            cboMarca.ValueMember = "Id";
            cboMarca.DisplayMember = "Descripcion";
            if(articulo != null)
            {
               // string precio = string.Format("{G2}", articulo.Precio.ToString());
                txtCodigo.Text = articulo.Codigo;
                txtNombre.Text = articulo.Nombre;
                txtDescripcion.Text = articulo.Descripcion;
                txtPrecio.Text = articulo.Precio.ToString();
                txtUrlImagen.Text = articulo.ImagenUrl;
                cboCategoria.SelectedItem = articulo.Categoria.Descripcion;
                cboMarca.SelectedItem = articulo.Marca.Descripcion;
                cargarImagen(txtUrlImagen.Text);
            }

        }
     

        private bool validacionCampos()
        {
            if(string.IsNullOrEmpty(txtCodigo.Text))
            {
                MessageBox.Show("Debe completar el campo Código");
                return true;
            }
            else if(string.IsNullOrEmpty(txtNombre.Text))
            {
                MessageBox.Show("Debe completar el campo Nombre");
                return true;
            }
            else if(string.IsNullOrEmpty(txtPrecio.Text))
            {
                MessageBox.Show("Debe completar el campo Precio");
               
                return true;

            }
            return false;
          


        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            if (validacionCampos())
                return;

            if (articulo == null)
                articulo = new Articulo();
             articulo.Codigo = txtCodigo.Text;
             articulo.Categoria = (Categoria)cboCategoria.SelectedItem;
             articulo.Marca = (Marca)cboMarca.SelectedItem;
             articulo.Nombre = txtNombre.Text;
             articulo.Descripcion = txtDescripcion.Text;
             articulo.ImagenUrl = txtUrlImagen.Text;
            
            try
            {
                Convert.ToDecimal(txtPrecio.Text);
                articulo.Precio = decimal.Parse(txtPrecio.Text);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Cargar solo Números");
                return;
            }



            
            if(articulo.Id != 0)
            {
                negocio.modificar(articulo);
                MessageBox.Show("El articulo " + articulo + " se ha modificado exitosamente");
            }
            else
            {
                negocio.agregar(articulo);
                MessageBox.Show("El articulo " + articulo + " se ha agregado exitosamente");
            }

                Close();
        }
        public void cargarImagen(string imagen)
        {
            try
            {
                pbxAltaArticulo.Load(imagen);
            }
            catch (Exception ex)
            {

                pbxAltaArticulo.Load("https://w7.pngwing.com/pngs/22/842/png-transparent-picture-frame-blue-border-empty-blank-isolated-thumbnail.png");
            }
        }

        private void txtUrlImagen_Leave(object sender, EventArgs e)
        {
            cargarImagen(txtUrlImagen.Text);
        }
    }
}
