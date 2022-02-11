using System;
using System.Drawing;
using System.Windows.Forms;
using WMPLib;
using System.IO;
using System.Text;
// в параметр e передаются свойства, которые описывают произошедшее событие  
// Объект Bitmap используется для работы с изображениями, определяемыми данными пикселей.
//GDI — это интерфейс Windows для представления графических объектов и передачи их на устройства отображения, такие, как мониторы и принтеры.
namespace Keyboard_Simulator
{
    public partial class Simulator : Form
    {
        private char[] mass;// массив символов, который нужен для проверки правильности введённых символов
        private int i = 0;// индекс массива символов
        private string b;// строка, которая получит строку из класса RandomCharacter и станет текстом для пользователя
        public string file1;// строка, которая указывает путь к файлу
        public int texttrue;// определитель, набор слов это или текст
        private int countfalse;// счетчик неправильных символов
        private int countsymb;// счетчик всех символов(правильных и неправильных)
        private int countprav;// cчетчик правильных символов
        private int time=0;// счетчик времени
        private float prav = 0;// процент ошибок
        private int symb = 0;// количество знаков в минуту
        private WindowsMediaPlayer WMP = new WindowsMediaPlayer();// создается объект класса WindowsMediaPlayer для воспроизведения музыки
        private StreamReader reader1; // создаем «потоковый читатель»
        private string text3;// строка, которая получит текст из файла руководства по клавиатурному тренажеру
        private string text2; // строка, которая получит текст из файла руководства по положению пальцев
        private bool musicflag = true; // определяет, включена ли музыка или нет
        private int whatis; // определяет, какой это текст (русский или английский)
        private bool pictureflag = true; //определяет, включена ли клавиатура или не включены
        public Simulator()
        {
            InitializeComponent(); // запускаем программу
            ForUser.BringToFront();
            ForUser.Text = "Для начала выберите режим тренировки!";
            this.KeyPreview = false;
            WMP.settings.volume = 30;// устанавливаем громкость музыки
            закончитьТренировкуToolStripMenuItem.Enabled = false;// режим "Закончить тренировку" не активен
            начатьТренировкуToolStripMenuItem.Enabled = false;// режим "Начать тренировку" не активен
            продолжитьМузыкуToolStripMenuItem.Enabled = false;// режим "Продолжить музыку" не активен
            reader1 = new StreamReader(Path.Combine(Application.StartupPath, "руководство.txt"));/*связываем «потоковый читатель» с файловым потоком
            (класс StreamReader позволяет считывать весь текст из файла). Path.Combine() объединяет две строки в путь,
            Application.StartupPath получает путь для исполняемого файла, запустившего приложение, не включая исполняемое имя*/
            text3 = reader1.ReadToEnd(); // записываем в строку весь текст из файла "руководство.txt"
            reader1.Close();// закрываем считываемый файл и освобождаем все ресурсы
            reader1 = new StreamReader(Path.Combine(Application.StartupPath, "руководство по положению пальцев.txt"));
            text2 = reader1.ReadToEnd();
            reader1.Close();
            включитьToolStripMenuItem.Enabled = false;
            pictureflag = true;
            PictureHelp(pictureflag);
        }
        private void PictureHelp(bool flag)
        {
            if (flag)
            {
                Bitmap img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\зоны.png")); // создается переменная класса Bitmap(хранит растровые изображения), которая хранит в себе изображение из указанного пути.
                pictureBox1.Image = img;
            }
        }
        private void Simulator_Load(object sender, EventArgs e) // срабатывает при загрузке формы
        {
            WMP.URL = Path.Combine(Application.StartupPath, "Music\\3\\378e471317469fa.mp3"); // открывает музыкальный файл, который указан в пути
            WMP.controls.play(); // запускает музыкальный файл
            DialogResult result = MessageBox.Show(text3, "Руководство по клавиатурному тренажеру", MessageBoxButtons.OK, MessageBoxIcon.Information); /*открывает сообщение,
            где указан текст строка с руководством*/
        }
        private void руководствоПоПоложениюПальцевToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(text2, "Руководство по положению пальцев", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void руководствоПоКлавиатурномуТренажеруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(text3, "Руководство по клавиатурному тренажеру", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CloseButton_Click(object sender, EventArgs e) //срабатывает при нажимании на крестик в приложении
        {
            this.Close();
        }

        private void CloseButton_MouseEnter(object sender, EventArgs e)// срабатывает, когда курсор оказывается на крестике
        {
            CloseButton.ForeColor = Color.White;
        }

        private void CloseButton_MouseLeave(object sender, EventArgs e)// срабатывает, когда курсор уходит с крестика
        {
            CloseButton.ForeColor = Color.Black;
        }

        Point lastpoint; // переменная, в которой будут определяться координаты, где была нажата мышка
        private void MainPanel_MouseMove(object sender, MouseEventArgs e) // срабатывает при наведении курсора на панель
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;/* обращаемся к координате left, берем координаты курсора и отнимаем координаты,где была нажата мышка
                (с того места, где была нажата мышка, будет вестись отчет).
                Если значение по иксу изменилось на +10 пикселей, то программа сдвинется на 10 пикселей влево*/
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void MainPanel_MouseDown(object sender, MouseEventArgs e) //срабатывает при отпускании кнопки мыши
        {
            lastpoint = new Point(e.X, e.Y);// устанавлием координаты место, где была нажата мышка
        }

        private void TopPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void TopPanel_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        private void Title_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastpoint.X;
                this.Top += e.Y - lastpoint.Y;
            }
        }

        private void Title_MouseDown(object sender, MouseEventArgs e)
        {
            lastpoint = new Point(e.X, e.Y);
        }

        private void ваОлToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();// создаем объект класса RandomCharacter
            b = a.CombinationOne();// вызываем метод, который вернет строку из случайных символов
            начатьТренировкуToolStripMenuItem.Enabled = true;// меню "Начать тренировку" становится активной
            whatis = 1;
        }
        private void фыДжToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.CombinationTwo();
            начатьТренировкуToolStripMenuItem.Enabled = true;
            whatis = 1;
        }

        private void миТьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.CombinationThree();
            начатьТренировкуToolStripMenuItem.Enabled = true;
            whatis = 1;
        }

        private void епНрToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.CombinationFour();
            начатьТренировкуToolStripMenuItem.Enabled = true;
            whatis = 1;
        }

        private void укГшToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.CombinationFive();
            начатьТренировкуToolStripMenuItem.Enabled = true;
            whatis = 1;
        }

        private void чсБюToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.CombinationSix();
            начатьТренировкуToolStripMenuItem.Enabled = true;
            whatis = 1;
        }

        private void йцЩзToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.CombinationSeven();
            начатьТренировкуToolStripMenuItem.Enabled = true;
            whatis = 1;
        }

        private void яЭхъToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.CombiantionEight();
            начатьТренировкуToolStripMenuItem.Enabled = true;
            whatis = 1;
        }

        private void dfJkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.CombinationOneEn();
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void asLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.CombinationTwoEn();
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void vbNmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.CombinationThreeEn();
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void tgYhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.CombinationFourEn();
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void erUiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.CombinationFiveEn();
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void qwOpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.CombinationSixEn();
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void xcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.CombinationSevenEn();
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void zToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.CombinationEightEn();
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.SpecialLesson1();
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.SpecialLesson2();
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.SpecialLesson3();
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.SpecialLesson4();
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.SpecialLesson5();
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.SpecialLesson6();
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.SpecialLesson7();
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.SpecialLesson8();
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            RandomCharacter a = new RandomCharacter();
            b = a.SpecialLesson9();
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }
        private void короткиеСловаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file1 = Path.Combine(Application.StartupPath, "Book\\Короткие_Слова_Ru.txt");
            whatis = 1;
            texttrue = 0;
            RandomCharacter a = new RandomCharacter();
            b = a.FileBook(file1, texttrue);
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void длинныеСловаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file1 = Path.Combine(Application.StartupPath, "Book\\Длинные_Слова_Ru.txt");// в строку записывается путь до файла
            whatis = 1;
            texttrue = 0; //определитель принимает значение 0, что указывает, что это набор слов
            RandomCharacter a = new RandomCharacter();
            b = a.FileBook(file1, texttrue);
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void текст1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file1 = Path.Combine(Application.StartupPath, "Book\\Текст1_Ru.txt");
            whatis = 1;
            texttrue = 1; //определитель принимает значение 1, что указывает, что это текст
            RandomCharacter a = new RandomCharacter();
            b = a.FileBook(file1, texttrue);
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void текст2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            file1 = Path.Combine(Application.StartupPath, "Book\\Текст2_Ru.txt");
            whatis = 1;
            texttrue = 1;
            RandomCharacter a = new RandomCharacter();
            b = a.FileBook(file1, texttrue);
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void EnкороткиеСловаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            file1 = Path.Combine(Application.StartupPath, "Book\\Короткие_Слова_En.txt");
            texttrue = 0;
            RandomCharacter a = new RandomCharacter();
            b = a.FileBook(file1, texttrue);
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void EnдлинныеСловаToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            file1 = Path.Combine(Application.StartupPath, "Book\\Длинные_Слова_En.txt");
            texttrue = 0;
            RandomCharacter a = new RandomCharacter();
            b = a.FileBook(file1, texttrue);
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void Enтекст1ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            file1 = Path.Combine(Application.StartupPath, "Book\\Текст1_En.txt");
            texttrue = 1;
            RandomCharacter a = new RandomCharacter();
            b = a.FileBook(file1, texttrue);
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }

        private void Enтекст2ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            file1 = Path.Combine(Application.StartupPath, "Book\\Текст2_En.txt");
            texttrue = 1;
            RandomCharacter a = new RandomCharacter();
            b = a.FileBook(file1, texttrue);
            начатьТренировкуToolStripMenuItem.Enabled = true;
        }
        private void включитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureflag = true;
            if (b == "")
            {
                PictureHelp(pictureflag);
            }
            pictureBox1.Visible = true;
            включитьToolStripMenuItem.Enabled = false;
            выключитьToolStripMenuItem.Enabled = true;
        }

        private void выключитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Visible = false;
            pictureflag = false;
            выключитьToolStripMenuItem.Enabled = false;
            включитьToolStripMenuItem.Enabled = true;
        }
        private void выборРежимовToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.KeyPreview = false;
            time = 0;
            countfalse = 0;
            countsymb = 0;
            countprav = 0;
            i = 0;
            закончитьТренировкуToolStripMenuItem.Enabled = false;
            ForUser.TextAlign = ContentAlignment.MiddleCenter;
            ForUser.Text = "Для начала выберите режим тренировки!";
            timer1.Enabled = false;
            b = "";
            PictureHelp(pictureflag);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            time += 1;
        }
        private void TheEnd()
        {
            b = "";
            this.KeyPreview = false;
            WMP.controls.pause();//музыкальный файл ставится на паузу
            WindowsMediaPlayer WMP1 = new WindowsMediaPlayer(); /*создается новый объект класса WindowsMediaPlayer,
            который отвечает за музыку во время выводы сообщения с результатами пользователю*/
            WMP1.URL = Path.Combine(Application.StartupPath, "Music\\для событий\\Андрей Макаревич - Meme Directed by Robert B. Weide.mp3");
            WMP1.settings.volume = 50;
            WMP1.controls.play();
            ForUser.TextAlign = ContentAlignment.MiddleCenter;
            ForUser.Text = "Для начала выберите режим тренировки!";
            int minute = time / 60;// время в минутах, за которое пользователь вводил символы до нажатия кнопки "Закончить" или когда завершил ввод всего текста
            int second = time - 60 * (int)minute;//время в секундах, за которое пользователь вводил символы до нажатия кнопки "Закончить" или когда завершил ввод всего текста
            if (countsymb != 0)
            {
                prav = (float)countfalse / (float)countsymb * 100;// процент ошибок из общего количества символов
                symb = (int)((float)countprav / ((float)minute + (float)second / 60));// скорость знаков в минуту (учитываются количество только правильно введенных символов)
            }
            timer1.Enabled = false;
            PictureHelp(pictureflag);
            DialogResult result = MessageBox.Show($"Ваши результаты:\nКоличество ошибок: {countfalse}" +
                $"\nВсего введенных символов: {countsymb}\nПроцент ошибок: {prav:f2} %\nВаше время: {minute:f0} минут(а) {second} секунд(а)\nВаша скорость набора: {symb:f0} знаков в минуту " +
                $"(скорость знаков высчитывается из количества правильных символов)", "Результат", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // выводится сообщение с результатами
            if (result == DialogResult.OK) //если пользователь ответил на сообщение, то новый объект класса WindowsMediaPlayer останавливает музыку во время сообщения
            {
                WMP1.controls.stop();
            }
            if (musicflag)
            {
                WMP.controls.play();//основной музыкальный файл продолжает играть
            }
            закончитьТренировкуToolStripMenuItem.Enabled = false;
        }
        private void начатьТренировкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            time = 0;
            countfalse = 0;
            countsymb = 0;
            countprav = 0;
            i = 0;
            timer1.Interval = 1000;// задает интервал в милисекундах до вызывания события timer1_Tick()
            this.KeyPreview = true;
            mass = b.ToCharArray(); //массив символов заполняется символами из текста
            ForUser.Text = b; //текст выводится в программе
            начатьТренировкуToolStripMenuItem.Enabled = false;
            закончитьТренировкуToolStripMenuItem.Enabled = true;
            ForUser.TextAlign = ContentAlignment.MiddleLeft;
            SymbolsPicture(mass[i]);
            if (pictureflag)
            {
                pictureBox1.Visible = true;
            }
        }

        private void закончитьТренировкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TheEnd();
            закончитьТренировкуToolStripMenuItem.Enabled = false;
        }

        private void остановитьМузыкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WMP.controls.pause();
            musicflag = false;
            остановитьМузыкуToolStripMenuItem.Enabled = false;
            продолжитьМузыкуToolStripMenuItem.Enabled = true;
        }

        private void продолжитьМузыкуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WMP.controls.play();
            musicflag = true;
            остановитьМузыкуToolStripMenuItem.Enabled = true;
            продолжитьМузыкуToolStripMenuItem.Enabled = false;
        }
        private void Simulator_KeyUp(object sender, KeyEventArgs e)
        {
            if (this.KeyPreview)
            {
                if (i != mass.Length - 1 && pictureflag)
                {
                    SymbolsPicture(mass[i + 1]);
                }
                if (e.KeyCode == Keys.Escape)
                {
                    TheEnd();
                }
            }
        }

        private void Simulator_KeyPress(object sender, KeyPressEventArgs e)
         {
             if (this.KeyPreview)
             {
                 timer1.Enabled = true;
                 if (e.KeyChar == mass[i] && i == mass.Length - 1)
                 {
                     TheEnd();
                 }
                 else if (e.KeyChar != (char)Keys.ShiftKey && e.KeyChar == mass[i])// изменить!
                 {
                     countprav += 1;
                     countsymb += 1;
                     i += 1;
                     StringBuilder texti = new StringBuilder();
                     for (int j = i; j < mass.Length; j++)
                     {
                         texti.Append(mass[j]);
                     }
                     ForUser.Text = texti.ToString();
                 }
                 else if (e.KeyChar != mass[i] && e.KeyChar != (char)Keys.Escape && e.KeyChar != (char)Keys.ShiftKey)
                 {
                     WMP.controls.pause();
                     WindowsMediaPlayer WMP2 = new WindowsMediaPlayer();
                     WMP2.settings.volume = 30;
                     WMP2.URL = Path.Combine(Application.StartupPath, "Music\\для событий\\beep-08b.mp3");
                     WMP2.controls.play();
                     if (musicflag)
                     {
                         WMP.controls.play();//основной музыкальный файл продолжает играть
                     }
                     countsymb += 1;
                     countfalse += 1;
                 }
             }
         }
        private void трек1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WMP.URL = Path.Combine(Application.StartupPath, "Music\\1\\Mondotek_-_Alive_PH_Electro_Remix__65196841.mp3");
            WMP.controls.play();
        }

        private void трек2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WMP.URL = Path.Combine(Application.StartupPath, "Music\\1\\Tony Igy - Astronomia (Radio Edit).mp3");
            WMP.controls.play();
        }

        private void трек3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WMP.URL = Path.Combine(Application.StartupPath, "Music\\1\\Tristam & Braken - Flight.mp3");
            WMP.controls.play();

        }

        private void трек4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WMP.URL = Path.Combine(Application.StartupPath, "Music\\1\\веревочка - Быстрая музыка для весёлых конкурсов_(HugeMusic.ru).mp3");
            WMP.controls.play();

        }

        private void трек5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WMP.URL = Path.Combine(Application.StartupPath, "Music\\1\\Мистер Дудец. Полная версия (Timmy trumpet & SCNDL - Bleed).mp3");
            WMP.controls.play();

        }

        private void музыка1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WMP.URL = Path.Combine(Application.StartupPath, "Music\\3\\6e255b4b6b8375e.mp3");
            WMP.controls.play();

        }

        private void музыка2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WMP.URL = Path.Combine(Application.StartupPath, "Music\\3\\24b0d8ed4ae4cb2.mp3");
            WMP.controls.play();

        }

        private void музыка3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WMP.URL = Path.Combine(Application.StartupPath, "Music\\3\\378e471317469fa.mp3");
            WMP.controls.play();

        }

        private void музыка4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WMP.URL = Path.Combine(Application.StartupPath, "Music\\3\\b5607d8a7d89812.mp3");
            WMP.controls.play();

        }

        private void музыка5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WMP.URL = Path.Combine(Application.StartupPath, "Music\\3\\de144d31b1f3b3f.mp3");
            WMP.controls.play();

        }

        private void сделатьМузыкуПогромчеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WMP.settings.volume = WMP.settings.volume + 5;
        }

        private void сделатьМузыкуПотишеToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WMP.settings.volume = WMP.settings.volume - 5;
        }
        private void SymbolsPicture(char char0)
        {
            Bitmap img = new Bitmap (Path.Combine(Application.StartupPath, $"клавиатура\\зоны.png"));
            if (mass[i] == ' ')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\пробел.png"));
            }
            else if (mass[i] == '-')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\минус.png"));
            }
            else if (mass[i] == '=')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\равно.png"));
            }
            else if (mass[i] == '_')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\нижнее подчеркивание.png"));
            }
            else if (mass[i] == '+')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\плюс.png"));
            }
            else if (mass[i] == '!')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\восклицательный знак.png"));
            }
            else if (mass[i] == '@')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\собака.png"));
            }
            else if (mass[i] == '#')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\решетка.png"));
            }
            else if (mass[i] == '$')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\доллар.png"));
            }
            else if (mass[i] == '%')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\процент.png"));
            }
            else if (mass[i] == '^')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\двоеточие вверху.png"));
            }
            else if (mass[i] == '&')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\вопрос.png"));
            }
            else if (mass[i] == '*')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\звезда.png"));
            }
            else if (mass[i] == '(')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\скобка влево.png"));
            }
            else if (mass[i] == ')')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\скобка вправо.png"));
            }
            else if (mass[i] == '\\')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\наклонная палка влево.png"));
            }
            else if (mass[i] == '`')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\апостроф.png"));
            }
            else if (mass[i] == '~')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\тильда.png"));
            }
            else if (mass[i] == '|')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\прямая палка.png"));
            }
            else if (mass[i] == '>')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\стрелка вправо.png"));
            }
            else if (mass[i] == '<')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\стрелка влево.png"));
            }
            else if (mass[i] == ',')
            {
                if (whatis == 1)
                {
                    img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\запятая ру.png"));
                }
                else
                {
                    img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\запятая(ин).png"));
                }
            }
            else if (mass[i] == ':')
            {
                if (whatis == 1)
                {
                    img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\двоеточие вверху.png"));
                }
                else
                {
                    img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\двоеточие.png"));
                }
            }
            else if (mass[i] == ';')
            {
                if (whatis == 1)
                {
                    img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\доллар.png"));
                }
                else
                {
                    img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\ж.png"));
                }
            }
            else if (mass[i] == '\'')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\э.png"));
            }
            else if (mass[i] == '\"')
            {
                if (whatis == 1)
                {
                    img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\собака.png"));
                }
                else
                {
                    img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\двойные кавычки.png"));
                }
            }
            else if (mass[i] == '?')
            {
                if (whatis == 1)
                {
                    img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\вопрос.png"));
                }
                else
                {
                    img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\символ.(ру)1.png"));
                }
            }
            else if (mass[i] == '.')
            {
                if (whatis == 1)
                {
                    img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\символ.(ру).png"));
                }
                else
                {
                    img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\ю.png"));
                }
            }
            else if (mass[i] == '[')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\квадратная скобка[.png"));
            }
            else if (mass[i] == ']')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\квадратная скобка].png"));
            }
            else if (mass[i] == '{')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\квдрат.png"));
            }
            else if (mass[i] == '}')
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\квдрат2.png"));
            }
            else if (char.IsLower(mass[i]))
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\{mass[i]}.png"));
            }
            else if (!char.IsDigit(mass[i]))
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\{char.ToLower(mass[i])}вв.png"));
            }
            else
            {
                img = new Bitmap(Path.Combine(Application.StartupPath, $"клавиатура\\{mass[i]}.png"));
            }
            pictureBox1.Image = img;
        }
    }
}
 