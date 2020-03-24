using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace BreakBrainGame
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int time = 10;
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
        /// Проверка количества кликов и тайсера.
        /// </summary>
        void LVL1_CheckCountClick()
        {
            if (clickCount >= 10) { ClearLvL(); numberLVL++; }
            else if (time <= -2) { clickCount = 0; time = 10; }
        }

        /// <summary>
        /// Создаёт кнопку для 1 уровня.
        /// </summary>
        /// <returns></returns>
        Button LVL1_CreateButton() {
            Button but = new Button { Width = 200, Height = 200, FontSize = 25, Content = "ЖМяк", };
            return but;
        }

        /// <summary>
        /// Создаёт Лейбл для 1 уровня.
        /// </summary>
        /// <returns></returns>
        Label LVL1_CreateLabel() {
            Label lbl = new Label { VerticalAlignment = VerticalAlignment.Top, Width = 200, Height = 200, FontSize = 20, };
            return lbl;
        }

        /// <summary>
        /// Клики на кнопку.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LVL1_But_Click(object sender, RoutedEventArgs e) {
            clickCount++;           // Прибавлять количество кликов.
            if (clickCount == 1)    // Запуск таймера.
                LVL1_Timer();
            LVL1_CheckCountClick();      // Проверка на количество кликов.

        }

        /// <summary>
        /// Запуск таймера.
        /// </summary>
        void LVL1_Timer() {
            lbl.Content = time;
            timer.Interval = new TimeSpan(10000000);
            timer.Start();
        }

        /// <summary>
        /// Действия на каждый тик таймера.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void LVL1_Timer_Tick(object sender, EventArgs e)
        {
            time--;
            lbl.Content = time;
            if (clickCount >= 10 && time > 0) { timer.Stop(); LVL1_CheckCountClick(); }
        }
        
        /// <summary>
        /// Загружает первый уровень на форму.
        /// </summary>
        void LVL1_Load() {
            but = LVL1_CreateButton();          // Кнопка для жмяков
            but.Click += LVL1_But_Click;
            gameGrid.Children.Add(but);

            lbl = LVL1_CreateLabel();           // Лейбл для вывода таймера
            gameGrid.Children.Add(lbl);

            timer = new DispatcherTimer();      // Таймер
            timer.Tick += LVL1_Timer_Tick;
            CreateLVLlabel();                   // Отображение номера уровня
        }




        /// <summary>
        /// Загружает второй уровень на форму.
        /// </summary>
        void LVL2_Load()
        {

        }




        /// <summary>
        /// Загружает третий уровень на форму.
        /// </summary>
        void LVL3_Load()
        {

        }




        /// <summary>
        /// Полностью очищает форму.
        /// </summary>
        void ClearLvL() {
            int lenghtGridChildren = mainGrid.Children.Count - 1;   // Количество элементов в Гриде.
            for (int i = lenghtGridChildren; i >= 0; i--)
                mainGrid.Children.RemoveAt(i);
        }

        /// <summary>
        /// Вывод номера уровня.
        /// </summary>
        void CreateLVLlabel() {
            numberLVLlabel = new Label { Content = $"Уровень {numberLVL}", FontSize = 25, };
            numberLVLGrid.Children.Add(numberLVLlabel);
        }
    }
}
