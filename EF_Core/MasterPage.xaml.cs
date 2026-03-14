using EF_Core.Data;
using EF_Core.Models;
using EF_Core.Service;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EF_Core
{
    /// <summary>
    /// Логика взаимодействия для MasterPage.xaml
    /// </summary>
    public partial class MasterPage : Page
    {

        public UserService Service { get; set; } = new UserService();
        public User? SelectedUser { get; set; }
        public User? user { get; set; } = null;

        

        public MasterPage()
        {
            InitializeComponent();

            DataContext = this;
        }


        private void OnAddClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UpdatePage(Service));
        }


        private void OnUpdateClick(object sender, RoutedEventArgs e)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Выберите пользователя из списка.");
                return;
            }

            NavigationService.Navigate(new UpdatePage(Service, SelectedUser));

        }


        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Выберите элемент из списка!");
                return;
            }
            NavigationService.Navigate(new ProfileUsers(SelectedUser, BaseDbService.Instance));
        }

        private void LookRolesClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CheckRolesPage());
        }

        private void WorkWithGropsClick(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new GroopPage());
        }
    }
}
