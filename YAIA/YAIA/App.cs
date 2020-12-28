using System;
using System.Collections.Generic;
using View;
using View.Util;

namespace YAIA
{ 
    public class App
    {
        
        private delegate void DbMethod(DataAccess data);
        private readonly Dictionary<Option, DbMethod> _dbMethods;  
             
        private static App _instance;
        public static App Instance
        {
            get => _instance ??= new App();
            private set => _instance = value;
        }

        private const DataAccess DefaultData = DataAccess.Ado;

        private DataAccess DataAccess
        {
            get;
            set;
        }

        private enum Option
        {
            Unknown = -1,
            Exit,
            InsertInvoice,
            InsertCreditNote,
            InsertItems,
            UpdateInvoiceValue,
            ViewCreditNoteForYear,
            ObtainCode,
            EmitInvoice,
            ConcurrencyException,
            SwitchData
        }
        
        private App()
        {
            _dbMethods = new Dictionary<Option, DbMethod>();
            _dbMethods.Add(Option.InsertInvoice, new InsertInvoice().Query);
            _dbMethods.Add(Option.InsertCreditNote, new InsertCreditNote().Query);
            _dbMethods.Add(Option.InsertItems, new InsertItemToInvoice().Query);
            _dbMethods.Add(Option.UpdateInvoiceValue, new UpdateInvoiceValue().Query);
            _dbMethods.Add(Option.ViewCreditNoteForYear, new ViewCreditNoteForYear().Query);
            _dbMethods.Add(Option.ObtainCode, new ObtainCreditCode().Query);
            _dbMethods.Add(Option.EmitInvoice, new EmitInvoice().Query);
            _dbMethods.Add(Option.ConcurrencyException, new ConcurrencyExceptionExample().Query);
            _dbMethods.Add(Option.SwitchData, SwitchData);
        }
        
        private Option DisplayMenu()
        {
            Option option = Option.Unknown;
            try
            {
                Console.WriteLine("Yet Another Invoice Application");
                Console.WriteLine("1. f.Insert Invoice");
                Console.WriteLine("2. g.Insert Credit Note");
                Console.WriteLine("3. h.Insert Items In Invoice");
                Console.WriteLine("4. i.Update Invoice Value");
                Console.WriteLine("5. j.View Credit Note For Year");
                Console.WriteLine("6. 1.b) Obtain Next Invoice Or Credit Note Code");
                Console.WriteLine("7. 1.c) Emit Invoice");
                Console.WriteLine("8. 4) Force Concurrency Exception In EFCore Optimist Locking");
                Console.WriteLine($"9. ---Switch Data Access--- (Current: {DataAccess.ToString().ToUpper()})");
                Console.WriteLine("0. Exit");
                var result = Console.ReadLine();
                option = (Option)Enum.Parse(typeof(Option), result ?? string.Empty);
            }
            catch (ArgumentException)
            {
                //Nothing to do. The option was not a valid one. Read another.
            }

            return option;
        }
        
        private void SwitchData(DataAccess _)
        {
            DataAccess = DataAccess == DataAccess.Ado ? DataAccess.EfCore : DataAccess.Ado;
            Console.WriteLine($"Switched to {DataAccess.ToString().ToUpper()}");
        }
        
        public void Run()
        {
            DataAccess = DefaultData;
            Option userInput;
            do
            {
                Console.Clear();
                userInput = DisplayMenu();
                Console.Clear();
                try
                {
                    _dbMethods[userInput](DataAccess);
                    Console.WriteLine("Press Enter to continue...");
                    Console.ReadKey();
                }
                catch (KeyNotFoundException)
                {
                    //Nothing to do. The option was not a valid one. Read another.
                }

            } while (userInput != Option.Exit);
        }
    }
}


//ADO Test
// using (var s = new Session())
// {
//     IMapperContributor map = s.CreateMapperContributor();
//     Entity.Contributor contributor = map.Read(123);
//     Console.WriteLine(contributor);
// }

//EFCore Test
// using (var ctx = new YAIA_Context())
// {
//     var contributor =
//         (from c in ctx.Contributors.AsNoTracking() where c.Nif == 123 select c).SingleOrDefault();
//     Console.WriteLine(contributor.Name);

// // Call Function 1:
//  var resultParameter = new SqlParameter("@result", SqlDbType.VarChar);
//  resultParameter.Size = 2000; // some meaningful value
//  resultParameter.Direction = ParameterDirection.Output;
//  ctx.Database.ExecuteSqlRaw("set @result = dbo.nextCode({0});", "invoice", resultParameter);
//  var result = resultParameter.Value as string;
//  Console.WriteLine(result);

// // Call Function 2:
// DbConnection connection = ctx.Database.GetDbConnection();
// using(DbCommand cmd = connection.CreateCommand()) {
//     cmd.CommandText = "select dbo.nextCode('invoice')";
//     if (connection.State.Equals(ConnectionState.Closed)) { connection.Open(); }
//     var value = (string) cmd.ExecuteScalar();
//     Console.WriteLine(value);
// }
// if (connection.State.Equals(ConnectionState.Open)) { connection.Close(); }
// }