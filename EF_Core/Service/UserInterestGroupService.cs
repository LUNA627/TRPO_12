using EF_Core.Data;
using EF_Core.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Service
{
    public class UserInterestGroupService
    {
        private readonly AppDbContext _db = BaseDbService.Instance;
        public ObservableCollection<UserInterestGroup> Members { get; set; } = new();

        public void GetMembersByGroup(int groupId)
        {
            var members = _db.UserInterestGroups
                .Where(m => m.InterestGroupId == groupId)
                .ToList();

            Members.Clear();
            foreach (var member in members)
                Members.Add(member);
        }

        public void Add(UserInterestGroup member)
        {
            var newMember = new UserInterestGroup
            {
                UserId = member.UserId,
                InterestGroupId = member.InterestGroupId,
                JoinedAt = member.JoinedAt,
                IsModerator = member.IsModerator
            };

            _db.UserInterestGroups.Add(newMember);
            _db.SaveChanges();
            Members.Add(newMember);
        }

        public void Remove(UserInterestGroup member)
        {
            _db.UserInterestGroups.Remove(member);
            if (_db.SaveChanges() > 0 && Members.Contains(member))
                Members.Remove(member);
        }
    }
}
