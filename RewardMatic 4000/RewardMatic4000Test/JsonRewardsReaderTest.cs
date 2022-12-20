using NUnit.Framework;
using RewardMatic_4000.Infrastructure;
using System.Collections.Generic;

namespace RewardMatic4000Test
{
    public class JsonRewardsReaderTest
    {
        [Test]
        public void ReadJson()
        {
            IRewardsReader reader = new JsonRewardsReader("rewards.json");
            IList<RewardGroup> rewardGroups = reader.Read();

            Assert.That(rewardGroups.Count, Is.EqualTo(5));
            Assert.That(rewardGroups[0].Name, Is.EqualTo("Getting Started"));
            Assert.That(rewardGroups[0].GetRewardsCount(), Is.EqualTo(6));
            Assert.That(rewardGroups[0].GetRewardByIndex(0).ScoreDifferential, Is.EqualTo(200));
            Assert.That(rewardGroups[0].GetRewardByIndex(0).Message, Is.EqualTo("Starting strong!"));
        }
    }
}
