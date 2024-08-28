using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace ToDo_List
{
    /// <summary>
    /// Логика взаимодействия для Note.xaml
    /// </summary>
    public partial class Note : UserControl
    {
        internal event EventHandler RemoveRequested;
        internal event EventHandler SNLastFocus;
        internal string Id;
        public Note()
        {
            InitializeComponent();
            Id = Guid.NewGuid().ToString();
            Animate();
        }
        internal ToDoList GetTodos()
        {
            List<Notes> todos = new List<Notes>();
            foreach (Todo child in TodoList.Children)
            {
                todos.Add(child.GetTodo());
            }
            ToDoList ret = new ToDoList { Header = Header.Text, Notes = todos, Id = this.Id };
            return ret;
        }
        internal void SetTodos(string header, List<Notes> todos, string Id)
        {
            Header.Text = header;
            this.Id = Id;
            foreach (var item in todos)
            {
                var todo = new Todo();
                todo.EnterPressed += Todo_EnterPressed;
                todo.SetTodo(item.IsDone, item.Note);
                TodoList.Children.Add(todo);
            }
        }
        private void AddTodo()
        {
            var todo = new Todo();
            todo.EnterPressed += Todo_EnterPressed;
            TodoList.Children.Add(todo);
        }

        private void Todo_EnterPressed(object sender, EventArgs e)
        {
            AddTodo();
        }

        private void Header_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Header.Text.Length > 25)
            {
                Header.Text = Header.Text.Substring(0, 25);
            }
        }

        private void Add_Todo(object sender, RoutedEventArgs e)
        {
            AddTodo();
        }

        private void Del_Note(object sender, RoutedEventArgs e)
        {
            RemoveRequested?.Invoke(this, EventArgs.Empty);
        }
        internal void ClearNote()
        {
            List<Todo> itemsToRemove = new List<Todo>();
            foreach (Todo item in TodoList.Children)
            {
                var that = item.GetTodo();
                if (that.Note == "" || that.Note == " ")
                {
                    itemsToRemove.Add(item);
                }
            }
            foreach (Todo item in itemsToRemove)
            {
                TodoList.Children.Remove(item);
            }
        }

        private void UserControl_LostFocus(object sender, RoutedEventArgs e)
        {
            SNLastFocus?.Invoke(this, EventArgs.Empty);
        }
        private void Animate()
        {
            var a = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.5));
            this.BeginAnimation(OpacityProperty, a);

        }
    }
}
