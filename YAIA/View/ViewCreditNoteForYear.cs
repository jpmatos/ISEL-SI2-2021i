using System;
using System.Collections;
using System.Data.Common;
using EFCore;
using View.Interface;
using View.Util;

namespace View
{
    public class ViewCreditNoteForYear : AbstractView
    {
        public override void Query(DataAccess dataAccess)
        {
            Parameter year = new Parameter("Year", typeof(int), false);
            try
            {
                year.Value = Input(year);
                IEnumerator result;
                switch (dataAccess)
                {
                    case DataAccess.Ado:
                        result = Controller.ADO.ViewCreditNoteForYear.Execute(Int32.Parse(year.Value));
                        PrintResultADO(result);
                        break;
                    case DataAccess.EfCore:
                        result = Controller.EFCore.ViewCreditNoteForYear.Execute(Int32.Parse(year.Value));
                        PrintResultEfCore(result);
                        break;
                    default:
                        Console.WriteLine($"Unknown data access '{dataAccess}'");
                        throw new Exception();
                }

                Console.WriteLine("Successfully queried invoices");
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to query invoices");
            }
        }

        // Not Generic
        private void PrintResultEfCore(IEnumerator result)
        {
            Console.WriteLine();
            bool firstTime = true;
            while (result.MoveNext())
            {
                if (firstTime)
                {
                    Console.WriteLine("code codeInvoice state total_value total_IVA creation_date emission_date");
                    firstTime = false;
                }

                CreditNote current = (CreditNote) result.Current;
                Console.WriteLine(
                    $"{current.Code} {current.CodeInvoice} {current.State} {current.TotalValue} {current.TotalIva} {current.CreationDate} {current.EmissionDate}");
            }

            Console.WriteLine();
        }
    }
}