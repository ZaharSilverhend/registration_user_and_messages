using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace E_Message.Controllers.Helpers
{
    public class СhooseCommand : OutputCommand
    {
        public static int GetUserCommand(ref List<string> items)
        {
            int command = 0; 
            ConsoleKeyInfo tapKey;
            while (true)
            {
                DisplayItems(ref command, ref items);
                tapKey = Console.ReadKey(true);
                switch (tapKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        command = command - 1 < 0 ? command = items.Count - 1 : command - 1;
                        break;
                    case ConsoleKey.DownArrow:
                        command = command + 1 > items.Count - 1 ? command = 0 : command + 1;
                        break;
                    case ConsoleKey.Enter:
                        Console.Clear();
                        return command;
                }
            }
        }

    }
}
