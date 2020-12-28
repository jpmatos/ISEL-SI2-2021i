using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using Mapper;
using Microsoft.Data.SqlClient;

namespace Controller.ADO
{
    public static class InsertInvoice
    {
        public static void Execute(int nif, string name, string address)
        {
            using Session s = new Session();
            bool isMyConnection = false;
            try
            {
                isMyConnection = s.OpenConnection();

                using SqlCommand cmd = s.CreateCommand();
                cmd.CommandText = "insertInvoice";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NIF", nif);
                cmd.Parameters.AddWithValue("@name", name ?? SqlString.Null);
                cmd.Parameters.AddWithValue("@address", address ?? SqlString.Null);

                cmd.ExecuteReader();
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
            finally
            {
                s.CloseConnection(isMyConnection);
            }
        }
    }
}