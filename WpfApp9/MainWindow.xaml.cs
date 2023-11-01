using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp9.Windows;
using System.Drawing;


namespace WpfApp9
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
         Random random = new Random();
        string captchaText;
        bool captchaPassed = true;
        bool previousLoginAttemptFailed = false;
        public MainWindow()
        {
            InitializeComponent();           
        }

        private void btnAuth_Click(object sender, RoutedEventArgs e)
        {
            var currentUser = App.DB.Users.FirstOrDefault(u => u.Login == tLog.Text && u.Password == tPas.Text);

            if (previousLoginAttemptFailed)
            {
                if (tCaptcha.Text == captchaText)
                {
                    captchaPassed = true;
                }
                else
                {
                    captchaPassed = false;
                    refreshCaptcha();
                    MessageBox.Show("Не правильно введена капча");
                }
            }

            if (captchaPassed)
            {
                if (currentUser != null)
                {
                    App.currentUser = currentUser;
                    secondWindow secondWindow = new secondWindow();
                    secondWindow.Show();
                    this.Close();
                }
                else
                {
                    previousLoginAttemptFailed = true;
                    captchaImage.Visibility = Visibility.Visible;
                    tbCaptcha.Visibility = Visibility.Visible;
                    tCaptcha.Visibility = Visibility.Visible;
                    MessageBox.Show("Ошибка авторизации");
                    refreshCaptcha();
                }
            }
        }

        private void btnGuest_Click(object sender, RoutedEventArgs e)
        {
            Users user = new Users();
            user.Role = "Гость";
            App.currentUser = user;
            secondWindow secondWindow = new secondWindow();
            secondWindow.Show();
        }

        private void btnCan_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private string generateRandomCaptchaText(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(chars[random.Next(chars.Length)]);
            }
            return stringBuilder.ToString();

        }
        private BitmapImage generateCaptchaImage(string text, int width, int height)
        {
            System.Drawing.Bitmap image = new System.Drawing.Bitmap(width, height);
            System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(image);
            graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            RectangleF rectF = new RectangleF(0, 0, width, height);
            using (SolidBrush brush = new SolidBrush(System.Drawing.Color.White))
            {
                graphics.FillRectangle(brush, 0, 0, width, height);
            }
            using (System.Drawing.Font font = new System.Drawing.Font("Arial", 12, System.Drawing.FontStyle.Bold))
            {
                graphics.DrawString(text, font, Brushes.Black, rectF);
            }

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = ms;
            bitmapImage.EndInit();

            return bitmapImage;
        }
        private void refreshCaptcha()
        {
            captchaText = generateRandomCaptchaText(6); // Генерируем случайную капчу из 6 символов
            captchaImage.Source = generateCaptchaImage(captchaText, (int)captchaImage.Width, (int)captchaImage.Height);
        }
    }
}
