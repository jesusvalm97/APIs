using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using TestApi01.Models;

namespace TestApi01.Repository
{
    public class ProductosSQLServer : IProductosEnMemoria
    {
        //Vaeriable para guardar la conexión a la base de datos
        private string CadenaConexion;

        #region Constructores

        public ProductosSQLServer(AccesoDatos accesoDatos)
        {
            CadenaConexion = accesoDatos.CadenaConexionSQL;
        }

        #endregion

        /// <summary>
        /// Crear una conexión de SQL en base a la variable CadenaConexion
        /// </summary>
        /// <returns></returns>
        private SqlConnection Conexion()
        {
            return new SqlConnection(CadenaConexion);
        }

        public void BorrarProducto(string SKU)
        {
            throw new NotImplementedException();
        }

        public void CrearProducto(Producto producto)
        {
            SqlConnection sqlConnection = Conexion();
            SqlCommand sqlCommand = null;

            try
            {
                sqlConnection.Open();
                sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "dbo.ProductosAlta";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@Nombre", SqlDbType.VarChar, 500).Value = producto.Nombre;
                sqlCommand.Parameters.Add("@Descripcion", SqlDbType.VarChar, 5000).Value = producto.Descripcion;
                sqlCommand.Parameters.Add("@Precio", SqlDbType.Float).Value = producto.Precio;
                sqlCommand.Parameters.Add("@SKU", SqlDbType.VarChar, 50).Value = producto.SKU;
                sqlCommand.ExecuteNonQuery();
            }
            catch (Exception exception)
            {
                throw new Exception("Se produjo un error al dar de alta: " + exception.ToString());
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }
        }

        public Producto GetProducto(string SKU)
        {
            Producto producto = null;

            SqlConnection sqlConnection = Conexion();
            SqlCommand sqlCommand = null;

            try
            {
                sqlConnection.Open();
                sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "dbo.ObtenerProductos";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@SKU", SqlDbType.VarChar, 50).Value = SKU;
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                if (sqlDataReader.Read())
                {
                    producto = new Producto();
                    producto.Nombre = sqlDataReader["Nombre"].ToString();
                    producto.Descripcion = sqlDataReader["Descripcion"].ToString();
                    producto.Precio = double.Parse(sqlDataReader["Precio"].ToString());
                    producto.SKU = sqlDataReader["SKU"].ToString();
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Se produjo un error al dar de alta: " + exception.ToString());
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return producto;
        }

        public IEnumerable<Producto> GetProductos()
        {
            List<Producto> productos = new List<Producto>();
            Producto producto = null;

            SqlConnection sqlConnection = Conexion();
            SqlCommand sqlCommand = null;

            try
            {
                sqlConnection.Open();
                sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "dbo.ObtenerProductos";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                while (sqlDataReader.Read())
                {
                    producto = new Producto();
                    producto.Nombre = sqlDataReader["Nombre"].ToString();
                    producto.Descripcion = sqlDataReader["Descripcion"].ToString();
                    producto.Precio = double.Parse(sqlDataReader["Precio"].ToString());
                    producto.SKU = sqlDataReader["SKU"].ToString();
                    productos.Add(producto);
                }
            }
            catch (Exception exception)
            {
                throw new Exception("Se produjo un error al dar de alta: " + exception.ToString());
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return productos;
        }

        public void ModificarProducto(Producto producto)
        {
            throw new NotImplementedException();
        }
    }
}
