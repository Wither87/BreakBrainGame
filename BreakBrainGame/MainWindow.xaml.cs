using System;
using System.Diagnostics;
using System.IO;
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
        const int butSize = 100;

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
            Button but = new Button { Margin = new Thickness(5), FontSize = 20, Content = "ЖМяк", };
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
                case 2: but.Content = "Жмяк"; break;
                case 3: but.Height = 10; but.Width = 10; Grid.SetRow(but, 1); Grid.SetColumn(but, 3); break;
                case 4: but.Height = butSize; but.Width = butSize; but.Content = "Не ЖмЯкАй((("; but.FontSize = 17; Margin = new Thickness(10); Grid.SetRow(but, 4); Grid.SetColumn(but, 4); break;
                case 5: MessageBox.Show("Не жмякай!!!"); break;
                case 6: but.Width = 200; but.FontSize = 20; but.Content = "Ahhh, sempai..."; Grid.SetRow(but, 1); Grid.SetColumn(but, 0); Grid.SetColumnSpan(but, 2); break;
                case 7: MessageBox.Show("Бака!!"); MessageBox.Show("Бака!!"); MessageBox.Show("Бака!!"); MessageBox.Show("Бака!!"); MessageBox.Show("Бака!!"); break;
                case 8: 
                    but.Height = butSize;
                    but.Width = butSize;
                    but.Content = "Подумой..";
                    Grid.SetRow(but, 0);
                    Grid.SetColumn(but, 4);
                    Label label = new Label {
                        VerticalAlignment = VerticalAlignment.Bottom,
                        FontSize = 15,
                        Content = "Ты же понимаешь что тебе будет плохо, если жмякнешь ещё раз",
                    };
                    Grid.SetRow(label, 4);
                    Grid.SetColumn(label, 0);
                    Grid.SetColumnSpan(label, 5);
                    gameGrid.Children.Add(label);
                    label.MouseLeftButtonUp += Label_MouseLeftButtonUp;
                    break;
                case 9: 
                    MessageBox.Show("Я тебя предупреждал!");
                    if (time != _time)
                        for (int i = 0; i < 3; Process.Start("notepad"), i++) ;
                    break;
            }
        }

        /// <summary>
        /// Нажатие на текст для перехода на следующий уровень
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

            gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition());

            gameGrid.RowDefinitions.Add(new RowDefinition());
            gameGrid.RowDefinitions.Add(new RowDefinition());
            gameGrid.RowDefinitions.Add(new RowDefinition());
            gameGrid.RowDefinitions.Add(new RowDefinition());
            gameGrid.RowDefinitions.Add(new RowDefinition());

            but = LVL1_CreateButton();          // Кнопка для жмяков
            Grid.SetRow(but, 2);
            Grid.SetColumn(but, 2);
            gameGrid.Children.Add(but);

            lbl = LVL1_CreateLabel();           // Лейбл для вывода таймера
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 0);
            Grid.SetColumnSpan(lbl, 3);
            gameGrid.Children.Add(lbl);

            timer = new DispatcherTimer();      // Таймер
            timer.Tick += LVL1_Timer_Tick;
            CreateLVLlabel();                   // Отображение номера уровня
        }
         
// ---------------------------------------------------------------------------------------------------------------------------------------------------- //
// ---------------------------------------------------------------------------------------------------------------------------------------------------- //
// ---------------------------------------------------------------------------------------------------------------------------------------------------- //

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
                Margin = new Thickness(10),
            };
            return label;
        }
         
        /// <summary>
        /// Создаёт кнопку для 2 уровня
        /// </summary>
        /// <returns></returns>
        Button LVL2_CreateButton() {
            Button but = new Button { FontSize = 20, };
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

        private void Txtbox_TextChanged(object sender, TextChangedEventArgs e) {
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
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition());

            gameGrid.RowDefinitions.Add(new RowDefinition());
            gameGrid.RowDefinitions.Add(new RowDefinition());
            gameGrid.RowDefinitions.Add(new RowDefinition());
            gameGrid.RowDefinitions.Add(new RowDefinition());
            gameGrid.RowDefinitions.Add(new RowDefinition());
            gameGrid.RowDefinitions.Add(new RowDefinition());


            /// Лейбл 1
            lbl1 = LVL2_CreateLabel();
            lbl1.Content = but1Click;
            Grid.SetRow(lbl1, 1);
            Grid.SetColumn(lbl1, 4);
            gameGrid.Children.Add(lbl1);
            /// Кнопка 1
            but1 = LVL2_CreateButton();
            but1.Content = 1;
            but1.Click += But1_Click;
            but1.Margin = new Thickness(10);
            Grid.SetRow(but1, 0);
            Grid.SetColumn(but1, 0);
            gameGrid.Children.Add(but1);


            /// Лейбл 2
            lbl2 = LVL2_CreateLabel();
            lbl2.Content = but2Click;
            Grid.SetRow(lbl2, 0);
            Grid.SetColumn(lbl2, 7);
            gameGrid.Children.Add(lbl2);
            /// Кнопка 2
            but2 = LVL2_CreateButton();
            but2.Content = 2;
            but2.Click += But2_Click;
            but2.Margin = new Thickness(10);
            Grid.SetRow(but2, 4);
            Grid.SetColumn(but2, 3);
            gameGrid.Children.Add(but2);


            /// Лейбл 3
            lbl3 = LVL2_CreateLabel();
            lbl3.Content = but3Click;
            Grid.SetRow(lbl3, 0);
            Grid.SetColumn(lbl3, 1);
            gameGrid.Children.Add(lbl3);
            /// Кнопка 3
            but3 = LVL2_CreateButton();
            but3.Content = 3;
            but3.Click += But3_Click;
            but3.Margin = new Thickness(10);
            Grid.SetRow(but3, 3);
            Grid.SetColumn(but3, 7);
            gameGrid.Children.Add(but3);


            /// Лейбл 4 
            lbl4 = LVL2_CreateLabel();
            lbl4.Content = but4Click;
            Grid.SetRow(lbl4, 3);
            Grid.SetColumn(lbl4, 3);
            gameGrid.Children.Add(lbl4);
            /// Кнопка 4
            but4 = LVL2_CreateButton();
            but4.Content = 4;
            but4.Click += But4_Click;
            but4.Margin = new Thickness(10);
            Grid.SetRow(but4, 0);
            Grid.SetColumn(but4, 6);
            gameGrid.Children.Add(but4);


            /// Кнопка перехода на следующий уровень
            butExit = LVL2_CreateButton();
            butExit.Content = "Next";
            butExit.IsEnabled = false;
            butExit.Click += ButExit_Click;
            butExit.Margin = new Thickness(10);
            Grid.SetRow(butExit, 2);
            Grid.SetColumn(butExit, 0);
            gameGrid.Children.Add(butExit);


            /// Лейбл с подсказкой
            lblEnter = LVL2_CreateLabel();
            lblEnter.Margin = new Thickness(5);
            lblEnter.Content = "Не кликай!!";
            lblEnter.Visibility = Visibility.Hidden;
            Grid.SetRow(lblEnter, 5);
            Grid.SetColumn(lblEnter, 0);
            Grid.SetColumnSpan(lblEnter, 3);
            gameGrid.Children.Add(lblEnter);


            /// Текстбокс для ввода
            txtbox = LVL2_CreateTextBox();
            txtbox.TextChanged += Txtbox_TextChanged;
            Grid.SetRow(txtbox, 2);
            Grid.SetColumn(txtbox, 2);
            Grid.SetColumnSpan(txtbox, 3);
            gameGrid.Children.Add(txtbox);

            CreateLVLlabel();
        }

// ---------------------------------------------------------------------------------------------------------------------------------------------------- //
// ---------------------------------------------------------------------------------------------------------------------------------------------------- //
// ---------------------------------------------------------------------------------------------------------------------------------------------------- //

        // Необходимые поля для 3 уровня
        string[] badAnswer = File.ReadAllLines($"../../../BadAnswers.txt");
        string[] goodAnswer = File.ReadAllLines($"../../../GoodAnswers.txt");
        Random rnd = new Random();

        Label questionLabel1;
        Label questionLabel2;
        Label questionLabel3;
        Label questionLabel4;
        Label questionLabel5;

        ComboBox answerComboBox1;
        ComboBox answerComboBox2;
        ComboBox answerComboBox3;
        ComboBox answerComboBox4;
        ComboBox answerComboBox5;

        bool boolAnswer1 = false;
        bool boolAnswer2 = false;
        bool boolAnswer3 = false;
        bool boolAnswer4 = false;
        bool boolAnswer5 = false;

        Button exitButtonlvl3;

        Label LVL3_CreateLabel(string text) {
            Label lbl = new Label { Content = text, FontSize = 20, };
            return lbl;
        } 
        ComboBox LVL3_CreateComboBox(string answer1, string answer2, string answer3) {
            ComboBox combo = new ComboBox { Margin = new Thickness(5), FontSize = 15, };
            combo.Items.Add(answer1);
            combo.Items.Add(answer2);
            combo.Items.Add(answer3);
            return combo;
        }

        private void AnswerComboBox1_SelectionChanged(object sender, SelectionChangedEventArgs e) { 
            if (answerComboBox1.SelectedIndex == 0) {
                MessageBox.Show(goodAnswer[rnd.Next(0, goodAnswer.Length)]);
                boolAnswer1 = true;
            }
             else
                MessageBox.Show(badAnswer[rnd.Next(0, badAnswer.Length)]);
            CheckAnswers();
        } 
        private void AnswerComboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (answerComboBox2.SelectedIndex == 2) {
                MessageBox.Show(goodAnswer[rnd.Next(0, goodAnswer.Length)]);
                boolAnswer2 = true;
            }
            else
                MessageBox.Show(badAnswer[rnd.Next(0, badAnswer.Length)]);
            CheckAnswers();
        } 
        private void AnswerComboBox3_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (answerComboBox3.SelectedIndex == 2) {
                MessageBox.Show(goodAnswer[rnd.Next(0, goodAnswer.Length)]);
                boolAnswer3 = true;
            }
            else
                MessageBox.Show(badAnswer[rnd.Next(0, badAnswer.Length)]);
            CheckAnswers();
        }
        private void AnswerComboBox4_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (answerComboBox4.SelectedIndex == 1) {
                MessageBox.Show(goodAnswer[rnd.Next(0, goodAnswer.Length)]);
                boolAnswer4 = true;
            }
            else
                MessageBox.Show(badAnswer[rnd.Next(0, badAnswer.Length)]);
            CheckAnswers();
        }
        private void AnswerComboBox5_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (answerComboBox5.SelectedIndex == 0) {
                MessageBox.Show(goodAnswer[rnd.Next(0, goodAnswer.Length)]);
                boolAnswer5 = true;
            }
            else
                MessageBox.Show(badAnswer[rnd.Next(0, badAnswer.Length)]);
            CheckAnswers();
        }
        //   Don’t repeat yourself  так сказать


        private void ExitButtonlvl3_Click(object sender, RoutedEventArgs e) {
            ClearLvL(); numberLVL++; LVL4_Load();
        }

        void CheckAnswers()
        {
            if (boolAnswer1 && boolAnswer2 && boolAnswer3 && boolAnswer4 && boolAnswer5)
                exitButtonlvl3.IsEnabled = true;
            else
                exitButtonlvl3.IsEnabled = false;
        }

        /// <summary>
        /// Загружает третий уровень на форму.
        /// </summary>
        public void LVL3_Load()
        {
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition());
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition());

            gameGrid.RowDefinitions.Add(new RowDefinition());
            gameGrid.RowDefinitions.Add(new RowDefinition());
            gameGrid.RowDefinitions.Add(new RowDefinition());
            gameGrid.RowDefinitions.Add(new RowDefinition());
            gameGrid.RowDefinitions.Add(new RowDefinition());
            gameGrid.RowDefinitions.Add(new RowDefinition());

            // Лейбл 1 с вопросом
            questionLabel1 = LVL3_CreateLabel("Кто ты?");                               // 1 вопрос
            Grid.SetColumn(questionLabel1, 0);
            Grid.SetColumnSpan(questionLabel1, 3);
            Grid.SetRow(questionLabel1, 0);
            gameGrid.Children.Add(questionLabel1);
            // Комбобокс 1 с ответами
            answerComboBox1 = LVL3_CreateComboBox("Субъект", "Камень", "Кто же я?..."); // Ответы
            answerComboBox1.SelectionChanged += AnswerComboBox1_SelectionChanged;
            Grid.SetColumn(answerComboBox1, 3);
            Grid.SetRow(answerComboBox1, 0);
            gameGrid.Children.Add(answerComboBox1);

            // Лейбл 2 с вопросом
            questionLabel2 = LVL3_CreateLabel("Сходства краба и тиммейта");             // 2 вопрос
            Grid.SetColumn(questionLabel2, 0);
            Grid.SetColumnSpan(questionLabel2, 3);
            Grid.SetRow(questionLabel2, 1);
            gameGrid.Children.Add(questionLabel2);
            // Комбобокс 2 с ответами
            answerComboBox2 = LVL3_CreateComboBox("Оба красные", "Есть клешни", "Они организмы"); //Ответы
            answerComboBox2.SelectionChanged += AnswerComboBox2_SelectionChanged;
            Grid.SetColumn(answerComboBox2, 3);
            Grid.SetRow(answerComboBox2, 1);
            gameGrid.Children.Add(answerComboBox2);

            // Лейбл 3 с вопросом
            questionLabel3 = LVL3_CreateLabel("Какая фамилия у Трампа?");               // 3 вопрос
            Grid.SetColumn(questionLabel3, 0);
            Grid.SetColumnSpan(questionLabel3, 3);
            Grid.SetRow(questionLabel3, 2);
            gameGrid.Children.Add(questionLabel3);
            // Комбобокс 3 с ответами
            answerComboBox3 = LVL3_CreateComboBox("Каво...", "Путин", "Трамп..");       // Ответы
            answerComboBox3.SelectionChanged += AnswerComboBox3_SelectionChanged;
            Grid.SetColumn(answerComboBox3, 3);
            Grid.SetRow(answerComboBox3, 2);
            gameGrid.Children.Add(answerComboBox3);

            // Лейбл 4 с вопросом
            questionLabel4 = LVL3_CreateLabel("Не является рыбой");                     // 4 вопрос
            Grid.SetColumn(questionLabel4, 0);
            Grid.SetColumnSpan(questionLabel4, 3);
            Grid.SetRow(questionLabel4, 3);
            gameGrid.Children.Add(questionLabel4);
            // Комбобокс 4 с ответами
            answerComboBox4 = LVL3_CreateComboBox("Кальмар", "Коммунист", "Акула");     // Ответы
            answerComboBox4.SelectionChanged += AnswerComboBox4_SelectionChanged;
            Grid.SetColumn(answerComboBox4, 3);
            Grid.SetRow(answerComboBox4, 3);
            gameGrid.Children.Add(answerComboBox4);

            // Лейбл 5 с вопросом
            questionLabel5 = LVL3_CreateLabel("Когда началась Вторая мировая война");   // 5 вопрос
            Grid.SetColumn(questionLabel5, 0);
            Grid.SetColumnSpan(questionLabel5, 3);
            Grid.SetRow(questionLabel5, 4);
            gameGrid.Children.Add(questionLabel5);
            // Комбобокс 5 с ответами
            answerComboBox5 = LVL3_CreateComboBox("В 20 веке", "В девяностых", "В 2007"); // Ответы
            answerComboBox5.SelectionChanged += AnswerComboBox5_SelectionChanged;
            Grid.SetColumn(answerComboBox5, 3);
            Grid.SetRow(answerComboBox5, 4);
            gameGrid.Children.Add(answerComboBox5);

            // Кнопка для перехода на следующий уровень
            exitButtonlvl3 = new Button { IsEnabled = false, Content = "Next Level", FontSize = 15, };
            exitButtonlvl3.Click += ExitButtonlvl3_Click;
            Grid.SetColumn(exitButtonlvl3, 1);
            Grid.SetColumnSpan(exitButtonlvl3, 2);
            Grid.SetRow(exitButtonlvl3, 5);
            gameGrid.Children.Add(exitButtonlvl3);

            CreateLVLlabel();
        }

// ---------------------------------------------------------------------------------------------------------------------------------------------------- //
// ---------------------------------------------------------------------------------------------------------------------------------------------------- //
// ---------------------------------------------------------------------------------------------------------------------------------------------------- //


        void LVL4_Load()
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
            numberLVLlabel = new Label { Content = $"Уровень {numberLVL}", FontSize = 15, };
            numberLVLGrid.Children.Add(numberLVLlabel);
        }
    }
}
