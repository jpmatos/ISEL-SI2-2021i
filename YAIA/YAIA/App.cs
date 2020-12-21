using System;
using System.Collections.Generic;

namespace YAIA
{ 
    public class App
    {
        
        private delegate void DBMethod();
        private Dictionary<Option, DBMethod> __dbMethodsADO; 
        private Dictionary<Option, DBMethod> __dbMethodsEFCore;    
             
        private static App __instance;
        public static App Instance
        {
            get
            {
                if (__instance == null)
                    __instance = new App();
                return __instance;
            }
            private set { }
        }

        private const Framework DefaultData = Framework.ADO;

        private Framework Data
        {
            get;
            set;
        }

        private enum Framework
        {
            ADO,
            EFCore
        }

        private enum Option
        {
            Unknown = -1,
            Exit,
            InsertInvoice,
            SwitchData
        }
        
        private App()
        {
            __dbMethodsADO = new Dictionary<Option, DBMethod>();
            __dbMethodsADO.Add(Option.InsertInvoice, new View.ADO.InsertInvoice().Query);
            __dbMethodsADO.Add(Option.SwitchData, SwitchData);
            
            __dbMethodsEFCore = new Dictionary<Option, DBMethod>();
            __dbMethodsEFCore.Add(Option.InsertInvoice, new View.EFCore.InsertInvoice().Query);
            __dbMethodsEFCore.Add(Option.SwitchData, SwitchData);
        }
        
        private Option DisplayMenu()
        {
            Option option = Option.Unknown;
            try
            {
                Console.WriteLine("Yet Another Invoice Application");
                Console.WriteLine("1. Insert Invoice");
                Console.WriteLine($"2. ---Switch Data--- (Current: {Data})");
                Console.WriteLine("0. Exit");
                var result = Console.ReadLine();
                option = (Option)Enum.Parse(typeof(Option), result);
            }
            catch (ArgumentException)
            {
                // Nothing to do. User press select no option and press enter.
            }

            return option;
        }
        
        private void SwitchData()
        {
            if (Data == Framework.ADO)
            {
                Data = Framework.EFCore;
            }
            else
            {
                Data = Framework.ADO;
            }
            Console.WriteLine($"Switched to {Data}");
        }
        
        public void Run()
        {
            Data = DefaultData;
            Option userInput = Option.Unknown;
            do
            {
                Console.Clear();
                userInput = DisplayMenu();
                Console.Clear();
                try
                {
                    switch (Data)
                    {
                        case Framework.ADO:
                            __dbMethodsADO[userInput]();
                            break;
                        case Framework.EFCore:
                            __dbMethodsEFCore[userInput]();
                            break;
                    }
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