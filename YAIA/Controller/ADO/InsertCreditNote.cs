using System.Data;
using System.Diagnostics;
using Mapper;
using Microsoft.Data.SqlClient;

namespace Controller.ADO
{
    public static class InsertCreditNote
    {
        public static void Execute(string invoiceValue, DataTable itemList)
        {
            using Session s = new Session();
            bool isMyConnection = false;
            try
            {
                isMyConnection = s.OpenConnection();

                using SqlCommand cmd = s.CreateCommand();
                cmd.CommandText = "insertCreditNote";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@invoice", invoiceValue);
                cmd.Parameters.AddWithValue("@itemList", itemList);

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