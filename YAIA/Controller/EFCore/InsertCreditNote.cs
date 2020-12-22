using System.Data;
using System.Diagnostics;
using EFCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Controller.EFCore
{
    public static class InsertCreditNote
    {
        public static void Execute(string invoice, DataTable itemList)
        {

            try
            {
                using YAIA_Context ctx = new YAIA_Context();
                SqlParameter p1 = new SqlParameter("@p1", SqlDbType.Structured);
                p1.Value = itemList;
                p1.TypeName = "[dbo].[ItemListType]";
                ctx.Database.ExecuteSqlRaw("insertCreditNote @p0, @p1", invoice, p1);
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }
    }
}