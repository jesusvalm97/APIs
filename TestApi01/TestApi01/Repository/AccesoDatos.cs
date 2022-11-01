using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestApi01.Repository
{
    public class AccesoDatos
    {
        private string cadenaConexionSQL;

        public string CadenaConexionSQL { get => cadenaConexionSQL; }

        public AccesoDatos(string ConexionSQL)
        {
            cadenaConexionSQL = ConexionSQL;
        }
    }
}
