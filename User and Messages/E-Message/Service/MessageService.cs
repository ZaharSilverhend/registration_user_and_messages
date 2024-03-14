using E_Message.Interfece;
using E_Message.objects;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Message.Services
{
    public class MessageService : iMessageService
    {
        public void DisplayAllMessage(string connectionString)
        {

            Console.WriteLine("~Список всех сообщения~\n");
            string sqlExpression = "SELECT * FROM [Messages]";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows) // если есть данные
                    {
                        Console.WriteLine("ID\t\tTEXT\t\tUSER_ID\t\tUSER_EMAIL\n=======================================================================");
                        while (reader.Read())   // построчно считываем данные
                        {
                            var id = reader.GetValue(0);
                            var text = reader.GetValue(1);
                            var userId = reader.GetValue(2);
                            var userName = reader.GetValue(3);

                            Console.WriteLine($"[{id}]\t\t{text}\t\t[{userId}]\t\t_{userName}");
                        }
                    }
                    else { Console.WriteLine("~Список пуст!!!\nСначала внесите данные в БД~"); }
                }
            }
        }
        public void CreateMessage(string connectionString)
        {
            string id, message = "";
            Console.Write("Введите id пользователя под которым вы создаете сообщение: ");
            id = Console.ReadLine();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string checkUserSql = $"SELECT COUNT(*) FROM [Users] WHERE Id = {id}";
                string insertMessageSql = $"INSERT INTO [Messages] (MessageText, UserId) VALUES ('{message}','{id}')";
                string updateUserQuantitySql = $"UPDATE [Users] SET Quantity = Quantity + 1 WHERE Id = {id}";
                string updateMessagesSql = $"UPDATE [Messages] SET UserName = Users.Name FROM Messages INNER JOIN Users ON Messages.UserId = Users.Id WHERE Messages.UserId = {id};";
                connection.Open();
                using (SqlCommand checkUserCommand = new SqlCommand(checkUserSql, connection))
                {
                    int userCount = (int)checkUserCommand.ExecuteScalar();
                    if (userCount == 0)
                    {
                        Console.WriteLine($"\n~Пользователь с ID {id} не существует!!!\nСообщение не добавлено.~");
                        return;
                    }
                }
                Console.Write("\nВведите новое сообщение: ");
                message = Console.ReadLine();
                using (SqlCommand command = new SqlCommand(insertMessageSql, connection))
                using (SqlCommand command2 = new SqlCommand(updateUserQuantitySql, connection))
                using (SqlCommand command3 = new SqlCommand(updateMessagesSql, connection))
                {
                    int number = command.ExecuteNonQuery() + command2.ExecuteNonQuery() + command3.ExecuteNonQuery();
                    if (number > 0) { Console.WriteLine("\nДобавление прошло успешно."); }
                    else { Console.WriteLine("\nНе удалось добавить."); }
                }
            }
        }
        public void DeleteMessage(string connectionString)
        {
            string id;
            Console.Write("Введите id сообщения для удаления: ");
            id = Console.ReadLine();

            string sqlExpression2 = $"UPDATE [Users] SET Quantity = Quantity - 1 WHERE Id IN (SELECT UserId FROM [Messages] WHERE Id = {id})";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command2 = new SqlCommand(sqlExpression2, connection);

                int number = command2.ExecuteNonQuery();
            }
            string sqlExpression = $"DELETE FROM [Messages] WHERE (id) = ({id})";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                int number = command.ExecuteNonQuery() + command.ExecuteNonQuery();
                if (number > 0) { Console.WriteLine("\nУдаление прошло успешно."); }
                else { Console.WriteLine("\nНе удалось удалить."); }
            }
        }
        public void DeleteAllMessages(string connectionString)
        {
            string sqlExpression2 = $"UPDATE [Users] SET Quantity = 0";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command2 = new SqlCommand(sqlExpression2, connection);

                int number = command2.ExecuteNonQuery();
            }
            string sqlExpression = $"DELETE FROM [Messages]";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand command = new SqlCommand(sqlExpression, connection);

                int number = command.ExecuteNonQuery();
                if (number > 0) { Console.WriteLine("\nУдаление прошло успешно."); }
                else { Console.WriteLine("\n~Не удалось удалить.\nСписок пуст!!!\nСначала внесите данные в БД~"); }
            }

        }
    }
}
