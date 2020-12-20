using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using EFCore;
using IMappers;
using Mappers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace YAIA
{
    static class Program
    {
        public static void Main(string[] args)
        {
            //ADO Test
            // using (var s = new Session())
            // {
            //     IMapperContributor map = s.CreateMapperContributor();
            //     Entities.Contributor contributor = map.Read(123);
            //     Console.WriteLine(contributor);
            // }

            //EFCore Test
            using (var ctx = new YAIA_Context())
            {
                var contributor =
                    (from c in ctx.Contributors.AsNoTracking() where c.Nif == 123 select c).SingleOrDefault();
                Console.WriteLine(contributor.Name);

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
            }
        }
    }
}