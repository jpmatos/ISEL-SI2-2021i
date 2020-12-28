using System.Data;
using System.Diagnostics;
using EFCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Controller.EFCore
{
    public static class InsertCreditNote
    {
        public static void Execute(string invoiceValue, DataTable itemListValue)
        {
            try
            {
                using YAIA_Context ctx = new YAIA_Context();

                SqlParameter invoice = new SqlParameter("@invoice", SqlDbType.NVarChar);
                invoice.Value = invoiceValue;

                SqlParameter itemList = new SqlParameter("@itemList", SqlDbType.Structured);
                itemList.Value = itemListValue;
                itemList.TypeName = "[dbo].[ItemListType]";

                ctx.Database.ExecuteSqlRaw($"insertCreditNote {@invoice}, {@itemList}", invoice, itemList);
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }
    }
}