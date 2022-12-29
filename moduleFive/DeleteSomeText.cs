using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace moduleFive
{
    class DeleteSomeText
    {
        public static void SaveCursorPosition(out int Pos_x, out int Pos_y)
        {
            Pos_x = Console.CursorLeft;
            Pos_y = Console.CursorTop;
        }
        public static void DeleteWrongEnter(string enter, int x, int y)
        {
            string whitespace = "";
            for (int i = 0; i < enter.Length; i++)
            {
                whitespace += " ";
            }
            Console.SetCursorPosition(x, y);
            Console.WriteLine(whitespace);
            Console.SetCursorPosition(x, y);
        }
    }
}
