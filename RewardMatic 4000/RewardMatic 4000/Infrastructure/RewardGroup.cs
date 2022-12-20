#nullable enable
using System.Collections.Generic;

namespace RewardMatic_4000.Infrastructure
{
    public class RewardGroup
    {
        public string Name { get; }

        /// <summary>
        /// Should consider defining custom JSON converter for deserialization and having a private field instead, for 
        /// safety concerns, so that the list cannot be altered from the public interface.
        /// </summary>
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

        public int GetRewardsCount()
        { 
            return Rewards.Count;
        }
    }
}