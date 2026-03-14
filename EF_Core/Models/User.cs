using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EF_Core.Models
{
    public class User : ObservableObject
    {
        public User()
        {
        }

        private int _id;
        public int Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }

        private string _login = string.Empty;
        public string Login
        {
            get => _login;
            set => SetProperty(ref _login, value?.Trim() ?? string.Empty);
        }

        private string _name = string.Empty;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value?.Trim() ?? string.Empty);
        }

        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set => SetProperty(ref _email, value?.Trim() ?? string.Empty);
        }

        private string _password = string.Empty;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value ?? string.Empty);
        }

        private DateTime _createdAt;
        public DateTime CreatedAt
        {
            get => _createdAt;
            set => SetProperty(ref _createdAt, value);
        }

        public static User CreateNew(string login, string name, string email, string password)
        {
            return new User
            {
                Login = login,
                Name = name,
                Email = email,
                Password = password,
                CreatedAt = DateTime.UtcNow 
            };
        }


        private UserProfile _profile;
        public UserProfile Profile
        {
            get => _profile;
            set => SetProperty(ref _profile, value);
        }


        private int _roleId;
        public int RoleId
        {
            get => _roleId;
            set => SetProperty(ref _roleId, value);
        }


        private Role _role;
        public Role Role
        {
            get => _role;
            set => SetProperty(ref _role, value);
        }


        private ObservableCollection<UserInterestGroup> _userInterestGroups;
        public ObservableCollection<UserInterestGroup> UserInterestGroups
        {
            get => _userInterestGroups;
            set => SetProperty(ref _userInterestGroups, value);
        }

    }
}
