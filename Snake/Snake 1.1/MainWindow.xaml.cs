using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Media;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Snake_1._1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {   ///ЛОГИКА
        private ushort score = 0;
        private sbyte deltaX = 5, deltaY = 0;
        private string side = "up";
        private readonly DispatcherTimer timer;
        private readonly List<Rectangle> snakeSegments = new List<Rectangle>();
        private bool justAte = false;
        private readonly Random r = new Random();
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
        private void DrawFood()
        {
            ushort x, y;
            do
            {
                x = (ushort)r.Next(10, 1170);
                y = (ushort)r.Next(10, 560);
            } while (x % 10 != 0 || y % 10 != 0);
            Canvas.SetTop(Apple, y);
            Canvas.SetLeft(Apple, x);
        }
        private void GameTime(object sender, EventArgs e)
        {
            MoveSnake();
            EatIt();
            HitWalls();
        }
        private void HitWalls()
        {
            double x = Canvas.GetLeft(SnakeHead), y = Canvas.GetTop(SnakeHead);
            if (x == 0 || x == GameBoard.Width - 10)
            {
                GameOver();
            }
            if (y == 0 || y == GameBoard.Height - 15)
            {
                GameOver();
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
                if (score == 6726)
                {
                    WinGame();
                }
            }
        }
        private void LongHim()
        {
            Rectangle newSegment = new Rectangle
            {
                Width = 15,
                Height = 15,
                Fill = new RadialGradientBrush(Color.FromRgb(4, 164, 36), Color.FromRgb(4, 180, 48))
            };
            Rectangle lastSegment = snakeSegments[snakeSegments.Count - 1];
            Canvas.SetLeft(newSegment, Canvas.GetLeft(lastSegment) + deltaX);
            Canvas.SetTop(newSegment, Canvas.GetTop(lastSegment) + deltaY);
            _ = GameBoard.Children.Add(newSegment);
            snakeSegments.Add(newSegment);
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
        private void MoveSnake()
        {
            switch (side)
            {
                case "up":
                    Canvas.SetTop(SnakeHead, Canvas.GetTop(SnakeHead) - 10);
                    deltaY = 5; deltaX = 0;
                    SnakeHead.RenderTransform = new RotateTransform(180);
                    break;
                case "down":
                    Canvas.SetTop(SnakeHead, Canvas.GetTop(SnakeHead) + 10);
                    deltaY = -5; deltaX = 0;
                    SnakeHead.RenderTransform = null;
                    break;
                case "left":
                    Canvas.SetLeft(SnakeHead, Canvas.GetLeft(SnakeHead) - 10);
                    deltaY = 0; deltaX = 5;
                    SnakeHead.RenderTransform = null;
                    break;
                case "right":
                    Canvas.SetLeft(SnakeHead, Canvas.GetLeft(SnakeHead) + 10);
                    deltaY = 0; deltaX = -5;
                    SnakeHead.RenderTransform = null;
                    break;
                default:
                    throw new Exception("Ошибка определения стороны змеи");
            }
            CheckCollision();
            if (justAte)
            {
                justAte = false;
            }
            UpdateSnake();
        }
        private void UpdateSnake()
        {
            for (ushort i = (ushort)(snakeSegments.Count - 1); i > 0; i--)
            {
                Canvas.SetLeft(snakeSegments[i], Canvas.GetLeft(snakeSegments[i - 1]) + deltaX);
                Canvas.SetTop(snakeSegments[i], Canvas.GetTop(snakeSegments[i - 1]) + deltaY);
            }
        }
        private void CheckCollision()
        {
            for (ushort i = 1; i < snakeSegments.Count; i++)
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
        private void GameOver()
        {
            timer.Stop();
            GameMessage.Content = "GAMEOVER";
            GameMessage.Visibility = Visibility.Visible;
            using (SoundPlayer s = new SoundPlayer())
            {
                s.Stream = Properties.Resources.fail;
                s.Play();
            }
        }
        private void WinGame()
        {
            timer.Stop();
            GameMessage.Content = "YOU WIN!!!\nGOOD GAME";
            GameMessage.Visibility = Visibility.Visible;
            using (SoundPlayer s = new SoundPlayer())
            {
                s.Stream = Properties.Resources.fail;
                s.Play();
            }
            timer.Stop();
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
                    break;
            }
        }
        private void RestartGame()
        {
            Canvas.SetLeft(SnakeHead, 590);
            Canvas.SetTop(SnakeHead, 290);
            score = 0; deltaX = 5; deltaY = 0;
            ScoreOut.Content = "0";
            side = "up";
            foreach (Rectangle segment in snakeSegments)
            {
                GameBoard.Children.Remove(segment);
            }
            GameBoard.Children.Add(SnakeHead);
            snakeSegments.Clear();
            snakeSegments.Add(SnakeHead);
            justAte = false;
            timer.Interval = TimeSpan.FromMilliseconds(150);
            timer.Start();
            DrawFood();
        }
        ///ОБОЛОЧКА
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }
        private void Restart_MouseDown(object sender, MouseButtonEventArgs e)
        {
            GameMessage.Visibility = Visibility.Collapsed;
            RestartGame();
        }
        private void Exit_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult so = MessageBox.Show("A page opens in the browser, go to?", "Snake", MessageBoxButton.YesNo);
            timer.Stop();
            if (so == MessageBoxResult.Yes)
            {
                _ = Process.Start("https://d0lmany.netlify.app/");
            }
            else
            {
                timer.Start();
            }
        }
    }
}