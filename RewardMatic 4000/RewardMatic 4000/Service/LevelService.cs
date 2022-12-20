using RewardMatic_4000.Infrastructure;
using RewardMatic_4000.Model;
using System.Collections.Generic;

namespace RewardMatic_4000.Service
{
    public class LevelService : ILevelService
    {
        private readonly IRewardsReader _reader;
        private User _user;

        public LevelService(IRewardsReader reader)
        {
            _reader = reader;
        }

        public User GetUser()
        {
            if (_user == null)
            {
                IList<RewardGroup> groups = _reader.Read();
                _user = new User(groups);
            }

            return _user;
        }
    }
}
