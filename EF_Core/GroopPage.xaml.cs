using EF_Core.Models;
using EF_Core.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Логика взаимодействия для GroopPage.xaml
    /// </summary>
    public partial class GroopPage : Page
    {
        // Сервисы — как в MasterPage
        public InterestGroupService Service { get; set; } = new InterestGroupService();
        public UserInterestGroupService MemberService { get; set; } = new UserInterestGroupService();

        // Простые свойства — без private полей и OnPropertyChanged
        public InterestGroup SelectedGroup { get; set; }
        public ObservableCollection<InterestGroup> Groups { get; set; } = new();
        public ObservableCollection<UserInterestGroup> Members { get; set; } = new();
        public UserInterestGroup SelectedMember { get; set; }

        public GroopPage()
        {
            InitializeComponent();
            DataContext = this; // 🔹 Как в MasterPage
            LoadGroups();
        }

        private void LoadGroups()
        {
            Service.GroupGetAll();
            Groups.Clear();
            foreach (var group in Service.Groups)
                Groups.Add(group);
        }

        private void LoadMembers()
        {
            Members.Clear();
            if (SelectedGroup == null) return;

            MemberService.GetMembersByGroup(SelectedGroup.Id);
            foreach (var member in MemberService.Members)
                Members.Add(member);
        }

        private void BtnAddGroop_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new GroopAddWindow(Service);
            if (addWindow.ShowDialog() == true)
            {
                LoadGroups();
            }
        }

        private void BtnChangeGroup_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedGroup == null)
            {
                MessageBox.Show("Выберите группу для редактирования", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var editWindow = new GroupEditWindow(Service, SelectedGroup);
            if (editWindow.ShowDialog() == true)
            {
                LoadGroups();
            }
        }

        private void BtnRemoveGroup_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedGroup == null)
            {
                MessageBox.Show("Выберите группу для удаления", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show(
                $"Вы уверены, что хотите удалить группу \"{SelectedGroup.Title}\"?",
                "Подтверждение удаления",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Service.RemoveGroup(SelectedGroup);
                LoadGroups();
            }
        }

        private void BtnAddMember_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedGroup == null)
            {
                MessageBox.Show("Выберите группу для добавления участника!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var addWindow = new AddUserWindow(MemberService, SelectedGroup.Id);
            if (addWindow.ShowDialog() == true)
            {
                LoadMembers();
            }
        }

        private void lvGroups_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedGroup == null)
            {
                MessageBox.Show("Выберите элемент из списка!");
                return;
            }
            var detailWindow = new GroupDetailWindow(Service, SelectedGroup);
            detailWindow.ShowDialog();
        }

        private void lvGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // При изменении выбора — загружаем участников
            LoadMembers();
        }

        private void miRemoveMember_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedMember != null)  
            {
                var result = MessageBox.Show(
                    $"Удалить участника из группы?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    MemberService.Remove(SelectedMember);
                    LoadMembers();
                }
            }
            else
            {
                MessageBox.Show("Выберите участника для удаления!", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void lvMembers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedMember != null)
            {
                var userService = new UserService();
                var user = userService.Users.FirstOrDefault(u => u.Id == SelectedMember.UserId);

                if (user != null)
                {
                    var db = BaseDbService.Instance;

                    NavigationService.Navigate(new ProfileUsers(user, db));
                }
            }
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
