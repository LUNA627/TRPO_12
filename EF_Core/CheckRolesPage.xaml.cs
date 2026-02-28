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
    /// Логика взаимодействия для CheckRolesPage.xaml
    /// </summary>
    public partial class CheckRolesPage : Page
    {
        private readonly AppDbContext _db;
        private readonly UserService _userService;

        public ObservableCollection<Role> Roles { get; } = new();
        public ObservableCollection<User> UsersByRole { get; } = new();


        public int? SelectedRoleId
        {
            get => _selectedRoleId;
            set
            {
                _selectedRoleId = value;
                LoadUsersByRole();
            }
        }
        private int? _selectedRoleId;

        public CheckRolesPage()
        {
            InitializeComponent();

            _db = BaseDbService.Instance;
            _userService = App.UserService;

            // Загружаем роли
            var roles = _db.Roles.ToList();
            foreach (var role in roles)
                Roles.Add(role);

            // По умолчанию — показываем всех пользователей (или первую роль)
            if (Roles.Any())
                SelectedRoleId = Roles.First().Id;

            DataContext = this;
        }


        private void LoadUsersByRole()
        {
            UsersByRole.Clear();

            if (SelectedRoleId == null)
                return;

            var users = _db.Users
                .Include(u => u.Role)
                .Where(u => u.RoleId == SelectedRoleId.Value)
                .ToList();

            foreach (var user in users)
                UsersByRole.Add(user);
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
