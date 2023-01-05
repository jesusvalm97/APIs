using APIUAGTEST.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;

namespace APIUAGTEST.BusinessLogic
{
    public class BLAcademia : IBLAcademia
    {
        private string ConnectionString;

        public BLAcademia()
        {
            ConnectionString = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=tcp)(HOST=orcdes.uag.mx)(PORT=1521))(CONNECT_DATA=(SERVER=dedicated)(SERVICE_NAME=DEVACDM)));User Id=4911449;Password=I3sus4nt3r0";
        }

        public IEnumerable<Clase> GetClases()
        {
            List<Clase> result = new List<Clase>();

            string queryString = @"SELECT DISTINCT STD.*
                                   FROM SYSADM.PS_STDNT_ENRL STD
                                   WHERE STD.EMPLID = '3386147' AND 
                                         STD.CRSE_CAREER <> 'IDI'";

            using (OracleConnection connection = new OracleConnection(ConnectionString))
            {
                OracleCommand command = new OracleCommand();
                command.Connection = connection;
                command.CommandText = queryString;
                command.CommandType = System.Data.CommandType.Text;

                connection.Open();

                OracleDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    //result.Add(new Clase()
                    //{
                    //    EmplId = reader["EMPLID"].ToString(),
                    //    AcadCareer = reader["ACAD_CAREER"].ToString(),
                    //    ClassNbr = reader["CLASS_NBR"].ToString(),
                    //    GradeCategory = reader["GRADE_CATEGORY"].ToString(),
                    //    Institution = reader["INSTITUTION"].ToString(),
                    //    STRM = reader["STRM"].ToString()
                    //});
                }

                connection.Close();
            }

            //using (OracleConnection connection = new OracleConnection(ConnectionString))

            //using (OracleConnection connection = new OracleConnection())
            //{
            //    connection.ConnectionString = ConnectionString;
            //    OracleCommand command = new OracleCommand(queryString);
            //    command.Connection = connection;
            //    try
            //    {
            //        connection.Open();
            //        command.ExecuteNonQuery();

            //        var reader = command.ExecuteReader();

            //        if (reader.Read())
            //        {
            //            result.Add(new Clase()
            //            {
            //                EmplId = reader["EMPLID"].ToString(),
            //                AcadCareer = reader["ACAD_CAREER"].ToString(),
            //                ClassNbr = reader["CLASS_NBR"].ToString(),
            //                GradeCategory = reader["GRADE_CATEGORY"].ToString(),
            //                Institution = reader["INSTITUTION"].ToString(),
            //                STRM = reader["STRM"].ToString()
            //            });
            //        }
            //    }
            //    catch (Exception ex)
            //    {

            //    }
            //}

            return result;
        }
    }
}
