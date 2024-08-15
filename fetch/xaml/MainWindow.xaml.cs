using System;
using System.Collections.Generic;
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
using System.Net.Http;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Drawing;
using System.Net.NetworkInformation;

namespace xaml
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //ссылки на источники
        const string urlUser = "https://jsonplaceholder.typicode.com/users";
        const string urlPost = "https://jsonplaceholder.typicode.com/posts";
        const string urlComment = "https://jsonplaceholder.typicode.com/comments";
        const string urlPhoto = "https://jsonplaceholder.typicode.com/photos";
        static string[] album;
        static int currentphoto = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void start_Click(object sender, EventArgs e)
        {
            if (VerifyNet())
            {
                //извлекаем данные
                await FetchData(urlUser, outUser, "User");
                await FetchData(urlPost, outPost, "Post");
                await FetchData(urlComment, outComment, "Comment");
                await FetchData(urlPhoto, outPhotoList, "Photo");
                CreateAlbum(outPhoto, outPhotoList);
            }
            else { MessageBox.Show("Подключение к сети отсутствует"); }
        }
        //метод для проверки подключения
        static bool VerifyNet()
        {
            try
            {
                Ping ping = new Ping();
                PingReply reply = ping.Send("yandex.ru");
                return reply.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }
        static async Task FetchData(string url, ListBox listBox, string current)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    //собственно, fetch
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();
                    string responseBody = await response.Content.ReadAsStringAsync();
                    //сохраняем jsonы
                    List<User> jsons = JsonConvert.DeserializeObject<List<User>>(responseBody);
                    //получаем содержимое
                    string[] outs = { };
                    switch (current)
                    {
                        case "User":
                            outs = GetSaveStrings(jsons);
                            break;
                        case "Post":
                            outs = GetSaveStringsOne(jsons);
                            break;
                        case "Comment":
                            outs = GetSaveStringsOne(jsons);
                            break;
                        case "Photo":
                            outs = GetSaveStringsTwo(jsons);
                            break;
                    }
                    //выводим в окно
                    //listBox.Items.Add
                    foreach(var item in outs)
                    {
                        listBox.Items.Add(item);
                    }
                }
                catch
                {
                    MessageBox.Show("отрежь себе руки");
                }
            }
        }

        //методы для преобразования
        static public string[] GetSaveStrings(List<User> give)
        {
            List<string> get = new List<string>();
            foreach (var str in give)
            {
                get.Add(str.Name);
            }
            return get.ToArray();
        }
        static public string[] GetSaveStringsOne(List<User> give)
        {
            List<string> get = new List<string>();
            foreach (var str in give)
            {
                get.Add(str.Body);
            }
            return get.ToArray();
        }
        static public string[] GetSaveStringsTwo(List<User> give)
        {
            List<string> get = new List<string>();
            foreach (var str in give)
            {
                get.Add(str.Url);
            }
            return get.ToArray();
        }
        //МЕТОДЫ ДЛЯ КАРТИНОЧЕК
        //создание альбома
        static void CreateAlbum(Image box, ListBox from)
        {
            //получаем список ссылкок на фото
            if (from.Items.Count > 0)
            {
                List<string> temp = new List<string>();
                foreach (var item in from.Items)
                {
                    temp.Add(item.ToString());
                }
                album = temp.ToArray();
                //Выводим первое изображение
                OutPhotos(album[currentphoto], ref box);
            }
            else
            {
                MessageBox.Show("Нет ссылок");
            }

        }
        //загрузка + вывод через веб-клиента
        static void OutPhotos(string url, ref Image box)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    //Загрузка данных по ссылке
                    byte[] data = webClient.DownloadData(url);
                    //Создание фото из массива байтов
                    using (MemoryStream ms = new MemoryStream(data))
                    {
                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        bitmapImage.StreamSource = ms;
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.EndInit();
                        //Вывод
                        box.Source = bitmapImage;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Не удалось загрузить фото");
            }
        }
        //след. фото
        private void right_Click(object sender, EventArgs e)
        {
            if (outPhotoList.Items.Count != currentphoto)
            {
                currentphoto++;
                OutPhotos(album[currentphoto], ref outPhoto);
            }
            else { MessageBox.Show("Это последнее изображение"); }
        }
        //пред. фото
        private void left_Click(object sender, EventArgs e)
        {
            if (currentphoto != 0)
            {
                currentphoto--;
                OutPhotos(album[currentphoto], ref outPhoto);
            }
            else { MessageBox.Show("Это первое изображение"); }
        }
    }
    //класс для json
    public class User
    {
        public string Name { get; set; }
        public string Body { get; set; }
        public string Url { get; set; }
    }
}