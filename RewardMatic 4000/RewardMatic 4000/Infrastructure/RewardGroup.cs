#nullable enable
using System.Collections.Generic;

namespace RewardMatic_4000.Infrastructure
{
    public class RewardGroup
    {
        public string Name { get; }

        public List<Reward> Rewards { get; }

        public RewardGroup(string name, List<Reward> rewards)
        {
            Name = name;
            Rewards = rewards;
        }

        public Reward? GetRewardByIndex(int i)
        {
            if (i < Rewards.Count)
            {
                return Rewards[i];
            }
            else
            {
                return null;
            }
        }
    }
}