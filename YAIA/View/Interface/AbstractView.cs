using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Text.RegularExpressions;
using Entity.TableTypes;
using View.Util;

namespace View.Interface
{
    public abstract class AbstractView : IView
    {
        protected static readonly Regex RegexString = new Regex(@"^[a-zA-Z0-9 _-]*$");
        protected static readonly Regex RegexInteger = new Regex(@"^[0-9,.]*$");

        public abstract void Query(DataAccess dataAccess);

        protected static string Input(Parameter parameter)
        {
            bool cont = true;
            string read;
            do
            {
                Console.WriteLine($"{parameter.Name} {(parameter.Nullable ? "" : "(not null)")}");
                read = Console.ReadLine();

                if (read.Equals("null", StringComparison.InvariantCultureIgnoreCase) || read.Equals(""))
                {
                    if (!parameter.Nullable)
                        continue;

                    read = null;
                }

                cont = false;

                if (read != null)
                {
                    if (parameter.Type == typeof(string) && !RegexString.IsMatch(read) ||
                        parameter.Type == typeof(int) && !RegexInteger.IsMatch(read))
                    {
                        Console.WriteLine($"Invalid argument: {read}");
                        throw new ArgumentException($"Invalid argument: {read}");
                    }
                }
            } while (cont);

            return read;
        }


        protected void PrintResultADO(IEnumerator result)
        {
            Console.WriteLine();
            bool firstTime = true;
            while (result.MoveNext())
            {
                DbDataRecord record = (DbDataRecord) result.Current;
                if (firstTime)
                {
                    printHeader(record);
                    firstTime = false;
                }

                string print = "";
                for (int i = 0; i < record.FieldCount; i++)
                {
                    print += record.GetValue(i) + " ";
                }

                Console.WriteLine(print);
            }

            Console.WriteLine();
        }

        private void printHeader(DbDataRecord record)
        {
            string print = "";
            for (int i = 0; i < record.FieldCount; i++)
            {
                print += record.GetName(i) + " ";
            }

            Console.WriteLine(print);
        }


        protected List<ItemToAddList> InputItemToAddList()
        {
            List<ItemToAddList> res = new List<ItemToAddList>();
            bool cont = true;
            do
            {
                Console.WriteLine("SKU (enter to quit)");
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

                Console.WriteLine("Quantity (not null)");
                string quantity = Console.ReadLine();

                if (!RegexInteger.IsMatch(quantity) ||
                    quantity.Equals("null", StringComparison.InvariantCultureIgnoreCase) || quantity.Equals("") ||
                    quantity.Equals("0"))
                {
                    Console.WriteLine($"Invalid quantity '{quantity}'");
                    continue;
                }

                Console.WriteLine("Discount");
                string discount = Console.ReadLine().Replace(",", ".");

                if (discount.Equals(""))
                    discount = "0";

                if (!RegexInteger.IsMatch(discount))
                {
                    Console.WriteLine($"Invalid discount '{discount}'");
                    continue;
                }

                Console.WriteLine("Description");
                string description = Console.ReadLine();

                if (!RegexString.IsMatch(sku))
                {
                    Console.WriteLine($"Invalid description '{description}'");
                    continue;
                }

                res.Add(new ItemToAddList(sku, Int32.Parse(quantity), float.Parse(discount), description));
            } while (cont);

            return res;
        }
    }
}