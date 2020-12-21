using System;
using View.Abstract;
using EFCore;
using Microsoft.Data.SqlClient;

namespace View.EFCore
{
    public class InsertInvoice : AbstractInsertInvoice, IView
    {
        public void Query()
        {
            string[] results = Input(Name, Parameters);

            Contributor contributor = new Contributor();
            contributor.Nif = Convert.ToInt32(results[0]);
            contributor.Name = results[1];
            contributor.Address = results[2];

            Invoice invoice = new Invoice();
            invoice.NifNavigation = contributor;

            try
            {
                Controller.EFCore.InsertInvoice.Execute(invoice);
                Console.WriteLine("Successfully created Invoice");
            }
            catch (SqlException)
            {
                Console.WriteLine("Failed to create Invoice");
            }
        }
    }
}