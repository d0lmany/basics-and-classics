using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows;

namespace ToDo_List
{
    /// <summary>
    /// Логика взаимодействия для Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        internal event EventHandler Check;
        internal event EventHandler Delete;
        private readonly string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "d0lmanyCore", "ToDo List - Lite", ".config");
        public Settings()
        {
            InitializeComponent();
            CheckSet();
        }
        private void CheckSet()
        {
            using (StreamReader sr = new StreamReader(path))
            {
                string that = sr.ReadToEnd();
                Sets sets = JsonConvert.DeserializeObject<Sets>(that);
                tray.IsChecked = sets.Tray;
                focus.IsChecked = sets.Focus;
                if (sets.ButtonPlacement == 0)
                { R1.IsChecked = true; }
                else { R2.IsChecked = true; }
                HeightInput.Text = sets.Height.ToString();
                top.IsChecked = sets.Topmost;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            byte place;
            if (R1.IsChecked == true)
                place = 0;
            else place = 1;
            using (StreamWriter sw = new StreamWriter(path, false))
            {
                Sets sets = new Sets
                {
                    Focus = focus.IsChecked.Value,
                    Tray = tray.IsChecked.Value,
                    ButtonPlacement = place,
                    Height = ushort.Parse(HeightInput.Text),
                    Topmost = top.IsChecked.Value,
                };
                sw.WriteLine(JsonConvert.SerializeObject(sets));
            };
            Check?.Invoke(this, new EventArgs());
        }

        private void Fold_Click(object sender, RoutedEventArgs e)
        {
            Process.Start(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "d0lmanyCore", "ToDo List - Lite"));
        }

        private void Del_Click(object sender, RoutedEventArgs e)
        {
            Delete?.Invoke(this, new EventArgs());
        }

        private void Res_Click(object sender, RoutedEventArgs e)
        {
            File.Delete(path);
        }

        private void HeightInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if ((ushort.Parse(HeightInput.Text)) < 100)
            {
                MessageBox.Show("Incorrect value was entered");
                HeightInput.Text = "600";
            }
        }
    }
    internal struct Sets
    {
        public bool Focus { get; set; }
        public bool Tray { get; set; }
        public byte ButtonPlacement { get; set; }
        public ushort Height { get; set; }
        public bool Topmost { get; set; }
    }
}
