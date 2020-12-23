using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using EFCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Controller.EFCore
{
    public static class ViewCreditNoteForYear
    {
        public static IEnumerator Execute(int yearValue)
        {
            try
            {
                using YAIA_Context ctx = new YAIA_Context();
                SqlParameter year = new SqlParameter("@year", yearValue);
                List<CreditNote> result = ctx.CreditNotes.FromSqlRaw($"SELECT * FROM viewCreditNoteYear({@year})", year).AsNoTracking().ToList();
                return result.GetEnumerator();
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }
    }
}