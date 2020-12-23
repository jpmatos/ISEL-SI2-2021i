using System;
using System.Collections;
using System.Data;
using System.Diagnostics;
using Mapper;
using Microsoft.Data.SqlClient;

namespace Controller.ADO
{
    public static class ViewCreditNoteForYear
    {
        public static IEnumerator Execute(int year)
        {
            using Session s = new Session();
            bool isMyConnection = false;
            try
            {
                isMyConnection = s.OpenConnection();
                
                using SqlCommand cmd = s.CreateCommand();
                cmd.CommandText = "SELECT * FROM viewCreditNoteYear(@year)";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@year", year);

                SqlDataReader reader = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(reader);
                DataTableReader res = new DataTableReader(table);
                return res.GetEnumerator();
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