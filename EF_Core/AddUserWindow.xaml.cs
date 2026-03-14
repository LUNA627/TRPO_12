using EF_Core.Models;
using EF_Core.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EF_Core
{
    /// <summary>
    /// Логика взаимодействия для AddUserWindow.xaml
    /// </summary>
    public partial class AddUserWindow : Window, INotifyPropertyChanged
    {
        private readonly UserInterestGroupService _service;
        private readonly int _groupId;
        private readonly UserService _userService = new UserService();  
        public AddUserWindow(UserInterestGroupService service, int groupId)
        {
            InitializeComponent();
            _service = service;
            _groupId = groupId;

            JoinedAt = DateTime.Today;
            IsModerator = false;

            AvailableUsers = new ObservableCollection<User>();
            LoadUsers();

            DataContext = this;
        }

        private User _selectedUser;
        public User SelectedUser
        {
            get => _selectedUser;
            set { _selectedUser = value; OnPropertyChanged(); }
        }

        private DateTime _joinedAt;
        public DateTime JoinedAt
        {
            get => _joinedAt;
            set { _joinedAt = value; OnPropertyChanged(); }
        }

        private bool _isModerator;
        public bool IsModerator
        {
            get => _isModerator;
            set { _isModerator = value; OnPropertyChanged(); }
        }

        private ObservableCollection<User> _availableUsers;
        public ObservableCollection<User> AvailableUsers
        {
            get => _availableUsers;
            set { _availableUsers = value; OnPropertyChanged(); }
        }

        private void LoadUsers()
        {
            _userService.GetAll();
            var existingIds = _service.Members.Select(m => m.UserId).ToList();
            var available = _userService.Users
                .Where(u => !existingIds.Contains(u.Id))
                .ToList();

            AvailableUsers.Clear();
            foreach (var user in available)
                AvailableUsers.Add(user);
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Выберите пользователя!");
                return;
            }

            if (JoinedAt == default)
            {
                MessageBox.Show("Выберите дату вступления!");
                return;
            }

            if (_service.Members.Any(m => m.UserId == SelectedUser.Id && m.InterestGroupId == _groupId))
            {
                MessageBox.Show("Пользователь уже в этой группе!");
                return;
            }

            var newMember = new UserInterestGroup
            {
                UserId = SelectedUser.Id,
                InterestGroupId = _groupId,
                JoinedAt = JoinedAt,
                IsModerator = IsModerator
            };

            _service.Add(newMember);
            DialogResult = true;
            Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
