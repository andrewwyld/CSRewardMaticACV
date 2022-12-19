#nullable enable
using System.Collections.Generic;

namespace RewardMatic_4000.Infrastructure
{
    public class RewardGroup
    {
        private readonly List<Reward> _groupRewards;

        public string Name { get; }

        public List<Reward> Rewards
        {
            get
            { 
                return _groupRewards;
            }
        }

        public RewardGroup(string name, List<Reward> rewards)
        {
            Name = name;
            _groupRewards = rewards;
        }

        public Reward? GetRewardByIndex(int i)
        {
            if (i < _groupRewards.Count)
            {
                return _groupRewards[i];
            }
            else
            {
                return null;
            }
        }
    }
}