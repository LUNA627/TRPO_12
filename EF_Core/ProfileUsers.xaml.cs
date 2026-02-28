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
using EF_Core.Data;
using EF_Core.Models;

namespace EF_Core
{
    /// <summary>
    /// Логика взаимодействия для ProfileUsers.xaml
    /// </summary>
    public partial class ProfileUsers : Page
    {
        public User _currentUser;
        public AppDbContext _db;
        public ProfileUsers(User user, AppDbContext db)
        {
            InitializeComponent();
            _currentUser = user;
            _db = db;
            if (_db == null)
            {
                throw new Exception("Бд не передан");
            }
            DataContext = _currentUser;

            if (_currentUser.Role == null && _db.Entry(_currentUser).Reference(u => u.Role).IsLoaded == false)
            {
                _db.Entry(_currentUser).Reference(u => u.Role).Load();
            }
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            var roles = _db.Roles.ToList();
            NavigationService.Navigate(new ChangedUserPage(_currentUser, roles, _db));
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
