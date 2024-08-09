using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
namespace Snake
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int score = 0, deltaX = 5, deltaY = 0;
        private string side = "up";
        private readonly DispatcherTimer timer;
        private readonly List<System.Windows.Shapes.Rectangle> snakeSegments = new List<System.Windows.Shapes.Rectangle>();
        private bool justAte = false;
        private Bitmap tailBitmap = Properties.Resources.tailV, headBitmap = Properties.Resources.headU;
        private BitmapSource tailImage, headImage;
        public MainWindow()
        {
            InitializeComponent();
            snakeSegments.Add(SnakeHead);
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(150)
            };
            timer.Tick += GameTime;
            timer.Start();
            DrawFood();
        }

        private void GameTime(object sender, EventArgs e)
        {
            MoveSnake();
            EatIt();
            HitWalls();
        }
        private void MoveSnake()
        {
            switch (side)
            {
                case "up":
                    Canvas.SetTop(SnakeHead, Canvas.GetTop(SnakeHead) - 10);
                    deltaY = 5; deltaX = 0;
                    tailBitmap = Properties.Resources.tailV;
                    headBitmap = Properties.Resources.headU;
                    break;
                case "down":
                    Canvas.SetTop(SnakeHead, Canvas.GetTop(SnakeHead) + 10);
                    deltaY = -5; deltaX = 0;
                    tailBitmap = Properties.Resources.tailV;
                    headBitmap = Properties.Resources.headD;
                    break;
                case "left":
                    Canvas.SetLeft(SnakeHead, Canvas.GetLeft(SnakeHead) - 10);
                    deltaY = 0; deltaX = 5;
                    tailBitmap = Properties.Resources.tailH;
                    headBitmap = Properties.Resources.headL;
                    break;
                case "right":
                    Canvas.SetLeft(SnakeHead, Canvas.GetLeft(SnakeHead) + 10);
                    deltaY = 0; deltaX = -5;
                    tailBitmap = Properties.Resources.tailH;
                    headBitmap = Properties.Resources.headR;
                    break;
                default:
                    throw new Exception("Ошибка определения стороны змеи");
            }
            foreach (System.Windows.Shapes.Rectangle segs in snakeSegments) { segs.Fill = new ImageBrush(tailImage); }
            CheckCollision();
            if (justAte)
            {
                justAte = false;
            }
            UpdateSnake();
        }
        private void UpdateSnake()
        {
            for (int i = snakeSegments.Count - 1; i > 0; i--)
            {
                Canvas.SetLeft(snakeSegments[i], Canvas.GetLeft(snakeSegments[i - 1]) + deltaX);
                Canvas.SetTop(snakeSegments[i], Canvas.GetTop(snakeSegments[i - 1]) + deltaY);
            }
            tailImage = Imaging.CreateBitmapSourceFromHBitmap(
                tailBitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromWidthAndHeight(tailBitmap.Width, tailBitmap.Height)
            );

            headImage = Imaging.CreateBitmapSourceFromHBitmap(
                headBitmap.GetHbitmap(),
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromWidthAndHeight(headBitmap.Width, headBitmap.Height)
            );
            SnakeHead.Fill = new ImageBrush(headImage);
        }

        private void HitWalls()
        {
            double x = Canvas.GetLeft(SnakeHead), y = Canvas.GetTop(SnakeHead);
            if (x == -10 || x == Place.Width - 10)
            {
                GameOver();
            }
            if (y == -10 || y == Place.Height - 5)
            {
                GameOver();
            }
        }
        private void CheckCollision()
        {
            for (int i = 1; i < snakeSegments.Count; i++)
            {
                double segmentLeft = Canvas.GetLeft(snakeSegments[i]);
                double segmentTop = Canvas.GetTop(snakeSegments[i]);
                if (Math.Abs(Canvas.GetLeft(SnakeHead) - segmentLeft) < 10 &&
                    Math.Abs(Canvas.GetTop(SnakeHead) - segmentTop) < 10)
                {
                    GameOver();
                }
            }
        }

        private void EatIt()
        {
            if (Canvas.GetLeft(Apple) == Canvas.GetLeft(SnakeHead) && Canvas.GetTop(Apple) == Canvas.GetTop(SnakeHead))
            {
                DrawFood();
                score++;
                ScoreOut.Content = score;
                using (SoundPlayer sound = new SoundPlayer())
                {
                    sound.Stream = Properties.Resources.eat;
                    sound.Play();
                }
                LongHim();
                justAte = true;
                VerifySpeed();
                if (score == 7440)
                {
                    GameOverOut.Content = "you win!!!\ncongratulate you";
                    GameOverOut.Foreground = System.Windows.Media.Brushes.Red;
                    GameOverOut.Visibility = Visibility.Visible;
                    SoundPlayer p = new SoundPlayer
                    {
                        Stream = Properties.Resources.fail
                    };
                    p.Play();
                    p.Dispose();
                    timer.Stop();
                }
            }

        }
        private void LongHim()
        {
            System.Windows.Shapes.Rectangle newSegment = new System.Windows.Shapes.Rectangle
            {
                Width = 15,
                Height = 15,
                Fill = new ImageBrush(tailImage)
            };
            System.Windows.Shapes.Rectangle lastSegment = snakeSegments[snakeSegments.Count - 1];
            Canvas.SetLeft(newSegment, Canvas.GetLeft(lastSegment) + deltaX);
            Canvas.SetTop(newSegment, Canvas.GetTop(lastSegment) + deltaY);
            _ = Place.Children.Add(newSegment);
            snakeSegments.Add(newSegment);
        }
        private void Exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void GameOver()
        {
            timer.Stop();
            GameOverOut.Visibility = Visibility.Visible;
            using (SoundPlayer sound = new SoundPlayer())
            {
                sound.Stream = Properties.Resources.fail;
                sound.Play();
            }
        }
        private void VerifySpeed()
        {
            if (score % 50 == 0)
            {
                if (timer.Interval != TimeSpan.FromMilliseconds(50))
                {
                    timer.Interval -= TimeSpan.FromMilliseconds(5);
                }
            }
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            _ = Process.Start("https://d0lmany.netlify.app/");
        }

        private void Restart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GameOverOut.Visibility = Visibility.Hidden;
            GameOverOut.Content = "game over";
            Canvas.SetLeft(SnakeHead, 600);
            Canvas.SetTop(SnakeHead, 300);
            score = 0;
            deltaX = 5;
            deltaY = 0;
            side = "up";
            foreach (System.Windows.Shapes.Rectangle segment in snakeSegments)
            {
                Place.Children.Remove(segment);
            }
            snakeSegments.Clear();
            snakeSegments.Add(SnakeHead);
            _ = Place.Children.Add(SnakeHead);
            justAte = false;
            timer.Interval = TimeSpan.FromMilliseconds(150);
            timer.Start();
            DrawFood();
        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Left:
                    side = "left";
                    break;
                case Key.Right:
                    side = "right";
                    break;
                case Key.Up:
                    side = "up";
                    break;
                case Key.Down:
                    side = "down";
                    break;
                default:
                    ;
                    break;
            }
        }
        private void DrawFood()
        {
            Random r = new Random();
            int x, y;
            do
            {
                x = r.Next(10, 1190);
                y = r.Next(10, 300);
            } while (x % 10 != 0 || y % 10 != 0);
            Canvas.SetTop(Apple, y);
            Canvas.SetLeft(Apple, x);
        }
    }
}
