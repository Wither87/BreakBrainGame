using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Media;

namespace BreakBrainGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IGame
    {
        int messageValue = 0;
        Button nextLvlButton;

        void CheckMessageValue() {
            messageValue++;
            switch (messageValue) {
                case 6:  MessageBox.Show("Прости если я тебе мешаю играть)"); messageValue++; break;
                case 10: MessageBox.Show("Ну как тебе игра?)"); messageValue++; break;
                case 14: MessageBox.Show("Хмм, я тебя не дооценил, ты всё ещё играешь"); messageValue++; break;
                case 19: MessageBox.Show("Слушай, у тебя стальные нервы, если ты ещё продолжаешь играть)"); messageValue++; break;
                case 24: MessageBox.Show("Я тебе ещё не надоел?)"); messageValue++; break;
            }
        }

        /// <summary>
        /// Полностью очищает форму.
        /// </summary>
        public void ClearLvL()
        {
            if (gameGrid.Children.Count != 0) {
                int lenghtGridChildren = gameGrid.Children.Count - 1;           // Количество элементов в игровом Гриде.
                for (int i = lenghtGridChildren; i >= 0; i--)
                    gameGrid.Children.RemoveAt(i);
            }
            if (gameGrid.ColumnDefinitions.Count != 0) {
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
            numberLVLlabel = new Label { Content = $"Уровень {++numberLVL}", FontSize = 15, };
            numberLVLGrid.Children.Add(numberLVLlabel);
        }

        // Необходимые поля для 1 уровня
        const int _time = 15;
        int time = _time;
        int clickCount = 0;
        int numberLVL = 0;
        Button but;
        Label lbl;
        Label numberLVLlabel;
        DispatcherTimer timer;
        const int butSize = 100;

        public MainWindow() {
            InitializeComponent();
            //LVL1_Load();
            //LVL2_Load();
            //LVL3_Load();
            //LVL4_Load();
            //LVL5_Load();
            LVL6_Load();
        }

        /// <summary>
        /// Клики на кнопку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LVL1_But_Click(object sender, RoutedEventArgs e) {
            clickCount++;               // Прибавлять количество кликов.
            if (clickCount == 1)        // Запуск таймера.
                LVL1_Timer();
            LVL1_CheckCountClick();     // Проверка на количество кликов.
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
        private void LVL1_CheckCountClick() {
            switch (clickCount) {
                case 1: but.Content = "Тык"; break;
                case 2: but.Content = "Жмяк"; break;
                case 3: but.Height = 10; but.Width = 10; Grid.SetRow(but, 1); Grid.SetColumn(but, 3); break;
                case 4: but.Height = butSize; but.Width = butSize; but.Content = "Не ЖмЯкАй((("; but.FontSize = 17; Margin = new Thickness(10); Grid.SetRow(but, 4); Grid.SetColumn(but, 4); break;
                case 5: MessageBox.Show("Не жмякай!!!"); CheckMessageValue(); break;
                case 6: but.Width = 200; but.FontSize = 20; but.Content = "Ahhh, sempai..."; Grid.SetRow(but, 1); Grid.SetColumn(but, 0); Grid.SetColumnSpan(but, 2); break;
                case 7: MessageBox.Show("Бака!!"); MessageBox.Show("Бака!!"); MessageBox.Show("Бака!!"); MessageBox.Show("Бака!!"); MessageBox.Show("Бака!!");CheckMessageValue(); break;
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
                    label.MouseLeftButtonUp += (s, e) => { timer.Stop(); ClearLvL(); LVL2_Load(); };
                    break;
                case 9: 
                    MessageBox.Show("Я тебя предупреждал!");
                    CheckMessageValue();
                    if (time != _time)
                        for (int i = 0; i < 3; Process.Start("notepad"), i++) ;
                    break;
            }
        }

        /// <summary>
        /// Действия на каждый тик таймера.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LVL1_Timer_Tick(object sender, EventArgs e) {
            time--;
            lbl.Content = $"Время до переустановки винды: {time}";
            if(time > 0) {
                if (clickCount >= 10) { timer.Stop(); }
            }
            else { timer.Stop(); clickCount = 0; time = _time; ClearLvL(); numberLVL--; LVL1_Load(); }
        }

        /// <summary>
        /// Загружает первый уровень на форму.
        /// </summary>
        public void LVL1_Load() {

            for (int i = 0; i < 5; gameGrid.ColumnDefinitions.Add(new ColumnDefinition()), i++) ;
            for (int i = 0; i < 5; gameGrid.RowDefinitions.Add(new RowDefinition()), i++) ;

            but = new Button { Margin = new Thickness(5), FontSize = 20, Content = "ЖМяк", };
            but.Click += LVL1_But_Click;        // Кнопка для жмяков         
            Grid.SetRow(but, 2);
            Grid.SetColumn(but, 2);
            gameGrid.Children.Add(but);

            lbl = new Label { VerticalAlignment = VerticalAlignment.Top, HorizontalAlignment = HorizontalAlignment.Left, FontSize = 15, };  // Лейбл для вывода таймера
            Grid.SetRow(lbl, 0);
            Grid.SetColumn(lbl, 0);
            Grid.SetColumnSpan(lbl, 3);
            gameGrid.Children.Add(lbl);

            timer = new DispatcherTimer();      // Таймер
            timer.Tick += LVL1_Timer_Tick;
            CreateLVLlabel();                   // Отображение номера уровня

            MessageBox.Show("Привеет))");
            CheckMessageValue();
        }
         
// ---------------------------------------------------------------------------------------------------------------------------------------------------- //
// ---------------------------------------------------------------------------------------------------------------------------------------------------- //
// ---------------------------------------------------------------------------------------------------------------------------------------------------- //

        // Необходимые поля для 2 уровня
        Button but1, but2, but3, but4;
        Label lbl1, lbl2, lbl3, lbl4, lblHint;
        int but1Click = 0, but2Click = 0, but3Click = 0, but4Click = 0;
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
            Button but = new Button { FontSize = 20, Margin = new Thickness(10), };
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
            if (but3Click == 14) lblHint.Visibility = Visibility.Visible;  // Включить видимость Лейбла
            else lblHint.Visibility = Visibility.Hidden;                   // Отключить видимость Лейбла
            if (but3Click >= 15) but3Click = 0;     // Обнулять счётчик
        }

        private void But4_Click(object sender, RoutedEventArgs e) {
            but4Click++;
            lbl4.Content = but4Click;
            if (but4Click == 8) {
                MessageBox.Show("Собери пазл))");
                CheckMessageValue();
            }                                       // Подсказка

            if (but4Click >= 15) but4Click = 0;     // Обнулять счётчик
        }

        private void Txtbox_TextChanged(object sender, TextChangedEventArgs e) {
            if (txtbox.Text == "103148") { nextLvlButton.IsEnabled = true; }
        }

        /// <summary>
        /// Загружает второй уровень на форму.
        /// </summary>
        public void LVL2_Load()
        {
            for (int i = 0; i < 8; gameGrid.ColumnDefinitions.Add(new ColumnDefinition()), i++);
            for (int i = 0; i < 6; gameGrid.RowDefinitions.Add(new RowDefinition()), i++);
            
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
            Grid.SetRow(but4, 0);
            Grid.SetColumn(but4, 6);
            gameGrid.Children.Add(but4);

            /// Кнопка перехода на следующий уровень
            nextLvlButton = LVL2_CreateButton();
            nextLvlButton.Content = "Next";
            nextLvlButton.IsEnabled = false;
            nextLvlButton.Click += (s, e) => { ClearLvL(); LVL3_Load(); };
            Grid.SetRow(nextLvlButton, 2);
            Grid.SetColumn(nextLvlButton, 0);
            gameGrid.Children.Add(nextLvlButton);

            /// Лейбл с подсказкой
            lblHint = LVL2_CreateLabel();
            lblHint.Margin = new Thickness(5);
            lblHint.Content = "Не кликай!!";
            lblHint.Visibility = Visibility.Hidden;
            Grid.SetRow(lblHint, 5);
            Grid.SetColumn(lblHint, 0);
            Grid.SetColumnSpan(lblHint, 3);
            gameGrid.Children.Add(lblHint);

            /// Текстбокс для ввода
            txtbox = LVL2_CreateTextBox();
            txtbox.TextChanged += Txtbox_TextChanged;
            Grid.SetRow(txtbox, 2);
            Grid.SetColumn(txtbox, 2);
            Grid.SetColumnSpan(txtbox, 3);
            gameGrid.Children.Add(txtbox);

            CreateLVLlabel();
            MessageBox.Show("Ты прошёл 1 уровень. Так держать!");
            CheckMessageValue();
        }

// ---------------------------------------------------------------------------------------------------------------------------------------------------- //
// ---------------------------------------------------------------------------------------------------------------------------------------------------- //
// ---------------------------------------------------------------------------------------------------------------------------------------------------- //

        // Необходимые поля для 3 уровня
        string[] badAnswer = File.ReadAllLines($"../../../BadAnswers.txt");
        string[] goodAnswer = File.ReadAllLines($"../../../GoodAnswers.txt");

        Label questionLabel1, questionLabel2, questionLabel3, questionLabel4, questionLabel5;
        ComboBox answerComboBox1, answerComboBox2, answerComboBox3, answerComboBox4, answerComboBox5;
        bool boolAnswer1 = false, boolAnswer2 = false, boolAnswer3 = false, boolAnswer4 = false, boolAnswer5 = false;
        
        int[] selectedIndexOld = new int[5], selectedIndexNew;

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

        private void AnswerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            selectedIndexNew = new int[] {
                answerComboBox1.SelectedIndex,
                answerComboBox2.SelectedIndex,
                answerComboBox3.SelectedIndex,
                answerComboBox4.SelectedIndex,
                answerComboBox5.SelectedIndex,
            };
            ChangeCheckBox();
        }

        void ChangeCheckBox() {
            for (int i = 0; i < selectedIndexNew.Length; i++)
                if(selectedIndexNew[i] != selectedIndexOld[i])
                    switch (i) {
                        case 0:
                            if (answerComboBox1.SelectedIndex == 0) { MessageBox.Show(goodAnswer[new Random().Next(0, goodAnswer.Length)]); boolAnswer1 = true; }
                            else { MessageBox.Show(badAnswer[new Random().Next(0, badAnswer.Length)]); boolAnswer1 = false; }
                            break;
                        case 1:
                            if (answerComboBox2.SelectedIndex == 2) { MessageBox.Show(goodAnswer[new Random().Next(0, goodAnswer.Length)]); boolAnswer2 = true; }
                            else { MessageBox.Show(badAnswer[new Random().Next(0, badAnswer.Length)]); boolAnswer2 = false; }
                            break;
                        case 2:
                            if (answerComboBox2.SelectedIndex == 2) { MessageBox.Show(goodAnswer[new Random().Next(0, goodAnswer.Length)]); boolAnswer3 = true; }
                            else { MessageBox.Show(badAnswer[new Random().Next(0, badAnswer.Length)]); boolAnswer3 = false; }
                            break;
                        case 3:
                            if (answerComboBox4.SelectedIndex == 1) { MessageBox.Show(goodAnswer[new Random().Next(0, goodAnswer.Length)]); boolAnswer4 = true; }
                            else { MessageBox.Show(badAnswer[new Random().Next(0, badAnswer.Length)]); boolAnswer4 = false; }
                            break;
                        case 4:
                            if (answerComboBox5.SelectedIndex == 0) { MessageBox.Show(goodAnswer[new Random().Next(0, goodAnswer.Length)]); boolAnswer5 = true; }
                            else { MessageBox.Show(badAnswer[new Random().Next(0, badAnswer.Length)]); boolAnswer5 = false; }
                            break;
                    }
            CheckAnswers();
            CheckMessageValue();

            for (int i = 0; i < selectedIndexOld.Length; i++)
                selectedIndexOld[i] = selectedIndexNew[i];
        }

        void CheckAnswers() {
            if (boolAnswer1 && boolAnswer2 && boolAnswer3 && boolAnswer4 && boolAnswer5)
                nextLvlButton.IsEnabled = true;
            else
                nextLvlButton.IsEnabled = false;
        }

        /// <summary>
        /// Загружает третий уровень на форму.
        /// </summary>
        public void LVL3_Load() {
            for (int i = 0; i < 4; gameGrid.ColumnDefinitions.Add(new ColumnDefinition()), i++) ;
            for (int i = 0; i < 6; gameGrid.RowDefinitions.Add(new RowDefinition()), i++) ;

            // Лейбл 1 с вопросом
            questionLabel1 = LVL3_CreateLabel("Кто ты?");                               // 1 вопрос
            Grid.SetColumn(questionLabel1, 0);
            Grid.SetColumnSpan(questionLabel1, 3);
            Grid.SetRow(questionLabel1, 0);
            gameGrid.Children.Add(questionLabel1);
            // Комбобокс 1 с ответами
            answerComboBox1 = LVL3_CreateComboBox("Субъект", "Камень", "Кто же я?..."); // Ответы
            answerComboBox1.SelectionChanged += AnswerComboBox_SelectionChanged;
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
            answerComboBox2.SelectionChanged += AnswerComboBox_SelectionChanged;
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
            answerComboBox3.SelectionChanged += AnswerComboBox_SelectionChanged;
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
            answerComboBox4.SelectionChanged += AnswerComboBox_SelectionChanged;
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
            answerComboBox5.SelectionChanged += AnswerComboBox_SelectionChanged;
            Grid.SetColumn(answerComboBox5, 3);
            Grid.SetRow(answerComboBox5, 4);
            gameGrid.Children.Add(answerComboBox5);

            // Кнопка для перехода на следующий уровень
            nextLvlButton = new Button { IsEnabled = false, Content = "Next Level", FontSize = 15, };
            nextLvlButton.Click += (s, e) => { ClearLvL(); LVL4_Load(); };
            Grid.SetColumn(nextLvlButton, 1);
            Grid.SetColumnSpan(nextLvlButton, 2);
            Grid.SetRow(nextLvlButton, 5);
            gameGrid.Children.Add(nextLvlButton);

            selectedIndexOld[0] = answerComboBox1.SelectedIndex;
            selectedIndexOld[1] = answerComboBox2.SelectedIndex;
            selectedIndexOld[2] = answerComboBox3.SelectedIndex;
            selectedIndexOld[3] = answerComboBox4.SelectedIndex;
            selectedIndexOld[4] = answerComboBox5.SelectedIndex;

            CreateLVLlabel();
            MessageBox.Show("Ты разгадал загадку на 2 уровене. Да ты крут!");
            CheckMessageValue();
        }

// ---------------------------------------------------------------------------------------------------------------------------------------------------- //
// ---------------------------------------------------------------------------------------------------------------------------------------------------- //
// ---------------------------------------------------------------------------------------------------------------------------------------------------- //

        RadioButton[] radio_group1 = new RadioButton[5];
        RadioButton[] radio_group2 = new RadioButton[5];
        CheckBox[,] checkBoxes = new CheckBox[3,5];
        bool checkBox1Checked = false, checkBox2Checked = false, checkBox3Checked = false, checkBox4Checked = false, radioBut1Cheked = false, radioBut2Cheked = false;

        void EmbedRadioBut(RadioButton[] rb, int columnCount, int row, string groupName) {
            for (int i = 0; i < columnCount; i++) {
                rb[i] = new RadioButton {
                    GroupName = groupName,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    IsEnabled = false,
                };
                rb[i].Click += CompleteLVLCheck;
                Grid.SetColumn(rb[i], i);
                Grid.SetRow(rb[i], row);
                gameGrid.Children.Add(rb[i]);
            }
        }

        void EmbedCheckBox(CheckBox[,] checkBox, int rowCount, int columnCount) {
            for (int i = 0; i < rowCount; i++)
                for (int j = 0; j < columnCount; j++) {
                    checkBox[i, j] = new CheckBox {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    };
                    checkBox[i, j].Click += CompleteLVLCheck;
                    Grid.SetColumn(checkBox[i, j], j);
                    Grid.SetRow(checkBox[i, j], i + 1);
                    gameGrid.Children.Add(checkBox[i, j]);
                }
        }

        private void CheckBox_0_1_Checked(object sender, RoutedEventArgs e) {
            for (int i = 0; i < radio_group1.Length; i++)
                radio_group1[i].IsEnabled = true;
            
        }
        private void CheckBox_0_1_Unchecked(object sender, RoutedEventArgs e) {
            for (int i = 0; i < radio_group1.Length; i++) {
                radio_group1[i].IsChecked = false;
                radio_group1[i].IsEnabled = false;
            }
        }
        private void CheckBox_1_4_Checked(object sender, RoutedEventArgs e) {
            for (int i = 0; i < radio_group2.Length; i++)
                radio_group2[i].IsEnabled = true;
            
        }
        private void CheckBox_1_4_Unchecked(object sender, RoutedEventArgs e) {
            for (int i = 0; i < radio_group2.Length; i++) {
                radio_group2[i].IsChecked = false;
                radio_group2[i].IsEnabled = false;
            }
        }
        private void CheckBox_1_2_Checked(object sender, RoutedEventArgs e) {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 5; j++)
                    if (checkBoxes[i, j] != checkBoxes[1, 2])  
                        checkBoxes[i, j].Visibility = Visibility.Hidden;
            nextLvlButton.Visibility = Visibility.Visible;
        }
        private void CheckBox_1_2_Unchecked(object sender, RoutedEventArgs e) {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 5; j++)
                    checkBoxes[i, j].Visibility = Visibility.Visible;
            nextLvlButton.Visibility = Visibility.Hidden;
        }
        private void CheckBox_2_2_Checked(object sender, RoutedEventArgs e) {
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 5; j++)
                    checkBoxes[i, j].IsChecked = false;
            for (int i = 0; i < radio_group1.Length; i++)
                radio_group1[i].IsChecked = false;
            for (int i = 0; i < radio_group2.Length; i++)
                radio_group2[i].IsChecked = false;
            MessageBox.Show("Хыхых, всёже ты сюда нажал)");
            CheckMessageValue();
        }

        private void CompleteLVLCheck(object sender, RoutedEventArgs e) {
            if (checkBox1Checked && checkBox2Checked && checkBox3Checked && checkBox4Checked && radioBut1Cheked && radioBut2Cheked)
                nextLvlButton.IsEnabled = true;
            else 
                nextLvlButton.IsEnabled = false;
        }

        void LVL4_Load() {
            for (int i = 0; i < 5; gameGrid.ColumnDefinitions.Add(new ColumnDefinition()), i++) ;
            for (int i = 0; i < 5; gameGrid.RowDefinitions.Add(new RowDefinition()), i++) ;

            EmbedRadioBut(radio_group1, 5, 0, "group1");
            EmbedRadioBut(radio_group2, 5, 4, "group2");
            EmbedCheckBox(checkBoxes, 3, 5);

            nextLvlButton = new Button { Content = "Next", Visibility = Visibility.Hidden, IsEnabled = false, FontSize = 18 };
            nextLvlButton.Click += (s, e) => { ClearLvL(); LVL5_Load(); };
            Grid.SetColumn(nextLvlButton, 2);
            Grid.SetRow(nextLvlButton, 3);
            gameGrid.Children.Add(nextLvlButton);

            checkBoxes[0, 1].Checked += CheckBox_0_1_Checked;
            checkBoxes[0, 1].Unchecked += CheckBox_0_1_Unchecked;

            checkBoxes[1, 4].Checked += CheckBox_1_4_Checked;
            checkBoxes[1, 4].Unchecked += CheckBox_1_4_Unchecked;

            checkBoxes[1, 2].Checked += CheckBox_1_2_Checked;
            checkBoxes[1, 2].Unchecked += CheckBox_1_2_Unchecked;

            checkBoxes[2, 2].Checked += CheckBox_2_2_Checked;

            checkBoxes[0, 0].Checked += (s, e) => checkBox1Checked = true;
            checkBoxes[0, 0].Unchecked += (s, e) => checkBox1Checked = false;

            checkBoxes[2, 0].Checked += (s, e) => checkBox2Checked = true;
            checkBoxes[2, 0].Unchecked += (s, e) => checkBox2Checked = false;

            checkBoxes[0, 4].Checked += (s, e) => checkBox3Checked = true;
            checkBoxes[0, 4].Unchecked += (s, e) => checkBox3Checked = false;

            checkBoxes[1, 3].Checked += (s, e) => checkBox4Checked = true;
            checkBoxes[1, 3].Unchecked += (s, e) => checkBox4Checked = false;

            radio_group1[3].Checked += (s, e) => radioBut1Cheked = true;
            radio_group1[3].Unchecked += (s, e) => radioBut1Cheked = false;

            radio_group2[0].Checked += (s, e) => radioBut2Cheked = true;
            radio_group2[0].Unchecked += (s, e) => radioBut2Cheked = false;

            CreateLVLlabel();
            MessageBox.Show("Думаю это было просто, не так ли?) Продолжаем играть!");
            CheckMessageValue();
        }

        // ---------------------------------------------------------------------------------------------------------------------------------------------------- //
        // ---------------------------------------------------------------------------------------------------------------------------------------------------- //
        // ---------------------------------------------------------------------------------------------------------------------------------------------------- //

        ListBox listBoxProducer, listBoxModels, listBoxPrices;
        ListBox incorrectListBoxProducer, incorrectListBoxModels, incorrectListBoxPrices;
        RadioButton radioButName, radioButModel, radioButPrice;
        bool[] corrects = new bool[6];

        void LVL5_CreateCorrectListBoxes() {
            listBoxProducer = new ListBox { Visibility = Visibility.Hidden };
            TextBlock textBlockProducer = new TextBlock {
                Text = "Производитель",
                FontWeight = FontWeights.Bold,
                TextDecorations = TextDecorations.Underline,
            };
            listBoxProducer.Items.Add(textBlockProducer);
            LVL5_FullInCorrectProducer();
            Grid.SetColumn(listBoxProducer, 3);
            Grid.SetRow(listBoxProducer, 0);
            Grid.SetRowSpan(listBoxProducer, 3);
            gameGrid.Children.Add(listBoxProducer);

            listBoxModels = new ListBox { Visibility = Visibility.Hidden };
            TextBlock textBlockModel = new TextBlock {
                Text = "Модель",
                FontWeight = FontWeights.Bold,
                TextDecorations = TextDecorations.Underline,
            };
            listBoxModels.Items.Add(textBlockModel);
            LVL5_FullInCorrectModels();
            Grid.SetColumn(listBoxModels, 3);
            Grid.SetRow(listBoxModels, 0);
            Grid.SetRowSpan(listBoxModels, 3);
            gameGrid.Children.Add(listBoxModels);

            listBoxPrices = new ListBox { Visibility = Visibility.Hidden };
            TextBlock textBlockPrice = new TextBlock {
                Text = "Цена",
                FontWeight = FontWeights.Bold,
                TextDecorations = TextDecorations.Underline,
            };
            listBoxPrices.Items.Add(textBlockPrice);
            LVL5_FullInCorrectPrice();
            Grid.SetColumn(listBoxPrices, 3);
            Grid.SetRow(listBoxPrices, 0);
            Grid.SetRowSpan(listBoxPrices, 3);
            gameGrid.Children.Add(listBoxPrices);
        }
        void LVL5_CreateRadioBut() {
            radioButName = new RadioButton { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center };
            radioButName.Checked += (s, e) => {
                listBoxProducer.Visibility = Visibility.Visible;
                listBoxModels.Visibility = Visibility.Hidden;
                listBoxPrices.Visibility = Visibility.Hidden;
            };
            Grid.SetColumn(radioButName, 1);
            Grid.SetRow(radioButName, 3);
            gameGrid.Children.Add(radioButName);

            radioButModel = new RadioButton { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center };
            radioButModel.Checked += (s, e) => {
                listBoxProducer.Visibility = Visibility.Hidden;
                listBoxModels.Visibility = Visibility.Visible;
                listBoxPrices.Visibility = Visibility.Hidden;
            };
            Grid.SetColumn(radioButModel, 2);
            Grid.SetRow(radioButModel, 3);
            gameGrid.Children.Add(radioButModel);

            radioButPrice = new RadioButton { VerticalAlignment = VerticalAlignment.Center, HorizontalAlignment = HorizontalAlignment.Center };
            radioButPrice.Checked += (s, e) => {
                listBoxProducer.Visibility = Visibility.Hidden;
                listBoxModels.Visibility = Visibility.Hidden;
                listBoxPrices.Visibility = Visibility.Visible;
            };
            Grid.SetColumn(radioButPrice, 3);
            Grid.SetRow(radioButPrice, 3);
            gameGrid.Children.Add(radioButPrice);
        }
        void LVL5_CreateIncorrectListBoxes() {
            incorrectListBoxProducer = new ListBox();
            TextBlock textBlockProducer = new TextBlock {
                Text = "Производитель",
                FontWeight = FontWeights.Bold,
                TextDecorations = TextDecorations.Underline,
            };
            incorrectListBoxProducer.Items.Add(textBlockProducer);
            LVL5_FullInIncorrectProducer();
            Grid.SetColumn(incorrectListBoxProducer, 0);
            Grid.SetRow(incorrectListBoxProducer, 0);
            Grid.SetRowSpan(incorrectListBoxProducer, 3);
            gameGrid.Children.Add(incorrectListBoxProducer);

            incorrectListBoxModels = new ListBox();
            TextBlock textBlockModel = new TextBlock {
                Text = "Модель",
                FontWeight = FontWeights.Bold,
                TextDecorations = TextDecorations.Underline,
            };
            incorrectListBoxModels.Items.Add(textBlockModel);
            LVL5_FullInIncorrectModel();
            Grid.SetColumn(incorrectListBoxModels, 1);
            Grid.SetRow(incorrectListBoxModels, 0);
            Grid.SetRowSpan(incorrectListBoxModels, 3);
            gameGrid.Children.Add(incorrectListBoxModels);

            incorrectListBoxPrices = new ListBox();
            TextBlock textBlockPrice = new TextBlock {
                Text = "Цена",
                FontWeight = FontWeights.Bold,
                TextDecorations = TextDecorations.Underline,
            };
            incorrectListBoxPrices.Items.Add(textBlockPrice);
            LVL5_FullInIncorrectPrice();
            Grid.SetColumn(incorrectListBoxPrices, 2);
            Grid.SetRow(incorrectListBoxPrices, 0);
            Grid.SetRowSpan(incorrectListBoxPrices, 3);
            gameGrid.Children.Add(incorrectListBoxPrices);
        }


        void LVL5_FullInIncorrectProducer() {
            Label apple = new Label { Content = "Aplle" };
            apple.MouseDoubleClick += (s, e) => { apple.Content = "Apple"; corrects[0] = true; CheckBoolCorrects(); };
            incorrectListBoxProducer.Items.Add(apple);
            incorrectListBoxProducer.Items.Add(new Label { Content = "Samsung" });
            incorrectListBoxProducer.Items.Add(new Label { Content = "Xiaomi" });
            incorrectListBoxProducer.Items.Add(new Label { Content = "HUAWEI" });
            Label lenovo = new Label { Content = "Lenowo" };
            lenovo.MouseDoubleClick += (s, e) => { lenovo.Content = "Lenovo"; corrects[1] = true; CheckBoolCorrects(); };
            incorrectListBoxProducer.Items.Add(lenovo);
            incorrectListBoxProducer.Items.Add(new Label { Content = "LG" });
        }
        void LVL5_FullInIncorrectModel() {
            incorrectListBoxModels.Items.Add(new Label { Content = "iPhone 11 64GB" });
            Label samsung = new Label { Content = "Galaxy A51 64MB" };
            samsung.MouseDoubleClick += (s, e) => { samsung.Content = "Galaxy A51 64GB"; corrects[2] = true; CheckBoolCorrects(); };
            incorrectListBoxModels.Items.Add(samsung);
            Label xiaomi = new Label { Content = "Redmi 8A 2/232GB" };
            xiaomi.MouseDoubleClick += (s, e) => { xiaomi.Content = "Redmi 8A 2/32GB"; corrects[3] = true; CheckBoolCorrects(); };
            incorrectListBoxModels.Items.Add(xiaomi);
            incorrectListBoxModels.Items.Add(new Label { Content = "P smart Z 4/64GB" });
            incorrectListBoxModels.Items.Add(new Label { Content = "A5 3 / 32GB" });
            Label lg = new Label { Content = "V40 TnihQ 6/128GB" };
            lg.MouseDoubleClick += (s, e) => { lg.Content = "V40 ThinQ 6/128GB"; corrects[4] = true; CheckBoolCorrects(); };
            incorrectListBoxModels.Items.Add(lg);
        }
        void LVL5_FullInIncorrectPrice() {
            incorrectListBoxPrices.Items.Add(new Label { Content = "54.290 ₽" });
            incorrectListBoxPrices.Items.Add(new Label { Content = "15.490 ₽" });
            incorrectListBoxPrices.Items.Add(new Label { Content = "7.140 ₽" });
            incorrectListBoxPrices.Items.Add(new Label { Content = "10.490 ₽" });
            Label lenovo = new Label { Content = "$ 8.500" };
            lenovo.MouseDoubleClick += (s, e) => { lenovo.Content = "8.500 ₽"; corrects[5] = true; CheckBoolCorrects(); };
            incorrectListBoxPrices.Items.Add(lenovo);
            incorrectListBoxPrices.Items.Add(new Label { Content = "33.980 ₽" });
        }

        void CheckBoolCorrects() {
            if(corrects[0] && corrects[1] && corrects[2] && corrects[3] && corrects[4] && corrects[5])
                nextLvlButton.IsEnabled = true;
        }

        // Правильные
        void LVL5_FullInCorrectProducer() {
            listBoxProducer.Items.Add("Apple");
            listBoxProducer.Items.Add("Samsung");
            listBoxProducer.Items.Add("Xiaomi");
            listBoxProducer.Items.Add("HUAWEI");
            listBoxProducer.Items.Add("Lenovo");
            listBoxProducer.Items.Add("LG");
        }
        void LVL5_FullInCorrectModels() {
            listBoxModels.Items.Add("iPhone 11 64GB");
            listBoxModels.Items.Add("Galaxy A51 64GB");
            listBoxModels.Items.Add("Redmi 8A 2/32GB");
            listBoxModels.Items.Add("P smart Z 4/64GB");
            listBoxModels.Items.Add("A5 3/32GB");
            listBoxModels.Items.Add("V40 ThinQ 6/128GB");
        }
        void LVL5_FullInCorrectPrice() {
            listBoxPrices.Items.Add("54.290 ₽");
            listBoxPrices.Items.Add("15.490 ₽");
            listBoxPrices.Items.Add("7.140 ₽");
            listBoxPrices.Items.Add("10.490 ₽");
            listBoxPrices.Items.Add("8.500 ₽");
            listBoxPrices.Items.Add("33.980 ₽");
        }
        // Это всё можно запихать в файлы, но мне лень

        void LVL5_Load() {
            for (int i = 0; i < 4; gameGrid.ColumnDefinitions.Add(new ColumnDefinition()), i++) ;
            for (int i = 0; i < 4; gameGrid.RowDefinitions.Add(new RowDefinition()), i++) ;

            LVL5_CreateCorrectListBoxes();
            LVL5_CreateRadioBut();
            LVL5_CreateIncorrectListBoxes();

            nextLvlButton = new Button { Content = "Next", FontSize = 20, Margin = new Thickness(5), IsEnabled = false };
            nextLvlButton.Click += (s, e) => { ClearLvL(); LVL6_Load(); };
            Grid.SetColumn(nextLvlButton, 0);
            Grid.SetRow(nextLvlButton, 3);
            gameGrid.Children.Add(nextLvlButton);

            CreateLVLlabel();
            MessageBox.Show("Здесь тебе придётся исправлять ошибки) удачи))");
            CheckMessageValue();
        }

        // ---------------------------------------------------------------------------------------------------------------------------------------------------- //
        // ---------------------------------------------------------------------------------------------------------------------------------------------------- //
        // ---------------------------------------------------------------------------------------------------------------------------------------------------- //

        ContextMenu mainContext;
        Grid lvl6Grid;
        MenuItem[] firstItem, secondItem, thirdItem, fourthItem, fifthItem;

        MenuItem LVL6_AddContext(MenuItem[] menu, string itemHeader, string subItem1Header, string subItem2Header, string subItem3Header, string subItem4Header, string subItem5Header) {
            MenuItem mItem = new MenuItem { Header = itemHeader };

            menu[0] = new MenuItem { Header = subItem1Header };
            menu[0].Click += MenuItem_Click;
                mItem.Items.Add(menu[0]);
            menu[1] = new MenuItem { Header = subItem2Header };
            menu[1].Click += MenuItem_Click;
            mItem.Items.Add(menu[1]);
            menu[2] = new MenuItem { Header = subItem3Header };
            menu[2].Click += MenuItem_Click;
                mItem.Items.Add(menu[2]);
            menu[3] = new MenuItem { Header = subItem4Header };
            menu[3].Click += MenuItem_Click;
                mItem.Items.Add(menu[3]);
            menu[4] = new MenuItem { Header = subItem5Header };
            menu[4].Click += MenuItem_Click;
                mItem.Items.Add(menu[4]);

            return mItem;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e) {
            MessageBox.Show("Не туда)");
        }

        void LVL6_Load() {
            lvl6Grid = new Grid();
            mainContext = new ContextMenu();

            firstItem = new MenuItem[5];
            MenuItem firstMenuItem  = LVL6_AddContext(firstItem, "Пункт 1", "Подпункт 1", "Подпункт 2", "Подпункт 3", "Подпункт 4", "Подпункт 5");
            secondItem = new MenuItem[5];
            MenuItem secondMenuItem = LVL6_AddContext(secondItem, "Пункт 2", "Подпункт 1", "Подпункт 2", "Подпункт 3", "Подпункт 4", "Подпункт 5");
            thirdItem = new MenuItem[5];
            MenuItem thirdMenuItem  = LVL6_AddContext(thirdItem, "Пункт 3", "Подпункт 1", "Подпункт 2", "Подпункт 3", "Подпункт 4", "Подпункт 5");
            fourthItem = new MenuItem[5];
            MenuItem fourthMenuItem = LVL6_AddContext(fourthItem, "Пункт 4", "Подпункт 1", "Подпункт 2", "Подпункт 3", "Подпункт 4", "Подпункт 5");
            fifthItem = new MenuItem[5];
            MenuItem fifthMenuItem  = LVL6_AddContext(fifthItem, "Пункт 5", "Подпункт 1", "Подпункт 2", "Подпункт 3", "Подпункт 4", "Подпункт 5");

            mainContext.Items.Add(firstMenuItem);

            firstItem[4].Click  -= MenuItem_Click;
            firstItem[4].Click  += (s, e) => { mainContext.Items.Add(secondMenuItem); MessageBox.Show("Хорошее начало!"); };

            secondItem[1].Click -= MenuItem_Click;
            secondItem[1].Click += (s, e) => { mainContext.Items.Add(thirdMenuItem); MessageBox.Show("Ты на верном пути)"); };

            thirdItem[3].Click  -= MenuItem_Click;
            thirdItem[3].Click  += (s, e) => { mainContext.Items.Add(fourthMenuItem); MessageBox.Show("Не сдавайся!"); };

            fourthItem[3].Click -= MenuItem_Click;
            fourthItem[3].Click += (s, e) => { mainContext.Items.Add(fifthMenuItem); MessageBox.Show("Ты почти смог!"); };

            fifthItem[0].Click -= MenuItem_Click;
            fifthItem[0].Click += (s, e) => { ClearLvL(); LVL7_Load(); };

            lvl6Grid.Background = Brushes.White;
            lvl6Grid.ContextMenu = mainContext;
            gameGrid.Children.Add(lvl6Grid);

            CreateLVLlabel();
            MessageBox.Show("Ты всё таки смог исправить все ошибки, поздравляю)");
            CheckMessageValue();
        }

        // ---------------------------------------------------------------------------------------------------------------------------------------------------- //
        // ---------------------------------------------------------------------------------------------------------------------------------------------------- //
        // ---------------------------------------------------------------------------------------------------------------------------------------------------- //

        void LVL7_Load() {

            CreateLVLlabel();
            MessageBox.Show("Ну как?) понравилось бессмысленно тыкать?)");
            CheckMessageValue();
        }
    }
}
