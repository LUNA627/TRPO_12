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
    /// Логика взаимодействия для GroupDetailWindow.xaml
    /// </summary>
    public partial class GroupDetailWindow : Window, INotifyPropertyChanged
    {
        private readonly InterestGroupService _service;
        private readonly InterestGroup _group;
        private readonly UserInterestGroupService _memberService;

        public GroupDetailWindow(InterestGroupService service, InterestGroup group)
        {
            InitializeComponent();
            _service = service;
            _group = group;

            Members = new ObservableCollection<UserInterestGroup>();


            DataContext = this;

            txtTitle.Text = group.Title;
            txtDescription.Text = group.Description ?? "Описание отсутствует";
            Title = $"Группа: {group.Title}";

            LoadMembers();
        }
        private ObservableCollection<UserInterestGroup> _members;
        public ObservableCollection<UserInterestGroup> Members
        {
            get => _members;
            set { _members = value; OnPropertyChanged(); }
        }

        private void LoadMembers()
        {
            _memberService.GetMembersByGroup(_group.Id);
            Members.Clear();
            foreach (var member in _memberService.Members)
                Members.Add(member);
        }

        private void btnAddMember_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddUserWindow(_memberService, _group.Id);
            if (addWindow.ShowDialog() == true)
            {
                LoadMembers();
            }
        }




        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    }
}
