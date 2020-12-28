using System;
using System.Collections.Generic;
using System.Data;
using Entity.TableTypes;
using View.Interface;
using View.Util;

namespace View
{
    public class InsertItemToInvoice : AbstractView
    {
        public override void Query(DataAccess dataAccess)
        {
            Parameter invoice = new Parameter("Invoice", typeof(string), false);
            try
            {
                invoice.Value = Input(invoice);
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

                switch (dataAccess)
                {
                    case DataAccess.Ado:
                        Controller.ADO.InsertItemToInvoice.Execute(invoice.Value, itemToAddDataTable);
                        break;
                    case DataAccess.EfCore:
                        Controller.EFCore.InsertItemToInvoice.Execute(invoice.Value, itemToAddDataTable);
                        break;
                    default:
                        Console.WriteLine($"Unknown data access '{dataAccess}'");
                        throw new Exception();
                }

                Console.WriteLine("Successfully inserted items in Invoice");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to insert items in Invoice");
            }
        }
    }
}