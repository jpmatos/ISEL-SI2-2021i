using System.Data;
using System.Diagnostics;
using Mapper;
using Microsoft.Data.SqlClient;

namespace Controller.ADO
{
    public static class InsertItemToInvoice
    {
        public static void Execute(string invoiceValue, DataTable itemToAdd)
        {
            using Session s = new Session();
            bool isMyConnection = false;
            try
            {
                isMyConnection = s.OpenConnection();
                using SqlCommand cmd = s.CreateCommand();
                cmd.CommandText = "insertItemToInvoice";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@invoice", invoiceValue);
                cmd.Parameters.AddWithValue("@itemToAdd", itemToAdd);
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