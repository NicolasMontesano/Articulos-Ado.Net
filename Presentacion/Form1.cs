using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using negocio;
using dominio;

namespace Presentacion
{
    public partial class Form1 : Form
    {
        private List<Articulo> listaArticulo;
        public Form1()
        {
            InitializeComponent();
        }
        public void cargarImagen(string imagen)
        {
            try
            {
                pbxArticulos.Load(imagen);
            }
            catch (Exception ex)
            {
                pbxArticulos.Load("https://w7.pngwing.com/pngs/22/842/png-transparent-picture-frame-blue-border-empty-blank-isolated-thumbnail.png");

            }
        }
        private void ocultarColumnas()
        {
            dgvArticulos.Columns["Id"].Visible = false;
            dgvArticulos.Columns["ImagenUrl"].Visible = false;
            dgvArticulos.Columns["Codigo"].Visible = false;
            dgvArticulos.Columns["Descripcion"].Visible = false;
            dgvArticulos.Columns["Categoria"].Visible = false;
        }

        private void cargarDatos()
        {
            try
            {
                ArticuloNegocio negocio = new ArticuloNegocio();
                Articulo art = new Articulo();
                listaArticulo = negocio.listar();
                dgvArticulos.DataSource = listaArticulo;
                dgvArticulos.Columns["Precio"].DefaultCellStyle.Format = "0.00";
                ocultarColumnas();

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            

            cargarDatos();
            cboCriterio.Items.Add("Mayor a ");
            cboCriterio.Items.Add("Igual a ");
            cboCriterio.Items.Add("Menor a ");

        }

        private void dgvArticulos_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvArticulos.CurrentRow != null)
            {
                Articulo seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                cargarImagen(seleccionado.ImagenUrl);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Alta_Articulo alta = new Alta_Articulo();
            alta.ShowDialog();
            cargarDatos();


        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            eliminarArticulo();
            cargarDatos();
        }

        private void eliminarArticulo()
        {
            ArticuloNegocio negocio = new ArticuloNegocio();
            Articulo seleccionado;
            if (dgvArticulos.CurrentRow != null)
            {
                DialogResult respuesta = MessageBox.Show("¿Estas segurx de eliminar el archivo?", "Eliminando", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                if (respuesta == DialogResult.Yes)
                {
                    negocio.eliminar(seleccionado.Id);
                }
            }
            else
            {
                MessageBox.Show("Debe seleccionar el Artículo a eliminar");
                txtFiltro.Clear();
            }
                
           
        }

        private void btnModificar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            if(dgvArticulos.CurrentRow != null)
            {
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                Alta_Articulo modificar = new Alta_Articulo(seleccionado);
                modificar.ShowDialog();

            }
            else
            {
                MessageBox.Show("Debe seleccionar el Artículo a modificar");
                txtFiltro.Clear();
            }

        }

        private void btnDetallar_Click(object sender, EventArgs e)
        {
            Articulo seleccionado;
            if (dgvArticulos.CurrentRow != null)
            {
                seleccionado = (Articulo)dgvArticulos.CurrentRow.DataBoundItem;
                Detallar detalle = new Detallar(seleccionado);
                detalle.ShowDialog();
              
            }
            else
                MessageBox.Show("Debe seleccionar un Artículo para ver el detalle");
            
        }

   
        private bool soloNumeros(string cadena)
        {
            foreach (char caracter in cadena)
            {
                if (!(char.IsNumber(caracter)))
                    return false;

            }


            return true;
        }
 
        
        private bool validarFiltroPrecio()
        {
            if(cboCriterio.SelectedIndex < 0)
            {
                MessageBox.Show("Debe seleccionar un criterio");
                return true;
            }
            if(string.IsNullOrEmpty(txtFiltroPrecio.Text))
            {
                MessageBox.Show("Debe cargar Números");
                return true;
            }
            if(!(soloNumeros(txtFiltroPrecio.Text)))
            {
                MessageBox.Show("Debe cargar solamente Números");
                return true;
            }
            return false;
        }

        private void cboCriterio_SelectedIndexChanged(object sender, EventArgs e)
        {
            string opcion = cboCriterio.SelectedItem.ToString();
            cboCriterio.Items.Clear();
            cboCriterio.Items.Add("Menor a ");
            cboCriterio.Items.Add("Mayor a ");
            cboCriterio.Items.Add("Igual a ");
        }

        private void txtFiltro_TextChanged(object sender, EventArgs e)
        {
            txtFiltroPrecio.Clear();
            
            string filtro = txtFiltro.Text;
            List<Articulo> listaFiltrada;
            if (filtro != "")
                listaFiltrada = listaArticulo.FindAll(x => x.Nombre.ToUpper().Contains(filtro.ToUpper()) || x.Marca.Descripcion.ToUpper().Contains(filtro.ToUpper()) || x.Precio.ToString().Contains(filtro.ToString()));
            else
                listaFiltrada = listaArticulo;
            dgvArticulos.DataSource = null;
            dgvArticulos.DataSource = listaFiltrada;
            if (dgvArticulos.CurrentRow == null)
                pbxArticulos.Visible = false;
            else
                pbxArticulos.Visible = true;
             


            ocultarColumnas();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            ArticuloNegocio negocio = new ArticuloNegocio(); 
            try
            {
                if (validarFiltroPrecio())
                    return;
                {
                    string criterio = cboCriterio.SelectedItem.ToString();
                    string filtro = txtFiltroPrecio.Text;
                    dgvArticulos.DataSource = negocio.filtrar(criterio, filtro);
                }
                
            }
            catch ( Exception ex)
            {

                MessageBox.Show(ex.ToString());
            }
        }

        private void txtFiltroPrecio_TextChanged(object sender, EventArgs e)
        {
            if (txtFiltroPrecio.Text == "")
                cargarDatos();
        }

       

    }
}
