using System;
using View.Interface;
using View.Util;

namespace View
{
    public class ConcurrencyExceptionExample : AbstractView
    {
        public override void Query(DataAccess dataAccess)
        {
            Parameter invoice1 = new Parameter("Invoice 1", typeof(string), false);
            Parameter invoice2 = new Parameter("Invoice 2", typeof(string), false);

            try
            {
                invoice1.Value = Input(invoice1);
                invoice2.Value = Input(invoice2);

                string option = "0";
                do
                {
                    Console.Clear();
                    Console.WriteLine("Force DbUpdateConcurrencyException?");
                    Console.WriteLine("This is done by executing an UPDATE statement in one of the invoices before context is saved.");
                    Console.WriteLine("The updated field is 'total_value' which has the annotation '[ConcurrencyCheck]'.");
                    Console.WriteLine("This annotation will cause a DbUpdateConcurrencyException when updated in an Entity in the Modified state.");
                    Console.WriteLine("1. Yes");
                    Console.WriteLine("2. No");
                    option = Console.ReadLine();
                } while (option != "1" && option != "2");

                bool isConcurrencyException =
                    Controller.EFCore.ConcurrencyExceptionExample.Execute(invoice1.Value, invoice2.Value,
                        option == "1");

                if (isConcurrencyException)
                    Console.WriteLine("A DbUpdateConcurrencyException was thrown! Check Debug output for exception details.");
                else
                    Console.WriteLine("No exception was thrown! NIF values were successfully swapped.");

                Console.WriteLine("Example demonstration finished.");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to demonstrate example.");
            }
        }
    }
}