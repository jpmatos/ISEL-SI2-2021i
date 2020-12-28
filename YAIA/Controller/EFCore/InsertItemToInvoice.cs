using System.Data;
using System.Diagnostics;
using EFCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Controller.EFCore
{
    public static class InsertItemToInvoice
    {
        public static void Execute(string invoiceValue, DataTable itemToAddList)
        {
            try
            {
                using YAIA_Context ctx = new YAIA_Context();

                SqlParameter invoice = new SqlParameter("@invoice", SqlDbType.NVarChar);
                invoice.Value = invoiceValue;

                SqlParameter itemToAdd = new SqlParameter("@itemToAdd", SqlDbType.Structured);
                itemToAdd.Value = itemToAddList;
                itemToAdd.TypeName = "[dbo].[ItemToAddListType]";

                ctx.Database.ExecuteSqlRaw($"insertItemToInvoice {@invoice}, {@itemToAdd}", invoice, itemToAdd);
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }
    }
}