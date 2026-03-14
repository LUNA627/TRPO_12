using EF_Core.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EF_Core.Models;

namespace EF_Core.Service
{
    public class InterestGroupService
    {
        private AppDbContext _db = BaseDbService.Instance;

        public ObservableCollection<InterestGroup> Groups { get; set; } = new();

        public int Commit() => _db.SaveChanges();

        public void GroupGetAll()
        {
            var groupAll = _db.InterestGroups.ToList();
            Groups.Clear();

            foreach (var group in groupAll)
            {
                Groups.Add(group);
            }
        }

        public bool GroupAdd(InterestGroup group)
        {
            if (string.IsNullOrEmpty(group.Title))
            {
                return false;
            }
            if (_db.InterestGroups.Any(u => u.Title == group.Title))
            {
                return false;
            }

            var newGroup = new InterestGroup
            {
                Title = group.Title,
                Description = group.Description,
            };

            _db.InterestGroups.Add(newGroup);

            if (Commit() > 0)
            {
                Groups.Add(newGroup);
                return true;
            }

            return false;
        }

        public void RemoveGroup(InterestGroup group)
        {
            _db.InterestGroups.Remove(group);
            if (Commit() > 0 && Groups.Contains(group)) {
                Groups.Remove(group);
            }
        }

        public InterestGroupService()
        {
            GroupGetAll();
        }
    }
}
