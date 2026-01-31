using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Data
{
    public class UserService
    {
        private readonly AppDbContext _db = BaseDbService.Instance.Context;
        public ObservableCollection<User> Users { get; set; } = new();

        public UserService()
        {
            GetAll();
        }

        public void Add(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges();
            Users.Add(user);
        }

        public void Add(string login, string name, string email, string password)
        {
            var user = User.CreateNew(login, name, email, password);
            _db.Users.Add(user);
            _db.SaveChanges();
            Users.Add(user);
        }

        public void UpdateUser()
        {
            _db.SaveChanges();
        }

        public void GetAll()
        {
            var users = _db.Users.ToList();
            Users.Clear();
            foreach (var item in users)
            {
                Users.Add(item);
            }
        }
    }
}
