using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;

namespace BreakBrainGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IGame
    {
        // Необходимые поля для 1 уровня
        const int _time = 15;
        int time = _time;
        int clickCount = 0;
        int numberLVL = 1;
        Button but;
        Label lbl;
        Label numberLVLlabel;
        DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();
            LVL1_Load();
        }

        /// <summary>
        /// Создаёт кнопку для 1 уровня.
        /// </summary>
        /// <returns></returns>
        Button LVL1_CreateButton() {
            Button but = new Button { Width = 100, Height = 100, FontSize = 25, Content = "ЖМяк", };
            but.Click += LVL1_But_Click;
            return but;
        }

        /// <summary>
        /// Создаёт Лейбл для 1 уровня.
        /// </summary>
        /// <returns></returns>
        Label LVL1_CreateLabel() {
            Label lbl = new Label { VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, Width = 300, Height = 60, FontSize = 15, };
            return lbl;
        }


        /// <summary>
        /// Клики на кнопку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LVL1_But_Click(object sender, RoutedEventArgs e)
        {
            clickCount++;           // Прибавлять количество кликов.
            if (clickCount == 1)    // Запуск таймера.
                LVL1_Timer();
            LVL1_CheckCountClick();      // Проверка на количество кликов.

        }

        /// <summary>
        /// Запуск таймера.
        /// </summary>
        private void LVL1_Timer() {
            lbl.Content = $"Время до переустановки винды: {time}";
            timer.Interval = new TimeSpan(10000000);
            timer.Start();
        }

        /// <summary>
        /// Проверка количества кликов и таймера
        /// </summary>
        private void LVL1_CheckCountClick()
        {
            switch (clickCount)
            {
                case 1: but.Content = "Тык"; break;
                case 2: but.Height = 40; break;
                case 3: but.Height = 10; but.Width = 10; but.HorizontalAlignment = HorizontalAlignment.Right; break;
                case 4: but.Content = "Не ЖмЯкАй((("; but.HorizontalAlignment = HorizontalAlignment.Left; but.VerticalAlignment = VerticalAlignment.Bottom; but.Width = 120; but.Height = 55; but.FontSize = 20; Margin = new Thickness(10); break;
                case 5: MessageBox.Show("Не жмякай!!!"); break;
                case 6: but.HorizontalAlignment = HorizontalAlignment.Center; but.VerticalAlignment = VerticalAlignment.Center; but.Width = 180; but.FontSize = 25; but.Content = "Ahhh, sempai..."; break;
                case 7: MessageBox.Show("Бака!!"); MessageBox.Show("Бака!!"); MessageBox.Show("Бака!!"); MessageBox.Show("Бака!!"); MessageBox.Show("Бака!!"); break;
                case 8:
                    but.HorizontalAlignment = HorizontalAlignment.Center;
                    but.Height = 200;
                    but.Width = 200;
                    but.Content = "Подумой..";
                    Label label = new Label
                    {
                        VerticalAlignment = VerticalAlignment.Bottom,
                        FontSize = 20,
                        Content = "Ты же понимаешь что тебе будет плохо, если жмякнешь ещё раз",
                    };
                    gameGrid.Children.Add(label);
                    label.MouseLeftButtonUp += Label_MouseLeftButtonUp;
                    break;
                case 9: 
                    MessageBox.Show("Я тебя предупреждал!");
                    if (time != _time)
                        for (int i = 0; i < 10; Process.Start("notepad"), i++) ;
                    break;
            }
        }

        private void Label_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            timer.Stop(); ClearLvL(); numberLVL++; LVL2_Load();
        }

        /// <summary>
        /// Действия на каждый тик таймера.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LVL1_Timer_Tick(object sender, EventArgs e)
        {
            time--;
            lbl.Content = $"Время до переустановки винды: {time}";
            if(time > 0) {
                if (clickCount >= 10) { timer.Stop(); }
            }
            else { timer.Stop(); clickCount = 0; time = _time; ClearLvL(); LVL1_Load(); }
        }

        /// <summary>
        /// Загружает первый уровень на форму.
        /// </summary>
        public void LVL1_Load() {
            but = LVL1_CreateButton();          // Кнопка для жмяков
            gameGrid.Children.Add(but);

            lbl = LVL1_CreateLabel();           // Лейбл для вывода таймера
            gameGrid.Children.Add(lbl);

            timer = new DispatcherTimer();      // Таймер
            timer.Tick += LVL1_Timer_Tick;
            CreateLVLlabel();                   // Отображение номера уровня
        }



        // Необходимые поля для 2 уровня
        Button but1;
        Button but2;
        Button but3;
        Button but4;
        Button butExit;

        Label lbl1;
        Label lbl2;
        Label lbl3;
        Label lbl4;
        Label lblEnter;

        int but1Click = 0;
        int but2Click = 0;
        int but3Click = 0;
        int but4Click = 0;

        TextBox txtbox;

        /// <summary>
        /// Создаёт лейбл для 2 уровня
        /// </summary>
        /// <returns></returns>
        Label LVL2_CreateLabel() {
            Label label = new Label {
                FontSize = 25,
            };
            return label;
        }
         
        /// <summary>
        /// Создаёт кнопку для 2 уровня
        /// </summary>
        /// <returns></returns>
        Button LVL2_CreateButton() {
            Button but = new Button {
                FontSize = 20,
                Content = "Тык",
            };
            return but;
        }

        /// <summary>
        /// Создаёт текстбокс для 2 уровня
        /// </summary>
        /// <returns></returns>
        TextBox LVL2_CreateTextBox() {
            TextBox txt = new TextBox {
                Width = 200,
                Height = 30,
                FontSize = 15,
                Visibility = Visibility.Hidden,
                IsEnabled = false,
            };
            return txt;
        }
               
        
        private void But1_Click(object sender, RoutedEventArgs e) {
            but1Click++;
            lbl1.Content = but1Click;
            if (but1Click == 10) txtbox.Visibility = Visibility.Visible;    // Включить видимость Текстокса
            else txtbox.Visibility = Visibility.Hidden;                     // Отключить видимость Текстбокса
            if (but1Click >= 15) but1Click = 0;     // Обнулять счётчик
        }

        private void But2_Click(object sender, RoutedEventArgs e) {
            but2Click++;
            lbl2.Content = but2Click;
            if (but2Click == 3) txtbox.IsEnabled = true;                    // Включить ввод в Текстбокс
            else txtbox.IsEnabled = false;                                  // Отключить ввод в Текстбокс
            if (but2Click >= 15) but2Click = 0;     // Обнулять счётчик
        }

        private void But3_Click(object sender, RoutedEventArgs e) {
            but3Click++;
            lbl3.Content = but3Click;
            if (but3Click == 14) lblEnter.Visibility = Visibility.Visible;  // Включить видимость Лейбла
            else lblEnter.Visibility = Visibility.Hidden;                   // Отключить видимость Лейбла
            if (but3Click >= 15) but3Click = 0;     // Обнулять счётчик
        }

        private void But4_Click(object sender, RoutedEventArgs e) {
            but4Click++;
            lbl4.Content = but4Click;
            if (but4Click == 8) MessageBox.Show("Собери пазл))");   // Подсказка
            if (but4Click >= 15) but4Click = 0;     // Обнулять счётчик
        }

        private void Txtbox_KeyDown(object sender, KeyEventArgs e) {
            if(e.Key == Key.Enter)
                if (txtbox.Text == "103148") { butExit.IsEnabled = true; }
        }

        private void ButExit_Click(object sender, RoutedEventArgs e) {
            ClearLvL();
            numberLVL++;
            LVL3_Load();
        }

        /// <summary>
        /// Загружает второй уровень на форму.
        /// </summary>
        public void LVL2_Load()
        {
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition());

            gameGrid.RowDefinitions.Add(new RowDefinition());
            gameGrid.RowDefinitions.Add(new RowDefinition());
            gameGrid.RowDefinitions.Add(new RowDefinition());


            /// Лейбл 1
            lbl1 = LVL2_CreateLabel();
            lbl1.Margin = new Thickness(10);
            lbl1.Content = but1Click;
            Grid.SetRow(lbl1, 2);
            Grid.SetColumn(lbl1, 3);
            gameGrid.Children.Add(lbl1);
            /// Кнопка 1
            but1 = LVL2_CreateButton();
            but1.Click += But1_Click;
            but1.Margin = new Thickness(10);
            Grid.SetRow(but1, 0);
            Grid.SetColumn(but1, 0);
            gameGrid.Children.Add(but1);


            /// Лейбл 2
            lbl2 = LVL2_CreateLabel();
            lbl2.Margin = new Thickness(10);
            lbl2.Content = but2Click;
            Grid.SetRow(lbl2, 2);
            Grid.SetColumn(lbl2, 0);
            gameGrid.Children.Add(lbl2);
            /// Кнопка 2
            but2 = LVL2_CreateButton();
            but2.Click += But2_Click;
            but2.Margin = new Thickness(10);
            Grid.SetRow(but2, 1);
            Grid.SetColumn(but2, 0);
            gameGrid.Children.Add(but2);


            /// Лейбл 3
            lbl3 = LVL2_CreateLabel();
            lbl3.Margin = new Thickness(10);
            lbl3.Content = but3Click;
            Grid.SetRow(lbl3, 2);
            Grid.SetColumn(lbl3, 2);
            gameGrid.Children.Add(lbl3);
            /// Кнопка 3
            but3 = LVL2_CreateButton();
            but3.Click += But3_Click;
            but3.Margin = new Thickness(10);
            Grid.SetRow(but3, 0);
            Grid.SetColumn(but3, 2);
            gameGrid.Children.Add(but3);


            /// Лейбл 4 
            lbl4 = LVL2_CreateLabel();
            lbl4.Margin = new Thickness(10);
            lbl4.Content = but4Click;
            Grid.SetRow(lbl4, 0);
            Grid.SetColumn(lbl4, 3);
            gameGrid.Children.Add(lbl4);
            /// Кнопка 4
            but4 = LVL2_CreateButton();
            but4.Click += But4_Click;
            but4.Margin = new Thickness(10);
            Grid.SetRow(but4, 1);
            Grid.SetColumn(but4, 1);
            gameGrid.Children.Add(but4);


            /// Кнопка перехода на следующий уровень
            butExit = LVL2_CreateButton();
            butExit.Content = "Жамк";
            butExit.IsEnabled = false;
            butExit.Click += ButExit_Click;
            butExit.Margin = new Thickness(10);
            Grid.SetRow(butExit, 2);
            Grid.SetColumn(butExit, 1);
            gameGrid.Children.Add(butExit);


            /// Лейбл с подсказкой
            lblEnter = LVL2_CreateLabel();
            lblEnter.Margin = new Thickness(5);
            lblEnter.Content = "Тыкни Enter";
            lblEnter.Visibility = Visibility.Hidden;
            Grid.SetRow(lblEnter, 0);
            Grid.SetColumn(lblEnter, 1);
            gameGrid.Children.Add(lblEnter);


            /// Текстбокс для ввода
            txtbox = LVL2_CreateTextBox();
            txtbox.KeyDown += Txtbox_KeyDown;
            Grid.SetRow(txtbox, 1);
            Grid.SetColumn(txtbox, 2);
            Grid.SetColumnSpan(txtbox, 2);
            gameGrid.Children.Add(txtbox);

            CreateLVLlabel();
        }




        /// <summary>
        /// Загружает третий уровень на форму.
        /// </summary>
        public void LVL3_Load()
        {
            CreateLVLlabel();
        }

        /// <summary>
        /// Полностью очищает форму.
        /// </summary>
        public void ClearLvL() {
            if(gameGrid.Children.Count != 0) {
                int lenghtGridChildren = gameGrid.Children.Count - 1;           // Количество элементов в игровом Гриде.
                for (int i = lenghtGridChildren; i >= 0; i--)
                    gameGrid.Children.RemoveAt(i);
            }
            if(gameGrid.ColumnDefinitions.Count != 0) {
                int countGridColumn = gameGrid.ColumnDefinitions.Count - 1;     // Количество столбцов в игровом Гриде.
                for (int i = countGridColumn; i >= 0; i--)
                    gameGrid.ColumnDefinitions.RemoveAt(i);
            }
            if (gameGrid.RowDefinitions.Count != 0) {
                int countGridrow = gameGrid.RowDefinitions.Count - 1;           // Количество строк в игровом Гриде.
                for (int i = countGridrow; i >= 0; i--)
                    gameGrid.RowDefinitions.RemoveAt(i);
            }
            if (numberLVLGrid.Children.Count != 0) {
                int lenghtGridChildren = numberLVLGrid.Children.Count - 1;      // Количество элементов в Гриде с номером уровня.
                for (int i = lenghtGridChildren; i >= 0; i--)
                    numberLVLGrid.Children.RemoveAt(i);
            }
        }

        /// <summary>
        /// Вывод номера уровня.
        /// </summary>
        public void CreateLVLlabel() {
            numberLVLlabel = new Label { Content = $"Уровень {numberLVL}", FontSize = 25, };
            numberLVLGrid.Children.Add(numberLVLlabel);
        }
    }
}
