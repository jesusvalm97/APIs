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
        private string CadenaConexion;

        public ProductosSQLServer(AccesoDatos accesoDatos)
        {
            CadenaConexion = accesoDatos.CadenaConexionSQL;
        }

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
            throw new NotImplementedException();
        }

        public IEnumerable<Producto> GetProductos()
        {
            throw new NotImplementedException();
        }

        public void ModificarProducto(Producto producto)
        {
            throw new NotImplementedException();
        }
    }
}
