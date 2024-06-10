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
using System.Windows.Shapes;

namespace Catching_eggs
{
    /// <summary>
    /// Логика взаимодействия для MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }
        public partial class App : Application
        {
            protected override void OnStartup(StartupEventArgs e)
            {
                base.OnStartup(e);
                MenuWindow menuWindow = new MenuWindow();
                menuWindow.Show();
            }
        }
        private void play_Click(object sender, RoutedEventArgs e)
        {

        }

        private void statistics_Click(object sender, RoutedEventArgs e)
        {

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
