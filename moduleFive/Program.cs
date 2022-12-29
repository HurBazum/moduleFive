using System.Drawing;

namespace moduleFive
{
    class Program
    {
        static void Main(string[] args)
        {
            FillTuple(out (string name, string surname, int age, bool hasPet, int amountPets, string[] petsName, int amountFavColors, string[] favColors) user);
            PaintUserCard(user);
        }
        static void FillTuple(out (string name, string surname, int age, bool hasPet, int amountPets, string[] petsName, int amountFavColors, string[] favColors) tuple)
        {
            tuple = (null, null, 0, default, 0, null, 0, null);
            Console.Write("Введите имя: ");
            EnterString(ref tuple.name);
            Console.Write("Введите фамилию: ");
            EnterString(ref tuple.surname);
            Console.Write("Введите возраст: ");
            EnterNumber(ref tuple.age);
            Console.Write("Наличие питомцев(да/нет): ");
            EnterHasPetOrNo(ref tuple.hasPet);
            CollectionPetsName(tuple.hasPet, ref tuple.petsName, ref tuple.amountPets);
            CollectionFavouriteColors(ref tuple.favColors, ref tuple.amountFavColors);
        }
        static void EnterString(ref string s)
        {
            do
            {
                DeleteSomeText.SaveCursorPosition(out int x, out int y);
                s = Console.ReadLine();
                for (int i = 0; i < s.Length; i++)
                {
                    if (!char.IsLetter(s[i]))
                    {
                        DeleteSomeText.DeleteWrongEnter(s, x, y);
                        s = null;
                        break;
                    }
                }
                if (s != null)
                {
                    s = s.ToLower();
                    s = String.Concat(Char.ToUpper(s.ToCharArray()[0]), s.Substring(1, s.Length - 1));
                }
            } while (s == null);
        }
        static void EnterHasPetOrNo(ref bool result)
        {
            string s;
            do
            {
                DeleteSomeText.SaveCursorPosition(out int x, out int y);
                s = Console.ReadLine();
                if (!s.Equals("да", StringComparison.CurrentCultureIgnoreCase) && !s.Equals("нет", StringComparison.CurrentCultureIgnoreCase))
                {
                    DeleteSomeText.DeleteWrongEnter(s, x, y);
                    s = null;
                }
            } while (s == null);
            if (s.Equals("да", StringComparison.CurrentCultureIgnoreCase))
            {
                result = true;
            }
            else
            {
                result = false; 
            }
        }
        static void EnterNumber(ref int number, int baseValue = 0) 
        {
            do
            {
                DeleteSomeText.SaveCursorPosition(out int x, out int y);
                string? enter = Console.ReadLine();
                if (!int.TryParse(enter, out number) || number <= baseValue || number > 100)
                {
                    DeleteSomeText.DeleteWrongEnter(enter, x, y);
                }
            } while (number <= baseValue);
        }
        static void CollectionPetsName(bool b, ref string[] pet_names, ref int amount)
        {
            if (b == true)
            {
                Console.Write("Введите количество питомцев: ");
                EnterNumber(ref amount);
                pet_names = new string[amount];
                for (int i = 0; i < pet_names.Length; i++)
                {
                    Console.Write($"Имя питомца #{i + 1}: ");
                    EnterString(ref pet_names[i]);
                }
            }
            for(int i = 0; i < amount; i++)
            { 
                if (i != pet_names.Length - 1)
                {
                    Console.Write(pet_names[i] + ", ");
                }
                else
                {
                    Console.WriteLine(pet_names[i] + " - Ваши питомцы.");
                }
            }
        }
        static void EnterColor(ref string s)
        {
            do
            {
                DeleteSomeText.SaveCursorPosition(out int x, out int y);
                s = Console.ReadLine();
                s = string.Concat(Char.ToUpper(s.ToCharArray()[0]), s.ToLower().Substring(1, s.Length - 1));
                
                if (!Color.FromName(s).IsKnownColor)
                {
                    DeleteSomeText.DeleteWrongEnter(s, x, y);
                    s = null;
                }
            } while (s == null);
        }
        static void CollectionFavouriteColors(ref string[] colors, ref int amount)
        {
            Console.Write("Введите количество любимых цветов: ");
            EnterNumber(ref amount);
            colors = new string[amount];
            Console.WriteLine("Теперь вводите названия цветов по-английски!");
            for (int i = 0; i < colors.Length; i++)
            {
                Console.Write($"Название цвета #{i + 1}: ");
                EnterColor(ref colors[i]);
            }
        }

        static void PaintUserCard((string name, string surname, int age, bool hasPet, int amountPets, string[] petsName, int amountFavColors, string[] favColors) tuple)
        {
            Console.WriteLine();
            Console.WriteLine();
            string name = $"Имя: {tuple.name}";
            string surname = $"Фамилия: {tuple.surname}";
            string age = $"Возраст: {tuple.age}";
            string pet = "Питомцы:";
            string colors = "Любимые цвета:";
            int bottomBoardLevel = (tuple.amountPets > tuple.amountFavColors) ? tuple.amountPets + 1: tuple.amountFavColors + 1;
            int spaceLeft = (name.Length > surname.Length) ? (name.Length > colors.Length) ? name.Length + 1 : colors.Length + 2 : (surname.Length < colors.Length) ? colors.Length + 2 : surname.Length + 1;
            string board = "";
            for(int i = 0; i < 2 * spaceLeft; i++)
            {
                board += "_";
            }

            Console.WriteLine(board);
            Console.CursorLeft += ( spaceLeft - name.Length ) / 2;
            Console.Write(name);
            Console.SetCursorPosition(spaceLeft + (spaceLeft - surname.Length) / 2, Console.CursorTop);
            Console.Write(surname);
            Console.CursorTop++;
            Console.CursorLeft = 0;
            Console.WriteLine(board);
            Console.CursorTop++;
            Console.CursorLeft = spaceLeft - age.Length / 2;
            Console.WriteLine(age);
            Console.WriteLine(board);

            Console.CursorLeft = spaceLeft / 2 - pet.Length / 2;
            int petsAndColorLevel = Console.CursorTop;
            Console.Write(pet);
            if(tuple.amountPets > 0)
            {
                for (int i = 0; i < tuple.petsName.Length; i++)
                {
                    Console.CursorTop++;
                    Console.CursorLeft = spaceLeft / 2 - tuple.petsName[i].Length / 2;
                    Console.Write(tuple.petsName[i]);
                    if (tuple.amountPets >= tuple.amountFavColors)
                    {
                        Console.CursorLeft = spaceLeft;
                        Console.Write("|");
                    }
                }

            }

            Console.CursorTop = petsAndColorLevel;
            Console.CursorLeft = spaceLeft + spaceLeft / 2 - colors.Length / 2;
            Console.Write(" " + colors);
            for (int i = 0; i < tuple.favColors.Length; i++)
            {
                Console.CursorTop++;
                Console.CursorLeft = spaceLeft + spaceLeft / 2 - tuple.favColors[i].Length / 2;
                Console.Write(tuple.favColors[i]);
                if (tuple.amountPets < tuple.amountFavColors)
                {
                    Console.CursorLeft = spaceLeft;
                    Console.Write("|");
                }
            }
            Console.SetCursorPosition(0, petsAndColorLevel + bottomBoardLevel);
            Console.WriteLine(board);
        }

    }
}