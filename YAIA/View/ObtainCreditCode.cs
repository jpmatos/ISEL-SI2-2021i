using System;
using View.Interface;
using View.Util;

namespace View
{
    public class ObtainCreditCode : AbstractView
    {
        public override void Query(DataAccess dataAccess)
        {
            try
            {
                string optionStr = "";
                do
                {
                    Console.WriteLine("1. Invoice Code");
                    Console.WriteLine("2. Credit Note Code");
                    optionStr = Console.ReadLine();
                } while (optionStr != "1" && optionStr != "2");

                string code = "";
                switch (dataAccess)
                {
                    case DataAccess.Ado:
                        code = Controller.ADO.ObtainNextCode.Execute(Int32.Parse(optionStr) == 1
                            ? "invoice"
                            : "creditnote");
                        break;
                    case DataAccess.EfCore:
                        code = Controller.EFCore.ObtainNextCode.Execute(Int32.Parse(optionStr) == 1
                            ? "invoice"
                            : "creditnote");
                        break;
                    default:
                        Console.WriteLine($"Unknown data access '{dataAccess}'");
                        throw new Exception();
                }

                Console.WriteLine(code);
            }
            catch (Exception)
            {
                Console.WriteLine("Failed to obtain next code");
            }
        }
    }
}