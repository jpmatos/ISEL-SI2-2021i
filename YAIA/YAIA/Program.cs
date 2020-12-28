using System;
using Connection;
using EFCore;
using Mapper;
using Microsoft.Data.SqlClient;

namespace YAIA
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            do
            {
                Console.WriteLine(
                    $"(DataSource = '{AbstractSession.DataSource}'; InitialCatalog = '{AbstractSession.InitialCatalog}')");
                Console.WriteLine("DB User:");
                string user = Console.ReadLine();
                Console.WriteLine("Password:");
                string password = Console.ReadLine();
                AbstractSession.UserId = user;
                AbstractSession.Password = password;
                YAIA_Context.UserId = user;
                YAIA_Context.Password = password;
                try
                {
                    new Session().Login();
                    App.Instance.Run();
                    break;
                }
                catch (SqlException)
                {
                    Console.Clear();
                    Console.WriteLine("Login failed!");
                }
            } while (true);
        }
    }
}