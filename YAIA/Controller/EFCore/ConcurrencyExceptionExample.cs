using System;
using System.Data;
using System.Diagnostics;
using System.Linq;
using EFCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Controller.EFCore
{
    public static class ConcurrencyExceptionExample
    {
        public static bool Execute(string invoice1Value, string invoice2Value, bool forceException)
        {
            try
            {
                using YAIA_Context ctx = new YAIA_Context();
                
                Console.WriteLine("Reading invoices from DB...");
                Invoice invoice1 = ctx.Invoices.Single(i => i.Code == invoice1Value);
                Invoice invoice2 = ctx.Invoices.Single(i => i.Code == invoice2Value);

                Console.WriteLine("Changing NIF values...");
                int aux = invoice1.Nif;
                invoice1.Nif = invoice2.Nif;
                invoice2.Nif = aux;

                if (forceException)
                {
                    SqlParameter invoice = new SqlParameter("@invoice", SqlDbType.NVarChar, 12);
                    invoice.Value = invoice1Value;
                    
                    SqlParameter total_value = new SqlParameter("@total_value", SqlDbType.Money);
                    total_value.Value = new Random().Next(1, 10000);
                    
                    Console.WriteLine("Updating 'total_value' before calling SavingChanges()...");
                    ctx.Database.ExecuteSqlRaw($"UPDATE Invoice SET total_value = {@total_value} WHERE code = '{@invoice1Value}'", invoice, total_value);
                }

                Console.WriteLine("Calling SaveChanges()...");
                ctx.SaveChanges();
                
                return false;
            }
            catch (SqlException e)
            {
                Debug.WriteLine(e.Message);
                throw;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Debug.WriteLine(ex.Message);
                return true;
            }
        }
    }
}