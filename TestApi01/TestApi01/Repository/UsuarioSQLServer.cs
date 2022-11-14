using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TestApi01.Models;

namespace TestApi01.Repository
{
    public class UsuarioSQLServer : IUsuariosSQLServer
    {
        //Vaeriable para guardar la conexión a la base de datos
        private string CadenaConexion;
        private readonly ILogger<UsuarioSQLServer> log;

        #region Constructores

        public UsuarioSQLServer(AccesoDatos accesoDatos, ILogger<UsuarioSQLServer> l)
        {
            CadenaConexion = accesoDatos.CadenaConexionSQL;
            log = l;
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

        public async Task<UsuarioAPI> GetUsuario(LoginAPI login)
        {
            UsuarioAPI usuarioAPI = null;

            SqlConnection sqlConnection = Conexion();
            SqlCommand sqlCommand = null;

            try
            {
                sqlConnection.Open();
                sqlCommand = sqlConnection.CreateCommand();
                sqlCommand.CommandText = "dbo.UsuarioAPIObtener";
                sqlCommand.CommandType = CommandType.StoredProcedure;

                sqlCommand.Parameters.Add("@UsuarioAPI", SqlDbType.VarChar, 500).Value = login.UsuarioAPI;
                sqlCommand.Parameters.Add("@PassAPI", SqlDbType.VarChar, 50).Value = login.PassAPI;
                SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();

                if (sqlDataReader.Read())
                {
                    usuarioAPI = new UsuarioAPI();
                    usuarioAPI.Usuario = sqlDataReader["UsuarioAPI"].ToString();
                    usuarioAPI.Email = sqlDataReader["EmailUsuario"].ToString();
                }
            }
            catch (Exception exception)
            {
                Tools.Error(exception, log);
            }
            finally
            {
                sqlCommand.Dispose();
                sqlConnection.Close();
                sqlConnection.Dispose();
            }

            return usuarioAPI;
        }
    }
}
