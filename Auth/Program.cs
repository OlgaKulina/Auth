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
            string passRep = String.Empty;

            do
            {
                Console.Clear();
                Console.WriteLine("Повторите пароль");
                passRep = Console.ReadLine();

                Console.WriteLine($"Pass\t\t{MD5Hash(ComputeSha256Hash(password + "vasya"))}\nPass rep\t{passRep}");
                Console.Read();
            } while (!password.Equals(passRep));                       

            AddLogin(login);                  

            Console.Read();
        }

        private static void AddLogin(string login)
        {
            // название процедуры
            string sqlExpression = "isLogin";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;
                // параметр для ввода логина
                SqlParameter logParam = new SqlParameter
                {
                    ParameterName = "@name",
                    Value = login
                };
                // добавляем параметр
                command.Parameters.Add(logParam);

                var result = command.ExecuteScalar();
                // если нам не надо возвращать id
                //var result = command.ExecuteNonQuery();

                Console.WriteLine("Id добавленного объекта: {0}", result);
            }
        }


        private static void AddPassword(string password)
        {
            // название процедуры
            string sqlExpression = "isPass";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                // указываем, что команда представляет хранимую процедуру
                command.CommandType = System.Data.CommandType.StoredProcedure;                
                
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


        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }
        static string ComputeSha256Hash(string rawData)
        {
            // Create a SHA256   
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
       

    }
    }


