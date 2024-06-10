using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.IO;




namespace Catching_eggs
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow1 : Window
    {
        private MediaPlayer player = new MediaPlayer();
        DispatcherTimer gameTimer = new DispatcherTimer();
        bool moveLeft, moveRight;

        List<Ellipse> itemRemover = new List<Ellipse>();

        Random rand = new Random();

        int eggCounter = 100;
        int basketSpeed = 20;
        int limit = 50;
        int attemptCounter = 3;
        int yscore = 0;
        int eggSpeed = 10;


        Rect basketHitBox;

        public MainWindow1()
        {
            InitializeComponent();


            gameTimer.Interval = TimeSpan.FromMilliseconds(20);
            gameTimer.Tick += GameLogic;
            gameTimer.Start();

            field.Focus();

            ImageBrush background = new ImageBrush();
            background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/pikselnyi-peizazh-16.png"));
            field.Background = background;

            ImageBrush basketImage = new ImageBrush();
            basketImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/basket.png"));
            basket.Fill = basketImage;

            Music();
        }

        private void GameLogic(object sender, EventArgs e)
        {

            basketHitBox = new Rect(Canvas.GetLeft(basket), Canvas.GetTop(basket), basket.Width, basket.Height);

            eggCounter -= 1;

            score.Content = "Счет :  " + yscore;
            attempt.Content = "Попытки : " + attemptCounter;

            if (eggCounter < 0)
            {
                Eggs();
                eggCounter = limit;
            }

            if (moveLeft && Canvas.GetLeft(basket) > 0)
            {
                Canvas.SetLeft(basket, Canvas.GetLeft(basket) - basketSpeed);
            }
            if (moveRight && Canvas.GetLeft(basket) + basket.Width < System.Windows.Application.Current.MainWindow.Width)
            {
                Canvas.SetLeft(basket, Canvas.GetLeft(basket) + basketSpeed);
            }
            foreach (var item in field.Children.OfType<Ellipse>())
            {
                if ((string)item.Tag == "egg")
                {
                    Canvas.SetTop(item, Canvas.GetTop(item) + eggSpeed);

                    Rect eggHitBox = new Rect(Canvas.GetLeft(item), Canvas.GetTop(item), item.Width, item.Height);


                    if (eggHitBox.IntersectsWith(basketHitBox))
                    {
                        yscore += 1;
                        itemRemover.Add(item);
                    }


                    if (Canvas.GetTop(item) > field.ActualHeight)
                    {
                        attemptCounter -= 1;
                        itemRemover.Add(item);
                    }
                    if (attemptCounter < 0)
                    {
                        gameTimer.Stop();
                        score.Visibility = Visibility.Collapsed;
                        attempt.Visibility = Visibility.Collapsed;
                        basket.Visibility = Visibility.Collapsed;
                        FinalMenu();
                    }
                }
            }


            foreach (var item in itemRemover)
            {
                field.Children.Remove(item);
            }
            itemRemover.Clear();
        }

        private void field_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moveLeft = true;
            }
            if (e.Key == Key.Right)
            {
                moveRight = true;
            }
        }

        private void field_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moveLeft = false;
            }
            if (e.Key == Key.Right)
            {
                moveRight = false;
            }
        }
        private void Eggs()
        {
            Ellipse newEgg = new Ellipse
            {
                Tag = "egg",
                Height = 15,
                Width = 10,
                Fill = Brushes.White,
            };

            Canvas.SetTop(newEgg, -100);
            Canvas.SetLeft(newEgg, rand.Next(30, 670));
            field.Children.Add(newEgg);


            DispatcherTimer eggTimer = new DispatcherTimer();
            eggTimer.Interval = TimeSpan.FromSeconds(10);
            eggTimer.Tick += (sender, e) =>
            {
                if (attemptCounter > 0)
                {
                    Eggs();
                }
                else
                {
                    eggTimer.Stop();
                }
            };
            eggTimer.Start();
        }

        private void FinalMenu()
        {


            Canvas canvas = new Canvas();

            ImageBrush background = new ImageBrush();
            background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/volk-nu-pogodi-2 копия.jpg"));
            canvas.Background = background;

            Button resume_button = new Button();
            resume_button.FontSize = 20;
            resume_button.Content = "Продолжить";
            resume_button.Click += ResumeButton_Click;
            Canvas.SetLeft(resume_button, 492);
            Canvas.SetTop(resume_button, 310);
            canvas.Children.Add(resume_button);
            this.Content = canvas;

            void ResumeButton_Click(object sender, RoutedEventArgs e)
            {
                System.Diagnostics.Process.Start(System.Windows.Application.ResourceAssembly.Location);
                System.Windows.Application.Current.Shutdown();
            }

            Button exit_button = new Button();
            exit_button.FontSize = 20;
            exit_button.Content = "Выйти";
            exit_button.Click += ExitButton_Click;
            Canvas.SetLeft(exit_button, 400);
            Canvas.SetTop(exit_button, 310);
            canvas.Children.Add(exit_button);
            this.Content = canvas;

            void ExitButton_Click(object sender, RoutedEventArgs e)
            {
                System.Windows.Application.Current.Shutdown();
            }
        }
        private void Music()
        {
            player = new MediaPlayer();
            player.Open(new Uri(System.IO.Path.Combine(Directory.GetCurrentDirectory(), "Music/Ария - Беспечный ангел [8 bit].mp3")));
            player.MediaEnded += MusicEnded;
            player.Play();

            void MusicEnded(object sender, EventArgs e)
            {
                player.Position = TimeSpan.Zero;
                player.Play();
            }
        }
        
    }
}
