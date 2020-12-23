using System;
using System.Collections.Generic;
using System.Data;
using Entity.TableTypes;
using View.Interface;
using View.Util;

namespace View
{
    public class InsertCreditNote : AbstractView
    {
        public override void Query(DataAccess dataAccess)
        {
            Parameter invoice = new Parameter("Invoice", typeof(string), false);
            try
            {
                invoice.Value = Input(invoice);
                List<ItemList> itemList = InputItemList();

                if (itemList.Count == 0)
                {
                    Console.WriteLine("Item list can't be empty!");
                    return;
                }

                DataTable itemListDataTable = new DataTable();
                itemListDataTable.Columns.Add("SKU", typeof(string));
                itemListDataTable.Columns.Add("quantity", typeof(int));

                foreach (var item in itemList)
                    itemListDataTable.Rows.Add(item.Sku, item.Quantity);
                
                switch (dataAccess)
                {
                    case DataAccess.Ado:
                        Controller.ADO.InsertCreditNote.Execute(invoice.Value, itemListDataTable);
                        break;
                    case DataAccess.EfCore:
                        Controller.EFCore.InsertCreditNote.Execute(invoice.Value, itemListDataTable);
                        break;
                    default:
                        Console.WriteLine($"Unknown data access '{dataAccess}'");
                        throw new Exception();
                }

                Console.WriteLine("Successfully created Credit Note");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to create Credit Note");
            }
        }

        private List<ItemList> InputItemList()
        {
            List<ItemList> res = new List<ItemList>();
            bool cont = true;
            do
            {
                Console.WriteLine($"SKU (enter to quit)");
                string sku = Console.ReadLine();

                if (sku.Equals("null", StringComparison.InvariantCultureIgnoreCase) || sku.Equals(""))
                {
                    cont = false;
                    continue;
                }

                if (!RegexString.IsMatch(sku))
                {
                    Console.WriteLine($"Invalid SKU '{sku}'");
                    continue;
                }

                Console.WriteLine($"Quantity (not null)");
                string quantity = Console.ReadLine();

                if (!RegexInteger.IsMatch(quantity) || 
                    quantity.Equals("null", StringComparison.InvariantCultureIgnoreCase) || quantity.Equals("") || quantity.Equals("0"))
                {
                    Console.WriteLine($"Invalid quantity '{quantity}'");
                    continue;
                }

                res.Add(new ItemList(sku, Int32.Parse(quantity)));
            } while (cont);

            return res;
        }
    }
}