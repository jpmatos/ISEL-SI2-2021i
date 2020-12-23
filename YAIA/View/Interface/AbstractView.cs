using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Text.RegularExpressions;
using View.Util;

namespace View.Interface
{
    public abstract class AbstractView : IView
    {
        protected static readonly Regex RegexString = new Regex(@"^[a-zA-Z0-9 _-]*$");
        protected static readonly Regex RegexInteger = new Regex(@"^[0-9,.]*$");

        public abstract void Query(DataAccess e);

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
                DbDataRecord record = (DbDataRecord)result.Current;
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
    }
}