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
using System.Windows.Shapes;

namespace EF_Core
{
    /// <summary>
    /// Логика взаимодействия для GroupEditWindow.xaml
    /// </summary>
    public partial class GroupEditWindow : Window
    {
        private InterestGroupService _service;
        private InterestGroup _group;
        public GroupEditWindow(InterestGroupService service, InterestGroup selectedGroup)
        {
            InitializeComponent();
            _service = service;

            _group = new InterestGroup
            {
                Id = selectedGroup.Id,
                Title = selectedGroup.Title,
                Description = selectedGroup.Description
            };

            DataContext = _group;
        }

        private void BtnEditGroup_Click(object sender, RoutedEventArgs e)
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
                g.Id != _group.Id);

            if (isDuplicate)
            {
                MessageBox.Show("Группа с таким названием уже существует!", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var original = _service.Groups.FirstOrDefault(g => g.Id == _group.Id);
            if (original != null)
            {
                original.Title = _group.Title;
                original.Description = _group.Description;
                _service.Commit();
            }

            DialogResult = true;
            Close();
        }

        private void BtnBack_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
