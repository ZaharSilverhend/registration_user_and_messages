using E_Message.Controllers;
using E_Message.Interfece;
using E_Message.Services;

namespace E_Message
{ 
    public class Program
    {
        static private iMessageService messageService;
        static private iUserService userService;
        public static void Main()
        {
            Controller run = new Controller(messageService, userService);
            run.Menu();
        }
    }
}
