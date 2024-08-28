using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Animation;
namespace ToDo_List
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "d0lmanyCore", "ToDo List - Lite");
        private bool allowedTray;
        private bool allowedFocus;
        private byte currentBP;
        private NotifyIcon icon;
        private System.Windows.Forms.ContextMenu menu;
        public MainWindow()
        {
            InitializeComponent();
            Check();
            Recover_Notes();
            Smooth(true, "");
        }
        private void CreateIcon()
        {
            if (allowedTray)
            {
                menu = new System.Windows.Forms.ContextMenu();
                menu.MenuItems.Add("Open", Open);
                menu.MenuItems.Add("Close", Close);
                menu.MenuItems.Add("Exit", Exit);
                icon = new NotifyIcon()
                {
                    Text = "ToDo List - Lite",
                    Icon = Properties.Resources.ico,
                    ContextMenu = menu,
                    Visible = true
                };
                Closer.ToolTip = "Close";
                icon.DoubleClick += (sender, e) => Open(sender, e);
            }
            else
            {
                icon = null;
                menu = null;
                Closer.ToolTip = "Close";
            }
        }
        private void Smooth(bool how, string c)
        {
            var a = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.2));
            var b = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.2));
            if (how) { this.BeginAnimation(OpacityProperty, a); }
            else
            {
                SaveAll();
                b.Completed += (e, s) =>
                {
                    if (c == "a")
                    { this.Hide(); }
                    else { System.Windows.Application.Current.Shutdown(); }
                };
                this.BeginAnimation(OpacityProperty, b);
            }
        }
        private void Open(object sender, EventArgs e) { Smooth(true, ""); this.Show(); }
        private void Close(object sender, EventArgs e) { Smooth(false, "a"); }
        private void Exit(object sender, EventArgs e)
        {
            Smooth(false, "");
        }
        private void Exit(object sender, MouseButtonEventArgs e)
        {
            if (allowedTray)
            {
                Smooth(false, "a");
            }
            else
            {
                Smooth(false, "");
            }
        }

        private void DragPls(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) this.DragMove();
        }

        private void Add_Note(object sender, RoutedEventArgs e)
        {
            var Note = new Note();
            Note.RemoveRequested += Note_RemoveRequested;
            if (allowedFocus)
            {
                Note.SNLastFocus += Note_SaveNotes;
            }
            Notes.Items.Insert(0, Note);
        }

        private async void Note_SaveNotes(object sender, EventArgs e)
        {
            var exp = (sender as Note).GetTodos();
            using (StreamWriter sw = new StreamWriter(path + $"//{exp.Id}.json"))
            {
                string that = JsonConvert.SerializeObject(exp);
                await sw.WriteLineAsync(that);
            }
        }
        private void SaveAll()
        {
            for (int i = 0; i < Notes.Items.Count; i++)
            {
                var get = Notes.Items[i] as Note;
                get.ClearNote();
                var exp = get.GetTodos();
                using (StreamWriter sw = new StreamWriter(path + $"//{exp.Id}.json"))
                {
                    string that = JsonConvert.SerializeObject(exp);
                    sw.WriteLine(that);
                }
            }
        }
        private void Recover_Notes()
        {
            string[] notes = Directory.GetFiles(path, "*.json");
            List<ToDoList> imp = new List<ToDoList>();
            foreach (var pathes in notes)
            {
                string text = File.ReadAllText(pathes);
                ToDoList toDoList = JsonConvert.DeserializeObject<ToDoList>(text);
                imp.Add(toDoList);
            }
            foreach (var note in imp)
            {
                var that = new Note();
                that.SetTodos(note.Header, note.Notes, note.Id);
                that.RemoveRequested += Note_RemoveRequested;
                if (allowedFocus)
                {
                    that.SNLastFocus += Note_SaveNotes;
                }
                Notes.Items.Insert(0, that);
            }
        }

        private void Note_RemoveRequested(object sender, EventArgs e)
        {
            var del = (sender as Note).GetTodos();
            File.Delete(path + $"//{del.Id}.json");
            var a = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.2));
            a.Completed += (s, args) => { Notes.Items.Remove(sender); };
            (sender as Note).BeginAnimation(OpacityProperty, a);
        }

        private void Settings(object sender, RoutedEventArgs e)
        {
            var Settings = new Settings
            {
                Owner = this
            };
            Settings.Check += Settings_Check;
            Settings.Delete += Clear;
            Settings.ShowDialog();
        }

        private void Clear(object sender, EventArgs e)
        {
            Notes.Items.Clear();
            string[] notes = Directory.GetFiles(path, "*.json");
            foreach (var note in notes)
            {
                File.Delete(note);
            }
        }

        private void Settings_Check(object sender, EventArgs e)
        {
            Check();
        }
        private void Check()
        {
            string str = path + "//.config";
            if (File.Exists(str))
            {
                using (StreamReader sr = new StreamReader(path + "//.config"))
                {
                    string that = sr.ReadToEnd();
                    Sets sets = JsonConvert.DeserializeObject<Sets>(that);
                    allowedTray = sets.Tray;
                    allowedFocus = sets.Focus;
                    currentBP = sets.ButtonPlacement;
                    this.Height = sets.Height;
                    this.Topmost = sets.Topmost;
                    CreateIcon();
                }
            }
            else
            {
                using (StreamWriter sw = new StreamWriter(str))
                {
                    Sets sets = new Sets
                    {
                        Focus = false,
                        Tray = true,
                        ButtonPlacement = 0,
                        Height = 600,
                        Topmost = false
                    };
                    sw.WriteLine(JsonConvert.SerializeObject(sets));
                    allowedTray = sets.Tray;
                    allowedFocus = sets.Focus;
                    currentBP = sets.ButtonPlacement;
                    this.Height = sets.Height;
                    this.Topmost = sets.Topmost;
                    CreateIcon();
                }
            }
            if (currentBP == 0)
            {
                Grid.SetColumn(flex1, 1);
                flex1.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
                Grid.SetColumn(flex2, 2);
                flex2.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
            }
            else
            {
                Grid.SetColumn(flex1, 0);
                flex1.HorizontalAlignment = System.Windows.HorizontalAlignment.Left;
                Grid.SetColumn(flex2, 3);
                flex2.HorizontalAlignment = System.Windows.HorizontalAlignment.Right;
            }
        }

        private void Roll(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
    public struct Notes
    {
        public string Note { get; set; }
        public bool IsDone { get; set; }
    }
    public struct ToDoList
    {
        public List<Notes> Notes { get; set; }
        public string Header { get; set; }
        public string Id { get; set; }
    }
}