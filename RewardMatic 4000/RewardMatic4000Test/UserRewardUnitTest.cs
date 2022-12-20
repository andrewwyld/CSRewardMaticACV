using NUnit.Framework;
using RewardMatic_4000.Infrastructure;
using RewardMatic_4000.Model;
using System.Collections.Generic;

namespace RewardMatic_4000
{
    public class Tests
    {
        private static readonly IList<RewardGroup> RewardGroups;
        private static readonly List<Reward> AvailableRewards;

        static Tests()
        {
            AvailableRewards = new List<Reward>()
            {
                new Reward(200, "Starting strong!"),
                new Reward(300, "Getting better!"),
                new Reward(400, "Nice job!"),
                new Reward(500, "Keep going for cake!"),
                new Reward(600, "OK, there's no cake. Keep going anyway!"),
                new Reward(700, "YOU FINISHED! You are the only person to do this."),
            };

            RewardGroups = new List<RewardGroup>()
            {
                new RewardGroup("TestGroup", AvailableRewards)
            };
        }

        // test to make sure a user's score updates correctly and is arithmetically consistent
        [Test]
        public void TestScoreIncrementsCorrectly()
        {
            User aspidistra = new User(RewardGroups);

            Assert.That(aspidistra.Score, Is.EqualTo(0));
            
            aspidistra.UpdateScore(250);
            
            Assert.That(aspidistra.Score, Is.EqualTo(250));

            aspidistra.UpdateScore(250000);
            
            Assert.That(aspidistra.Score, Is.EqualTo(250250));
        }

        // test to make sure the "reward in progress" function works correctly
        // TODO implement User.GetRewardInProgress()
        [Test]
        public void TestRewardInProgress()
        {
            User rangdo = new User(RewardGroups);
            
            Assert.That(AvailableRewards[0], Is.EqualTo(rangdo.GetRewardInProgress()));
            
            rangdo.UpdateScore(250);
            
            Assert.That(AvailableRewards[1], Is.EqualTo(rangdo.GetRewardInProgress()));
            
            rangdo.UpdateScore(250000);
            
            Assert.IsNull(rangdo.GetRewardInProgress());
        }

        [Test]
        public void TestRewardInProgressSmallStep()
        {
            User rangdo = new User(RewardGroups);

            Assert.That(AvailableRewards[0], Is.EqualTo(rangdo.GetRewardInProgress()));

            rangdo.UpdateScore(10);

            Assert.That(AvailableRewards[0], Is.EqualTo(rangdo.GetRewardInProgress()));

            rangdo.UpdateScore(10);

            Assert.That(AvailableRewards[0], Is.EqualTo(rangdo.GetRewardInProgress()));
        }

        // test to make sure the "latest reward received" function works correctly
        // TODO implement User.GetLatestRewardReceived()
        [Test]
        public void TestLatestReward()
        {
            User argond = new User(RewardGroups);
            
            Assert.IsNull(argond.GetLatestRewardReceived());
            
            argond.UpdateScore(250);
            
            Assert.That(argond.GetLatestRewardReceived(), Is.EqualTo(AvailableRewards[0]));
            
            argond.UpdateScore(250000);
            
            Assert.That(argond.GetLatestRewardReceived(), Is.EqualTo(AvailableRewards[5]));
        }

        [Test]
        public void TestLatestRewardSmallStep()
        {
            User argond = new User(RewardGroups);

            Assert.That(argond.GetLatestRewardReceived(), Is.Null);

            argond.UpdateScore(20);

            Assert.That(argond.GetLatestRewardReceived(), Is.Null);

            argond.UpdateScore(20);

            Assert.That(argond.GetLatestRewardReceived(), Is.Null);
        }

        [Test]
        public void GetRewardGroupInProgress()
        {
            IRewardsReader reader = new JsonRewardsReader("rewards.json");
            IList<RewardGroup> rewardGroups = reader.Read();

            User myself = new User(rewardGroups);
            RewardGroup currentGroup = myself.GetRewardGroupInProgress();

            Assert.That(currentGroup.Name, Is.EqualTo("Getting Started"));

            myself.UpdateScore(200);
            currentGroup = myself.GetRewardGroupInProgress();

            Assert.That(currentGroup.Name, Is.EqualTo("Getting Started"));

            /* complete first group */
            myself.UpdateScore(2500);
            currentGroup = myself.GetRewardGroupInProgress();

            Assert.That(currentGroup.Name, Is.EqualTo("Now We're Cooking"));

            myself.UpdateScore(1);
            currentGroup = myself.GetRewardGroupInProgress();

            Assert.That(currentGroup.Name, Is.EqualTo("Now We're Cooking"));

            /* first reward in second group */
            myself.UpdateScore(249);
            currentGroup = myself.GetRewardGroupInProgress();

            Assert.That(currentGroup.Name, Is.EqualTo("Now We're Cooking"));

            /* max increase, complete all remaining groups */
            myself.UpdateScore(250000);
            currentGroup = myself.GetRewardGroupInProgress();

            Assert.That(currentGroup, Is.Null);
        }

        [Test]
        public void GetLatestRewardGroupReceived()
        {
            IRewardsReader reader = new JsonRewardsReader("rewards.json");
            IList<RewardGroup> rewardGroups = reader.Read();

            User myself = new User(rewardGroups);
            RewardGroup latestGroup = myself.GetLatestRewardGroupReceived();

            Assert.That(latestGroup, Is.Null);

            myself.UpdateScore(50);
            latestGroup = myself.GetLatestRewardGroupReceived();

            Assert.That(latestGroup, Is.Null);

            myself.UpdateScore(150);
            latestGroup = myself.GetLatestRewardGroupReceived();

            Assert.That(latestGroup.Name, Is.EqualTo("Getting Started"));

            myself.UpdateScore(2500);
            latestGroup = myself.GetLatestRewardGroupReceived();

            Assert.That(latestGroup.Name, Is.EqualTo("Getting Started"));

            /* increase, but do not receive second group reward */
            myself.UpdateScore(50);
            latestGroup = myself.GetLatestRewardGroupReceived();

            Assert.That(latestGroup.Name, Is.EqualTo("Getting Started"));

            /* second group first reward */
            myself.UpdateScore(200);
            latestGroup = myself.GetLatestRewardGroupReceived();

            Assert.That(latestGroup.Name, Is.EqualTo("Now We're Cooking"));

            /* max increase */
            myself.UpdateScore(200000);
            latestGroup = myself.GetLatestRewardGroupReceived();

            Assert.That(latestGroup.Name, Is.EqualTo("The Alans of 1970s Library Music"));
        }

        [Test]
        public void GetCompleteRewardGroupReceived()
        {
            IRewardsReader reader = new JsonRewardsReader("rewards.json");
            IList<RewardGroup> rewardGroups = reader.Read();

            User myself = new User(rewardGroups);
            RewardGroup completeGroup = myself.GetCompleteRewardGroupReceived();

            Assert.That(completeGroup, Is.Null);

            /* First group rewards */
            myself.UpdateScore(200);
            completeGroup = myself.GetCompleteRewardGroupReceived();

            Assert.That(completeGroup, Is.Null);

            myself.UpdateScore(300);
            completeGroup = myself.GetCompleteRewardGroupReceived();

            Assert.That(completeGroup, Is.Null);

            myself.UpdateScore(2200);
            completeGroup = myself.GetCompleteRewardGroupReceived();

            Assert.That(completeGroup.Name, Is.EqualTo("Getting Started"));

            /* Second group rewards */
            myself.UpdateScore(250);
            completeGroup = myself.GetCompleteRewardGroupReceived();

            Assert.That(completeGroup.Name, Is.EqualTo("Getting Started"));

            myself.UpdateScore(375);
            completeGroup = myself.GetCompleteRewardGroupReceived();

            Assert.That(completeGroup.Name, Is.EqualTo("Getting Started"));

            myself.UpdateScore(2700);
            completeGroup = myself.GetCompleteRewardGroupReceived();

            Assert.That(completeGroup.Name, Is.EqualTo("Getting Started"));

            myself.UpdateScore(50);
            completeGroup = myself.GetCompleteRewardGroupReceived();

            Assert.That(completeGroup.Name, Is.EqualTo("Now We're Cooking"));

            /* Jump over third group */
            myself.UpdateScore(1700);
            completeGroup = myself.GetCompleteRewardGroupReceived();

            Assert.That(completeGroup.Name, Is.EqualTo("Our Data Entry Operative Has Left"));

            /* Max increase */
            myself.UpdateScore(200000);
            completeGroup = myself.GetCompleteRewardGroupReceived();

            Assert.That(completeGroup.Name, Is.EqualTo("The Alans of 1970s Library Music"));
        }
    }
}