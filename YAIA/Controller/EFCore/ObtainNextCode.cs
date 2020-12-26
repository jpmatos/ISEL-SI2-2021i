using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using EFCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Controller.EFCore
{
    public static class ObtainNextCode
    {
        public static string Execute(string option)
        {
            try
            {
                using YAIA_Context ctx = new YAIA_Context();
                switch (option)
                {
                    case "invoice":
                        List<Invoice> invoices = ctx.Invoices
                            .FromSqlRaw("SELECT * FROM Invoice WHERE YEAR(Invoice.creation_date) = YEAR(GETDATE())")
                            .AsNoTracking().ToList();
                        return "FT" + DateTime.Now.Year + "-" + (invoices.Count + 1);
                    case "creditnote":
                        List<CreditNote> creditNotes = ctx.CreditNotes
                            .FromSqlRaw("SELECT * FROM CreditNote WHERE YEAR(CreditNote.creation_date) = YEAR(GETDATE())")
                            .AsNoTracking().ToList();
                        return "NT" + DateTime.Now.Year + "-" + (creditNotes.Count + 1);
                    default:
                        Debug.WriteLine($"Invalid option '{option}'");
                        return "INVALID";
                }
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
        }
    }
}