using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ToDo_List
{
    /// <summary>
    /// Логика взаимодействия для Todo.xaml
    /// </summary>
    public partial class Todo : UserControl
    {
        internal bool isPressed = false;
        internal event EventHandler EnterPressed;
        public Todo()
        {
            InitializeComponent();
        }
        internal Notes GetTodo()
        {
            var it = new Notes
            {
                Note = TodVal.Text,
                IsDone = isPressed
            };
            return it;
        }
        internal void SetTodo(bool press, string text)
        {
            isPressed = press;
            TodVal.Text = text;
            CreateDone();
        }
        private void Done(object sender, MouseButtonEventArgs e)
        {
            isPressed = !isPressed;
            CreateDone();
        }
        private void CreateDone()
        {
            if (isPressed)
            {
                TodVal.Foreground = new SolidColorBrush(Color.FromRgb(76, 76, 76));
                TodVal.TextDecorations = TextDecorations.Strikethrough;
                DoneCheck.Opacity = 100;
                TodVal.IsReadOnly = true;
            }
            else
            {
                TodVal.Foreground = Brushes.White;
                TodVal.TextDecorations = null;
                DoneCheck.Opacity = 0;
                TodVal.IsReadOnly = false;
            }
        }
        private void TodVal_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!isPressed)
            {
                TodVal.TextDecorations = TextDecorations.Underline;
            }

        }

        private void TodVal_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!isPressed)
            {
                TodVal.TextDecorations = null;
            }
        }

        private void TodVal_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (TodVal.Text.Length > 100)
            {
                theBorder.VerticalAlignment = VerticalAlignment.Top;
                theBorder.Margin = new Thickness(10);
            }
            if (TodVal.Text.Length > 200)
            {
                TodVal.Text = TodVal.Text.Substring(0, 200);
            }
            if (TodVal.Text.Length < 50)
            {
                theBorder.VerticalAlignment = VerticalAlignment.Center;
                theBorder.Margin = new Thickness(0);
            }
        }

        private void TodVal_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EnterPressed?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
