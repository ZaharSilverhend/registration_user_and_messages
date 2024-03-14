using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Message.Controllers.Helpers
{
    public class OutputCommand
    {
        public static void DisplayItems(ref int command, ref List<string> items)
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine("\t\t ~Меню~ \n----------------------------------------");
            for (int i = 0; i < items.Count(); i++)
            {
                if (i == command)
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    if (command == 9) { Console.BackgroundColor = ConsoleColor.DarkRed; }
                    else if (command == 8 || command == 7 ||command == 6 || command == 5 ) { Console.BackgroundColor = ConsoleColor.Yellow; }
                    else if (command == 4 || command == 3) { Console.BackgroundColor = ConsoleColor.Green; }
                    else { Console.BackgroundColor = ConsoleColor.White; }
                    Console.WriteLine(items[i]);
                    Console.ResetColor();
                }
                else { Console.WriteLine(items[i]); }
            }
        }
    }
}
