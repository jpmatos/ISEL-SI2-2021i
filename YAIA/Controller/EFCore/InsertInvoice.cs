using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using EFCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Controller.EFCore
{
    public static class InsertInvoice
    {
        public static void Execute(int nifValue, string nameValue, string addressValue)
        {
            try
            {
                using YAIA_Context ctx = new YAIA_Context();
                
                SqlParameter nif = new SqlParameter("@nif", SqlDbType.Int);
                nif.Value = nifValue;
                
                SqlParameter name = new SqlParameter("@name", SqlDbType.NVarChar);
                name.Value = nameValue ?? SqlString.Null;
                
                SqlParameter address = new SqlParameter("@address", SqlDbType.NVarChar);
                address.Value = addressValue ?? SqlString.Null;
                
                ctx.Database.ExecuteSqlRaw($"insertInvoice {@nif}, {@name}, {@address}", nif, name, address);
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }
    }
}