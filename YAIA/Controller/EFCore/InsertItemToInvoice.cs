using System.Data;
using System.Diagnostics;
using EFCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Controller.EFCore
{
    public static class InsertItemToInvoice
    {
        public static void Execute(string invoice, DataTable itemToAdd)
        {
            try
            {
                using YAIA_Context ctx = new YAIA_Context();
                SqlParameter p1 = new SqlParameter("@p1", SqlDbType.Structured);
                p1.Value = itemToAdd;
                p1.TypeName = "[dbo].[ItemToAddListType]";
                ctx.Database.ExecuteSqlRaw("insertItemToInvoice @p0, @p1", invoice, p1);
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }
    }
}