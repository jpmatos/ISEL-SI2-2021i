using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using Entity;
using IMapper;
using Mapper;
using Microsoft.Data.SqlClient;

namespace Controller.ADO
{
    public class EmitInvoice
    {
        public static void Execute(int nif, string name, string address, DataTable itemToAddDataTable)
        {
            using Session s = new Session();
            bool isMyConnection = false;
            try
            {
                isMyConnection = s.OpenConnection();

                IMapperInvoice iMap = s.CreateMapperInvoice();
                InsertInvoice.Execute(nif, name, address);
                string invoice = iMap.GetLatestInvoiceCode();
                InsertItemToInvoice.Execute(invoice, itemToAddDataTable);
                
                using SqlCommand cmd = s.CreateCommand();
                cmd.CommandText = "updateInvoiceState";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@code", invoice);
                cmd.Parameters.AddWithValue("@state", "emitted");
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