using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            string ConnectionString = @"server=.\mssql2012;database=GeneralBusinessDB;uid=sa;pwd=sa2012LJ;";
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand();
                command.Connection = connection;
                command.CommandText = "select * from Menus";
                //command.Parameters.AddRange(null);
                connection.Open();
                var reader = command.ExecuteReader();
                var list = new List<Dictionary<string, dynamic>>();
                //读取list数据
                while (reader.Read())
                {
                    list.Add(Enumerable.Range(0, reader.FieldCount).ToDictionary(reader.GetName, reader.GetValue));
                }
                Console.WriteLine("ok");
                Console.ReadKey();
            }
        }
    }
}
