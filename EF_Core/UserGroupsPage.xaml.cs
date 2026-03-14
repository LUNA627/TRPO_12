using EF_Core.Data;
using EF_Core.Models;
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
    /// Логика взаимодействия для UserGroupsPage.xaml
    /// </summary>
    public partial class UserGroupsPage : Page
    {
        private readonly AppDbContext _db;
        private readonly User _user;
        private readonly ObservableCollection<UserInterestGroup> _groups;

        public UserGroupsPage(User user, AppDbContext db)
        {
            InitializeComponent();
            _user = user;
            _db = db;
            _groups = new ObservableCollection<UserInterestGroup>();

            LoadGroups();
            lvGroups.ItemsSource = _groups;
        }

        private void LoadGroups()
        {
            // Загружаем связи пользователя с группами + подгружаем данные группы
            var userGroups = _db.UserInterestGroups
                .Where(uig => uig.UserId == _user.Id)
                .ToList();

            // Явно загружаем навигационное свойство InterestGroup для каждой записи
            foreach (var ug in userGroups)
            {
                if (ug.InterestGroup == null)
                {
                    _db.Entry(ug).Reference(u => u.InterestGroup).Load();
                }
            }

            _groups.Clear();
            foreach (var group in userGroups)
                _groups.Add(group);
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}

