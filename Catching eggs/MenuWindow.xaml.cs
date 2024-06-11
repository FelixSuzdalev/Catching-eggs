using System;
using System.Collections.Generic;
using System.IO;
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
        public static DateTime time = DateTime.Now;
        public static string playerName1;
        public static string folderPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Statistics");
        public static string filePath = System.IO.Path.Combine(folderPath, "Statistics.txt");
        public MenuWindow()
        {
            InitializeComponent();


        }
        private void CreateFileStatistics()
        {
            try
            {
                Directory.CreateDirectory(folderPath);
            }
            catch { }
            FileStream file = new FileStream(filePath, FileMode.Append, FileAccess.Write);
            using (StreamWriter writer = new StreamWriter(file))
            {
                writer.WriteLine(time);
                writer.WriteLine("Player: " + playerName1);
                writer.Close();
            }

        }

        private void play_Click(object sender, RoutedEventArgs e)
        {
            if (inputNamePlayer.Text != "")
            {
                playerName1 = inputNamePlayer.Text;
                CreateFileStatistics();
                Close();
            }
            else MessageBox.Show("Поле игрок пустое", "Error");

        }

        private void statistics_Click(object sender, RoutedEventArgs e)
        {

        }

        private void exit_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}