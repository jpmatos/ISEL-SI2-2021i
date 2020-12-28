using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Linq;
using EFCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Controller.EFCore
{
    public class EmitInvoice
    {
        public static void Execute(int nifValue, string nameValue, string addressValue, DataTable itemToAddDataTable)
        {
            try
            {
                using YAIA_Context ctx = new YAIA_Context();
                
                InsertInvoice.Execute(nifValue, nameValue, addressValue);
                Invoice invoice = ctx.Invoices.FromSqlRaw("SELECT TOP 1 * FROM Invoice ORDER BY Invoice.creation_date DESC").AsNoTracking().First();
                InsertItemToInvoice.Execute(invoice.Code, itemToAddDataTable);
                
                
                SqlParameter code = new SqlParameter("@code", SqlDbType.NVarChar, 12);
                code.Value = invoice.Code;
                
                SqlParameter state = new SqlParameter("@state", SqlDbType.NVarChar, 16);
                state.Value = "emitted";
                
                ctx.Database.ExecuteSqlRaw($"updateInvoiceState {@code}, {@state}", code, state);
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }
    }
}