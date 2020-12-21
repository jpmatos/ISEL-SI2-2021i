using System;
using System.Collections.Generic;
using Entity;
using Controller;
using Microsoft.Data.SqlClient;
using View.Abstract;

namespace View.ADO
{
    public class InsertInvoice : AbstractInsertInvoice, IView
    {
        public void Query()
        {
            string[] results = Input(Name, Parameters);
            
            Contributor contributor = new Contributor();
            contributor.NIF = Convert.ToInt32(results[0]);
            contributor.Name = results[1];
            contributor.Address = results[2];
            
            Invoice invoice = new Invoice();
            invoice.Contributor = contributor;

            try
            {
                Controller.ADO.InsertInvoice.Execute(invoice);
                Console.WriteLine("Successfully created Invoice");
            }
            catch (SqlException)
            {
                Console.WriteLine("Failed to create Invoice");
            }
        }
    }
}