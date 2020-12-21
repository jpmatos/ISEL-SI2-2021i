using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using Entity;
using Mapper;
using Microsoft.Data.SqlClient;

namespace Controller.ADO
{
    public static class InsertInvoice
    {
        public static void Execute(Invoice invoice)
        {
            using Session s = new Session();
            bool isMyConnection = false;
            try
            {
                isMyConnection = s.OpenConnection();
                using SqlCommand cmd = s.CreateCommand();
                cmd.CommandText = "insertInvoice";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@NIF", invoice.Contributor.NIF);
                cmd.Parameters.AddWithValue("@name", invoice.Contributor.Name ?? SqlString.Null);
                cmd.Parameters.AddWithValue("@address", invoice.Contributor.Address ?? SqlString.Null);
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