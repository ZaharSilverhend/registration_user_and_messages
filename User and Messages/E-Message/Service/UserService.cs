using E_Message.Interfece;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using E_Message.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Message.Services
{
    public class UserService : iUserService
    {
        public void DisplayAllUser(string connectionString)
        {
            Console.WriteLine("~Список Users~\n");
            string sqlExpression = "SELECT * FROM [Users]";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Console.WriteLine("ID\t\tEMAIL\t\t\tNUMBER_M\n================================================");
                        while (reader.Read())
                        {
                            var id = reader.GetValue(0);
                            var name = reader.GetValue(1);
                            var messageCount = reader.GetValue(2);
                            Console.WriteLine($"[{id}]\t\t_{name}\t[{messageCount}]");
                        }
                    }
                    else { Console.WriteLine("~Список пуст!!!\nСначала внесите данные в БД~"); }
                }
            }
        }
        public void DisplayMessageInUser(string connectionString)
        {
            string id;
            Console.Write("Введите id пользователя: ");
            id = Console.ReadLine();
            string sqlExpression = $"SELECT * FROM [Messages] WHERE UserId = {id}";
            string checkUserSql = $"SELECT COUNT(*) FROM [Users] WHERE Id = {id}";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                using (SqlCommand checkUserCommand = new SqlCommand(checkUserSql, connection))
                {
                    int userCount = (int)checkUserCommand.ExecuteScalar();
                    if (userCount == 0)
                    {
                        Console.WriteLine($"\n~Пользователь с ID {id} не существует!!!\nСообщение не добавлено.~");
                        return;
                    }
                }
                Console.WriteLine($"\n~Cообщения пользователя~");
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        Console.WriteLine("\nID\t\tTEXT\t\tUSER_ID\t\tUSER_EMAIL\n=======================================================================");

                        while (reader.Read())
                        {
                            var Id = reader.GetValue(0);
                            var text = reader.GetValue(1);
                            var userId = reader.GetValue(2);
                            var userName = reader.GetValue(3);

                            Console.WriteLine($"[{Id}]\t\t{text}\t\t[{userId}]\t\t_{userName}");
                        }
                    }
                    else { Console.WriteLine("\n~Список пуст!!!\nСначала внесите данные в БД~"); }
                }
            }
        }
        public void CreateUser(string connectionString)
        {
            string name;
            Console.Write("Введите email нового пользователя: ");
            name = Console.ReadLine();

            string sqlExpression = $"INSERT INTO [Users] (Name, Quantity) VALUES ('{name}', '{0}')";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                if (number > 0) { Console.WriteLine("\nДобавление прошло успешно."); }
                else { Console.WriteLine("\nНе удалось добавить."); }
            }
        }
        public void DeleteUser(string connectionString)
        {
            string id;
            Console.Write("Введите id пользователя для удаления: ");
            id = Console.ReadLine();

            string sqlExpression = $"DELETE FROM [Users] WHERE (id) = ({id})";
            string sqlExpression2 = $"DELETE FROM [Messages] WHERE (UserId) = ({id})";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlCommand command2 = new SqlCommand(sqlExpression2, connection);
                int number = command.ExecuteNonQuery() + command2.ExecuteNonQuery();
                if (number > 0) { Console.WriteLine("\nУдаление прошло успешно."); }
                else { Console.WriteLine("\nНе удалось удалить."); }
            }
        }
        public void DeleteAllUsers(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlExpression = $"DELETE FROM [Users]";
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                int number = command.ExecuteNonQuery();
                if (number > 0) { Console.WriteLine("\nУдаление прошло успешно."); }
                else { Console.WriteLine("\n~Не удалось удалить.\nСписок пуст!!!\nСначала внесите данные в БД~"); }
            }
        }
    }
}
