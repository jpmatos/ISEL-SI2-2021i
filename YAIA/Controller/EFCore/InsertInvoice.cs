using System.Diagnostics;
using EFCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Controller.EFCore
{
    public static class InsertInvoice
    {
        public static void Execute(int nif, string name, string address)
        {
            try
            {
                using YAIA_Context ctx = new YAIA_Context();
                ctx.Database.ExecuteSqlRaw($"insertInvoice @p0, @p1, @p2", nif, name, address );
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }
    }
}