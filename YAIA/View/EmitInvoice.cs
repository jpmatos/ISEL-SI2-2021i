using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using Entity.TableTypes;
using View.Interface;
using View.Util;

namespace View
{
    public class EmitInvoice : AbstractView
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

                List<ItemToAddList> itemToAdd = InputItemToAddList();
                if (itemToAdd.Count == 0)
                {
                    Console.WriteLine("Item list can't be empty!");
                    return;
                }

                DataTable itemToAddDataTable = new DataTable();
                itemToAddDataTable.Columns.Add("SKU", typeof(string));
                itemToAddDataTable.Columns.Add("units", typeof(int));
                itemToAddDataTable.Columns.Add("discount", typeof(decimal));
                itemToAddDataTable.Columns.Add("description", typeof(string));

                foreach (var item in itemToAdd)
                    itemToAddDataTable.Rows.Add(item.Sku, item.Units, item.Discount, item.Description);

                Stopwatch stopWatch = new Stopwatch();
                stopWatch.Start();
                switch (dataAccess)
                {
                    case DataAccess.Ado:
                        Controller.ADO.EmitInvoice.Execute(Convert.ToInt32(nif.Value), name.Value, address.Value,
                            itemToAddDataTable);
                        break;
                    case DataAccess.EfCore:
                        Controller.EFCore.EmitInvoice.Execute(Convert.ToInt32(nif.Value), name.Value, address.Value,
                            itemToAddDataTable);
                        break;
                    default:
                        Console.WriteLine($"Unknown data access '{dataAccess}'");
                        throw new Exception();
                }

                stopWatch.Stop();
                Console.WriteLine("Successfully emitted invoice.");
                Console.WriteLine($"Time: '{stopWatch.Elapsed.Milliseconds}ms'");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to emit Invoice");
            }
        }
    }
}