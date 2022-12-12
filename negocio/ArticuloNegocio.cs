using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using dominio;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace negocio
{
    public class ArticuloNegocio
    {
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            string consulta = "select A.Id IdArticulo, C.Descripcion Categoria ,M.Descripcion Marca, Nombre, A.Descripcion Presentacion, Codigo, Precio, ImagenUrl from ARTICULOS A, MARCAS M, CATEGORIAS C where IdMarca = M.Id And IdCategoria = C.Id";
            try
            {
               
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while(datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["IdArticulo"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    aux.Marca = new Marca();
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Presentacion"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    
                   
                    lista.Add(aux);
                }
                return lista;
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
        public void agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            string consulta = "insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio ) values(@cod, @nom,@descr, @idM, @idC, @img, @prec)";
            try
            {
                datos.setearConsulta(consulta);
                datos.setearParametro("@cod", nuevo.Codigo);
                datos.setearParametro("@nom", nuevo.Nombre);
                datos.setearParametro("@descr", nuevo.Descripcion);
                datos.setearParametro("@idm", nuevo.Marca.Id);
                datos.setearParametro("@idc", nuevo.Categoria.Id);
                datos.setearParametro("@img", nuevo.ImagenUrl);
                datos.setearParametro("@prec", nuevo.Precio);
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
        public void modificar(Articulo modificado)
        {
            AccesoDatos datos = new AccesoDatos();
            string consulta = "update ARTICULOS set Codigo = @cod, Nombre = @nom, Descripcion = @desc, IdMarca = @idm, idCategoria = @idc, ImagenUrl = @img, Precio = @prec where Id = @id ";           
            try
            {
                datos.setearConsulta(consulta);
                datos.setearParametro("@cod", modificado.Codigo);
                datos.setearParametro("@nom", modificado.Nombre);
                datos.setearParametro("@desc", modificado.Descripcion);
                datos.setearParametro("@idm", modificado.Marca.Id);
                datos.setearParametro("@idc",modificado.Categoria.Id);
                datos.setearParametro("@img", modificado.ImagenUrl);
                datos.setearParametro("@prec", modificado.Precio);
                datos.setearParametro("@Id", modificado.Id);
                datos.ejecutarAccion();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            string consulta = "Delete from Articulos where Id = @id";
            try
            {

                datos.setearConsulta(consulta);
                datos.setearParametro("@id", id);
                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw (ex);
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public List<Articulo> filtrar(string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            string consulta = "select A.Id IdArticulo, C.Descripcion Categoria ,M.Descripcion Marca, Nombre, A.Descripcion Presentacion, Codigo, Precio, ImagenUrl from ARTICULOS A, MARCAS M, CATEGORIAS C where IdMarca = M.Id And IdCategoria = C.Id And ";
            try
            {
                if (criterio == "Mayor a ")
                    consulta += " Precio > " + filtro;
                else if (criterio == "Menor a ")
                    consulta += " Precio < " + filtro;
                else
                    consulta += " Precio = " + filtro; 
                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                while(datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["IdArticulo"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    aux.Marca = new Marca();
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Presentacion"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];


                    lista.Add(aux);
                }
                return lista;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            throw new NotImplementedException();
        }
    }
}
