using System.Data;
using System.Data.SqlClient;
using E_Message.Controllers.Helpers;
using E_Message.Interfece;
using E_Message.Services;
using Microsoft.Identity.Client;

namespace E_Message.Controllers
{
    public class Controller : СhooseCommand
    {
        private readonly iMessageService _messageService;
        private readonly iUserService _userService;
        public Controller(iMessageService messageService, iUserService userService)
        {
            _messageService = messageService;
            _userService = userService;
        }

        public int Menu()
        {

            string connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\не-кто\Desktop\progectDB\E-Message\DataBase\Database.mdf;Integrated Security=True";
            UserService commandsUser = new UserService();
            MessageService commandsMessage = new MessageService();
            ConsoleKeyInfo arrow;
            List<string> items = new List<string>()
            {
                " {<} Вывести всех пользователей         ",//0
                " {<} Вывести все сообщения              ",//1
                " {<} Вывести все сообщения пользователя ",//2
                " {+} Добавить пользователя              ",//3
                " {+} Добавить сообщение                 ",//4
                " {-} Удалить пользователя               ",//5
                " {-} Удалить сообщение                  ",//6
                " {-} Удалить все сообщения              ",//7
                " {-} Удалить всех пользователей         ",//8
                " <== ~Выход~                            "//9
            };
            while (true)
            {
                switch (GetUserCommand(ref items))
                {
                    case 0:
                        commandsUser.DisplayAllUser(connectionString);
                        break;
                    case 1:
                        commandsMessage.DisplayAllMessage(connectionString);
                        break;
                    case 2:
                        commandsUser.DisplayMessageInUser(connectionString);
                        break;
                    case 3:
                        commandsUser.CreateUser(connectionString);
                        break;
                    case 4:
                        commandsMessage.CreateMessage(connectionString);
                        break;
                    case 5:
                        commandsUser.DeleteUser(connectionString);
                        break;
                    case 6:
                        commandsMessage.DeleteMessage(connectionString);
                        break;
                    case 7:
                        commandsMessage.DeleteAllMessages(connectionString);
                        break;
                    case 8:
                        commandsUser.DeleteAllUsers(connectionString);
                        break;
                    case 9:
                        return 0;
                }
                Console.BackgroundColor = ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.WriteLine("\n <- {esc} ");
                Console.ResetColor();
                arrow = Console.ReadKey(true);
                while (arrow.Key != ConsoleKey.Escape) { arrow = Console.ReadKey(true); }
            }
        }
    }
}
