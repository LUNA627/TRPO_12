using EF_Core.Data;
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
    /// Логика взаимодействия для UpdatePage.xaml
    /// </summary>
    public partial class UpdatePage : Page
    {

        private readonly UserService _service;
        private readonly User _user;
        private readonly bool _isEdit;
        public UpdatePage(UserService service)
        {
            InitializeComponent();
            _service = service;
            _user = new User();
            _isEdit = false;
            DataContext = _user;
        }


        // Конструктор для редактирования
        public UpdatePage(UserService userService, User selectedUser)
        {
            InitializeComponent();
            _service = userService;
            _user = new User
            {
                Id = selectedUser.Id,
                Login = selectedUser.Login,
                Name = selectedUser.Name,
                Email = selectedUser.Email,
                Password = selectedUser.Password,
                CreatedAt = selectedUser.CreatedAt
            };
            _isEdit = true;
            DataContext = _user;
        }

        private void OnSaveClick(object sender, RoutedEventArgs e)
        {
            var email = _user.Email.Trim();
            bool dublEmail = _service.Users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && (!_isEdit || u.Id != _user.Id));

            if (dublEmail)
            {
                MessageBox.Show("Пользователь с таким email уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var login = _user.Login.Trim();
            bool dublLogin = _service.Users.Any(u => u.Id != (_isEdit ? _user.Id : -1) && string.Equals(u.Login, login, StringComparison.OrdinalIgnoreCase));
            if (dublLogin)
            {
                MessageBox.Show("Пользователь с таким логином уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (_isEdit)
            {
                var original = _service.Users.FirstOrDefault(u => u.Id == _user.Id);
                if (original != null)
                {
                    original.Login = _user.Login;
                    original.Name = _user.Name;
                    original.Email = _user.Email;
                    original.Password = _user.Password;
                    _service.UpdateUser();
                }
            }
            else
            {
                _user.CreatedAt = DateTime.UtcNow;
                _service.Add(_user);
            }
            NavigationService.GoBack();

        }


        private void OnCancelClick(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
