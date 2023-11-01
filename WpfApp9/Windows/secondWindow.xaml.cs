using System;
using System.Collections.Generic;
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

namespace WpfApp9.Windows
{
    /// <summary>
    /// Логика взаимодействия для secondWindow.xaml
    /// </summary>
    public partial class secondWindow : Window
    {
        Products product;
        public secondWindow()
        {
            InitializeComponent();

            if (App.currentUser.Role == "Гость")
            {
                tFIO.Text = "Гость";
            }
            else
            {
                tFIO.Text = App.currentUser.FullName;
                dgProducts.ItemsSource = App.DB.Products.ToList();
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.currentUser.Role == "Администратор")
                {
                    AddEditWindow addEditWindow = new AddEditWindow(product);
                    addEditWindow.Show();
                    this.Close();
                    restartDg();
                }
                else
                {
                    MessageBox.Show("Не достаточно прав!");
                }
            }
            catch { }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.currentUser.Role == "Администратор")
                {
                    product = dgProducts.SelectedItem as Products;
                    AddEditWindow addEditWindow = new AddEditWindow(product);
                    addEditWindow.tZag.Text = "Изменение продукта";
                    addEditWindow.Show();
                    this.Close();
                    restartDg();

                }
                else
                {
                    MessageBox.Show("Не достаточно прав!");
                }
            }
            catch { }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (App.currentUser.Role == "Администратор")
                {
                    product = dgProducts.SelectedItem as Products;
                    App.DB.Products.Remove(product);
                    App.DB.SaveChanges();
                    restartDg();

                }
                else
                {
                    MessageBox.Show("Не достаточно прав!");
                }
            }
            catch { }
        }
        public void restartDg()
        {
            dgProducts.ItemsSource = null;
            dgProducts.ItemsSource = App.DB.Products.ToList();
        }
    }
}
