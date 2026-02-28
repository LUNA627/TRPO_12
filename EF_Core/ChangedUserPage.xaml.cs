using EF_Core.Data;
using EF_Core.Models;
using EF_Core.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Логика взаимодействия для ChangedUserPage.xaml
    /// </summary>
    public partial class ChangedUserPage : Page
    {
        public User _user;
        public ObservableCollection<Role> _roles;
        public AppDbContext _db;

        public ChangedUserPage(User user, IEnumerable<Role> roles, AppDbContext db)
        {
            InitializeComponent();
            _user = user;
            _roles = new ObservableCollection<Role>(roles);
            _db = db;
            DataContext = _user;
            Resources["AvailableRoles"] = _roles;
        }

        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_user.Profile == null)
                    _user.Profile = new UserProfile { User = _user };

                
                _db.SaveChanges();

                _db.Entry(_user)
                    .Reference(u => u.Role)
                    .Load(); 

                _db.Entry(_user)
                    .Reference(u => u.Profile)
                    .Load();

                _db.Entry(_user).Reference(u => u.Role).Load();

                var userService = App.UserService; 
                var existing = userService.Users.FirstOrDefault(u => u.Id == _user.Id);
                if (existing != null)
                {
                    existing.Role = _user.Role;
                    existing.Profile = _user.Profile;
                    existing.RoleId = _user.RoleId;
                }

                MessageBox.Show("Сохранено успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                NavigationService.GoBack();
            }
            catch (Exception ex)
            {
                var inner = ex.InnerException;
                string msg = $"Ошибка сохранения:\n{ex.Message}";
                if (inner != null)
                    msg += $"\n\nВнутренняя ошибка:\n{inner.Message}\n{inner.StackTrace}";
                MessageBox.Show(msg, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
