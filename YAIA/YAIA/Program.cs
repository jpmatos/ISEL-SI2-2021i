using System;
using Connection;
using Mapper;
using Microsoft.Data.SqlClient;

namespace YAIA
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine($"(DataSource = '{AbstractSession.DataSource}'; InitialCatalog = '{AbstractSession.InitialCatalog}')");
            Console.WriteLine("DB User:");
            AbstractSession.UserId = Console.ReadLine();
            Console.WriteLine("Password:");
            AbstractSession.Password = Console.ReadLine();
            try
            {
                new Session().Login();
                App.Instance.Run();
            }
            catch (SqlException)
            {
                Console.WriteLine("Login failed!");
            }
        }
    }
}