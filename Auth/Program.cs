using System;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth
{
    class Program
    {
        static string connectionString = @"Data Source=10.2.2.212;Initial Catalog=NewBD_1;User ID=Student;Pooling=False";

        static void Main(string[] args)
        {
                        
            Console.Write("Enter login:");
            string name = Console.ReadLine();

            Console.Write("Enter password:");
            string password = Console.ReadLine();

            AddUser(name, password);
            Console.WriteLine();           

            Console.Read();
        }

        private static void AddUser(string name, string password)
        {
            // название процедуры
            string sqlExpression = "isLogin";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                // параметр для ввода имени
                SqlParameter nameParam = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = name
                };
                // добавляем параметр
                command.Parameters.Add(nameParam);
                // параметр для ввода пароля
                SqlParameter passParam = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = password
                };
                command.Parameters.Add(passParam);

                var result = command.ExecuteScalar();
                // если нам не надо возвращать id
                //var result = command.ExecuteNonQuery();

                Console.WriteLine("Id добавленного объекта: {0}", result);
            }
        }
        //private static void AddUser(string name, int age)
        //{
        //    // название процедуры
        //    string sqlExpression = "sp_InsertUser";

        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        SqlCommand command = new SqlCommand(sqlExpression, connection);
        //        // указываем, что команда представляет хранимую процедуру
        //        command.CommandType = System.Data.CommandType.StoredProcedure;
        //        // параметр для ввода имени
        //        SqlParameter nameParam = new SqlParameter
        //        {
        //            ParameterName = "@name",
        //            Value = name
        //        };
        //        // добавляем параметр
        //        command.Parameters.Add(nameParam);
        //        // параметр для ввода возраста
        //        SqlParameter ageParam = new SqlParameter
        //        {
        //            ParameterName = "@age",
        //            Value = age
        //        };
        //        command.Parameters.Add(ageParam);

        //        var result = command.ExecuteScalar();
        //        // если нам не надо возвращать id
        //        //var result = command.ExecuteNonQuery();

        //        Console.WriteLine("Id добавленного объекта: {0}", result);
        //    }
        //}


        // вывод всех пользователей
        
        }
    }


