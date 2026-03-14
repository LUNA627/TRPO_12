using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Core.Models
{
   public class UserInterestGroup : ObservableObject
   {
        private int _userId;
        public int UserId
        {
            get => _userId;
            set
            {
                SetProperty(ref _userId, value);
            }
        }

        private User? _user;
        public User? User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private int _interestGroupId;
        public int InterestGroupId
        {
            get => _interestGroupId;
            set
            {
                SetProperty(ref _interestGroupId, value);
            }
        }

        private InterestGroup? _interestGroup;
        public InterestGroup? InterestGroup
        {
            get => _interestGroup;
            set
            {
                SetProperty(ref _interestGroup, value);
            }
        }

        private DateTime _joinedAt;
        public DateTime JoinedAt
        {
            get => _joinedAt;
            set
            {
                SetProperty(ref _joinedAt, value);
            }
        }

        private bool _isModerator;
        public bool IsModerator
        {
            get => _isModerator;
            set
            {
                SetProperty(ref _isModerator, value);
            }
        }


    }
}
