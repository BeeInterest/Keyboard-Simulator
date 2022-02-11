using System;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

namespace Keyboard_Simulator
{
    class RandomCharacter
    {
        private Random random1 = new Random();//переменная для определения номера символа и для определения длины слов
        private const int kolvo = 400; // общее количество символов в тексте(не считая пробелов)
        private int[] numtext = new int[kolvo]; // массив, который заполнится номерами символов (каждый номер соответсвует определенному символу в методах)
        private char[] chars = new char[kolvo]; // массив, который заполнится символами(потом его переделают в текст)5
        private string text = ""; // сам текст, который будет выводится в программе
        public RandomCharacter()
        {
            for (int i = 0; i < kolvo; i++)
            {
                numtext[i] = random1.Next(0, 4); //заполнения массива числами от 0 до 3 (всего 4 символа, они определены в методах)
            }
        }
        private string CreateString(char[] chari)
        {
            for (int i = 0; i < kolvo; i++) // массив символов заполняется символами, взятыми из определенного метода с помощью массива чисел, который был заполнен случайно числами от 0 до 3 (это индексы символов)
            {
                chars[i] = chari[numtext[i]];
            }
            StringBuilder sb = new StringBuilder(); /* создается такая переменная, 
            для того чтобы ее можно было изменять(в нашем случае добавлять новые символы), строка такого не позволяет делать*/
            int k = random1.Next(3, 7);//определяем длину первого слова
            int j = 0;//счетчик букв, чтобы определить, когда пойдет пробел
            foreach (char ch in chars)
            {
                if (j == k)/*если счетчик букв соответствует длине слова,то в конец переменной stringbuilder добавляется пробел,
                потом объявляется новая длина слова и обнуляется счетчик букв*/
                {
                    j = 0;
                    sb.Append(' ');
                    k = random1.Next(3, 7);
                }
                sb.Append(ch);// добавления символа в конец переменной stringbuilder из массива символов
                j++;
            }
            text = sb.ToString();// переменная stringbuilder преобразовывается в строку
            return text;
        }
        public string CombinationOne()
        {
            char[] chars2 = {'в', 'а', 'о', 'л'};
            return CreateString(chars2);
        }
        // следующие методы до FileBook имеют точно такое же описание
        public string CombinationTwo()
        {
            char[] chars2 = {'ф', 'ы', 'д', 'ж'};
            return CreateString(chars2);
        }
        public string CombinationThree()
        {
            char[] chars2 = {'м', 'и', 'т', 'ь'};
            return CreateString(chars2);
        }
        public string CombinationFour()
        {
            char[] chars2 = { 'е', 'п', 'н', 'р' };
            return CreateString(chars2);
        }
        public string CombinationFive()
        {
            char[] chars2 = {'у', 'к', 'г', 'ш'};
            return CreateString(chars2);
        }
        public string CombinationSix()
        {
            char[] chars2 = {'ч', 'с', 'б', 'ю'};
            return CreateString(chars2);
        }
        public string CombinationSeven()
        {
            char[] chars2 = {'й', 'ц', 'щ', 'з'};
            return CreateString(chars2);
        }
        public string CombiantionEight()
        {
            char[] chars2 = {'я', 'э', 'х', 'ъ'};
            return CreateString(chars2);
        }
        public string CombinationOneEn()
        {
            char[] chars2 = {'d', 'f', 'j', 'k'};
            return CreateString(chars2);
        }
        public string CombinationTwoEn()
        {
            char[] chars2 = {'a', 's', 'l', ';'};
            return CreateString(chars2);
        }
        public string CombinationThreeEn()
        {
            char[] chars2 = {'v', 'b', 'n', 'm'};
            return CreateString(chars2);
        }
        public string CombinationFourEn()
        {
            char[] chars2 = {'t', 'g', 'y', 'h'};
            return CreateString(chars2);
        }
        public string CombinationFiveEn()
        {
            char[] chars2 = {'e', 'r', 'u', 'i'};
            return CreateString(chars2);
        }
        public string CombinationSixEn()
        {
            char[] chars2 = {'q', 'w', 'o', 'p'};
            return CreateString(chars2);
        }
        public string CombinationSevenEn()
        {
            char[] chars2 = {'x', 'c', ',', '.'};
            return CreateString(chars2);
        }
        public string CombinationEightEn()
        {
            char[] chars2 = {'z', '!', '?', '/'};
            return CreateString(chars2);
        }
        public string SpecialLesson1()
        {
            char[] chars2 = {'5', '6', '7', '8'};
            return CreateString(chars2);
        }
        public string SpecialLesson2()
        {
            char[] chars2 = {'3', '4', '9', '0'};
            return CreateString(chars2);
        }
        public string SpecialLesson3()
        {
            char[] chars2 = {'1', '2', '-', '='};
            return CreateString(chars2);
        }
        public string SpecialLesson4()
        {
            char[] chars2 = {'%', '^', '&', '*'};
            return CreateString(chars2);
        }
        public string SpecialLesson5()
        {
            char[] chars2 = { '#', '$', '(', ')' };
            return CreateString(chars2);
        }
        public string SpecialLesson6()
        {
            char[] chars2 = {'!', '@', '_', '+'};
            return CreateString(chars2);
        }
        public string SpecialLesson7()
        {
            char[] chars2 = { '`', '~', '\\', '|' };
            return CreateString(chars2);
        }
        public string SpecialLesson8()
        {
            char[] chars2 = {'[', ']', '{', '}'};
            return CreateString(chars2);
        }
        public string SpecialLesson9()
        {
            char[] chars2 = { '<', '>', '<', '>' };
            return CreateString(chars2);
        }

        public string FileBook(string file1, int textrue)
        {
            StreamReader reader = new StreamReader(file1); /* создаем «потоковый читатель» и связываем его с файловым потоком
            (класс StreamReader позволяет считывать весь текст из файла)*/
            string text1 = reader.ReadToEnd(); // в строку записывается весь текст из файла
            reader.Close(); // закрываем считываемый файл и освобождаем все ресурсы
            text1 = Regex.Replace(text1, "  ", " ");//это выражение заменяет в строке два пробела на один (в некоторых файлах есть два пробела)
            char[] text2 = text1.ToCharArray();// преобразовываем строку в массив символов (нужно будет для условий нижу)
            int mini = random1.Next(0, text2.Length - kolvo);// переменная-указатель, которая определяет начало текста 
            int kolvo1;// сколько всего символов будет в будущем тексте
            if (textrue == 1)/*если строка будет именно текстом(а не набором слов), то указатель перейдет на заглавную букву*/
            {
                for (int i = mini; i < text2.Length; i++)
                {
                    if (text2[i] == '.' || text2[i] == '?' || text2[i] == '!')/* если дошли до конца предложения,
                    то указатель переходит на заглавную букву следующего предложения и цикл заканчивается*/
                    {
                        mini = i + 2;
                        break;
                    }
                }
            }
            else if (textrue == 0)/*если строка будет именно набором слов, то указатель перенесётся на начало следующего слова*/
            { 
                for (int i = mini; i < text2.Length; i++)
                {
                    if (text2[i] == ' ')//если символ является пробелом, то указатель переходит на следующий символ(букву) и цикл заканчивается
                    {
                        mini = i+1;
                        break;
                    }
                }
            }
            if (text2.Length - mini < kolvo)/*если указатель оказывается на месте,
            от которого до конца текста остается меньше, чем kolvo (оно равняется 400), то новое количество букв в тексте определяется
            разностью всего количества символов в тексте и количества символов до указателя*/
            {
                kolvo1 = text2.Length - mini;
            }
            else
            {
                kolvo1 = kolvo;
            }
            for (int i = 0; i < kolvo1; i++)/*массив символов заполняется символами из текста*/
            {
                chars[i] = text2[mini];
                mini += 1;
            }
            text = String.Concat<char>(chars);//сцепляет элементы массива символов в одну строку
            return text;
        }
    }
}
