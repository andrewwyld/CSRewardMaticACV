using System.Collections.Generic;

namespace RewardMatic_4000.Infrastructure
{
    public interface IRewardsReader
    {
        IList<RewardGroup> Read();
    }
}
