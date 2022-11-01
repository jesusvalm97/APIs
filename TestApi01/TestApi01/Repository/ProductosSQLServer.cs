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

        public async Task BorrarProductoAsync(string SKU)
        {
            SqlConnection sqlConnection = Conexion();
            SqlCommand sqlCommand = null;

            try
            {
                sqlConnection.Open();
                sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "dbo.EliminarProductos";
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.Add("@SKU", SqlDbType.VarChar, 50).Value = SKU;
                await sqlCommand.ExecuteNonQueryAsync();
            }
            catch (Exception exception)
            {
                throw new Exception("Se produjo un error al eliminar el producto: " + exception.ToString());
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            await Task.CompletedTask;
        }

        public async Task CrearProductoAsync(Producto producto)
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
                await sqlCommand.ExecuteNonQueryAsync();
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

            await Task.CompletedTask;
        }

        public async Task<Producto> GetProductoAsync(string SKU)
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
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

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

        public async Task<IEnumerable<Producto>> GetProductosAsync()
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
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

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

        public async Task ModificarProductoAsync(Producto producto)
        {
            SqlConnection sqlConnection = Conexion();
            SqlCommand sqlCommand = null;

            try
            {
                sqlConnection.Open();
                sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "dbo.ModificarProductos";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@Nombre", SqlDbType.VarChar, 500).Value = producto.Nombre;
                sqlCommand.Parameters.Add("@Descripcion", SqlDbType.VarChar, 5000).Value = producto.Descripcion;
                sqlCommand.Parameters.Add("@Precio", SqlDbType.Float).Value = producto.Precio;
                sqlCommand.Parameters.Add("@SKU", SqlDbType.VarChar, 50).Value = producto.SKU;
                await sqlCommand.ExecuteNonQueryAsync();
            }
            catch (Exception exception)
            {
                throw new Exception("Se produjo un error al modificar el producto: " + exception.ToString());
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            await Task.CompletedTask;
        }
    }
}
