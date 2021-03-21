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

namespace Barley_Break
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int steps;

        public MainWindow()
        {
            InitializeComponent();

            for(int i = 3; i <= 10; i ++)
            {
                SIZE.Items.Add($"Размер поля {i}x{i}");
            }

            SIZE.SelectedIndex = 0;

            Game.Step += () =>
            {
                StepCount.Content = $"Ходов {++steps}";
            };

            MIX.Click += (s, a) =>
            {
                var size = SIZE.SelectedIndex + 3;
                Game.Start(Arena, size, size);
            };
        }
    }

    
}
