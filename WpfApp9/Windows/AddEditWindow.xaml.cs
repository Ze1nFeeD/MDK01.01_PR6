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
    /// Логика взаимодействия для AddEditWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        public AddEditWindow(Products product)
        {
         
           InitializeComponent();
            this.product = product;
            if(product !=null)
            {
               tName.Text = product.Name;
                tPrice.Text = product.Price.ToString();
            }

            
        }
        Products product;
        private void Button_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                if (product == null)
                {

                   product= new Products();
                    product.Name = tName.Text;
                    product.Price = Convert.ToInt32(tPrice.Text);

                    App.DB.Products.Add(product);
                    App.DB.SaveChanges();
                    secondWindow secondWindow = new secondWindow();
                    secondWindow.Show();
                    this.Close();
                }
                else
                {
                    product.Name = tName.Text;
                    product.Price = Convert.ToInt32(tPrice.Text);
                    App.DB.SaveChanges();
                }
            }
            catch { MessageBox.Show("Ошибка"); }
        }
    }
}
