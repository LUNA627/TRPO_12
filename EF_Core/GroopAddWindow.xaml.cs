using EF_Core.Models;
using EF_Core.Service;
using System;
using System.Linq;
using System.Windows;

namespace EF_Core
{
    public partial class GroopAddWindow : Window
    {
        private readonly InterestGroupService _service;
        private readonly InterestGroup _group;
        private readonly bool _isEdit;

        public GroopAddWindow(InterestGroupService service)
        {
            InitializeComponent();
            _service = service;
            _group = new InterestGroup(); 
            _isEdit = false;
            DataContext = _group; 
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var title = _group.Title?.Trim();
            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Название группы обязательно!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool isDuplicate = _service.Groups.Any(g =>
                g.Title.Equals(title, StringComparison.OrdinalIgnoreCase) &&
                (!_isEdit || g.Id != _group.Id));

            if (isDuplicate)
            {
                MessageBox.Show("Группа с таким названием уже существует!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }


            _service.GroupAdd(_group);

            DialogResult = true;
            Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        
    }
}