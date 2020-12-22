using System;
using Microsoft.Data.SqlClient;
using View.Interface;
using View.Util;

namespace View
{
    public class InsertInvoice : AbstractView
    {
        public override void Query(DataAccess dataAccess)
        {
            Parameter nif = new Parameter("NIF", typeof(int), false);
            Parameter name = new Parameter("Name", typeof(string), true);
            Parameter address = new Parameter("Address", typeof(string), true);
            
            try
            {
                nif.Value = Input(nif);
                name.Value = Input(name);
                address.Value = Input(address);
                switch (dataAccess)
                {
                    case DataAccess.Ado:
                        Controller.ADO.InsertInvoice.Execute(Convert.ToInt32(nif.Value), name.Value, address.Value);
                        break;
                    case DataAccess.EfCore:
                        Controller.EFCore.InsertInvoice.Execute(Convert.ToInt32(nif.Value), name.Value, address.Value);
                        break;
                    default:
                        Console.WriteLine($"Unknown data access '{dataAccess}'");
                        throw new Exception();
                }
                Console.WriteLine("Successfully created Invoice");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to create Invoice");
            }
        }
    }
}