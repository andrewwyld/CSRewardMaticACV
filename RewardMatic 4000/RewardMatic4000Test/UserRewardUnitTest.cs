using NUnit.Framework;
using RewardMatic_4000.Infrastructure;
using RewardMatic_4000.Model;

namespace RewardMatic_4000
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        // test to make sure a user's score updates correctly and is arithmetically consistent
        [Test]
        public void TestScoreIncrementsCorrectly()
        {
            User aspidistra = new User();

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
            User rangdo = new User();
            
            Assert.That(Reward.AvailableRewards[0], Is.EqualTo(rangdo.GetRewardInProgress()));
            
            rangdo.UpdateScore(250);
            
            Assert.That(Reward.AvailableRewards[1], Is.EqualTo(rangdo.GetRewardInProgress()));
            
            rangdo.UpdateScore(250000);
            
            Assert.IsNull(rangdo.GetRewardInProgress());
        }

        [Test]
        public void TestRewardInProgressSmallStep()
        {
            User rangdo = new User();

            Assert.That(Reward.AvailableRewards[0], Is.EqualTo(rangdo.GetRewardInProgress()));

            rangdo.UpdateScore(10);

            Assert.That(Reward.AvailableRewards[0], Is.EqualTo(rangdo.GetRewardInProgress()));

            rangdo.UpdateScore(10);

            Assert.That(Reward.AvailableRewards[0], Is.EqualTo(rangdo.GetRewardInProgress()));
        }

        // test to make sure the "latest reward received" function works correctly
        // TODO implement User.GetLatestRewardReceived()
        [Test]
        public void TestLatestReward()
        {
            User argond = new User();
            
            Assert.IsNull(argond.GetLatestRewardReceived());
            
            argond.UpdateScore(250);
            
            Assert.That(argond.GetLatestRewardReceived(), Is.EqualTo(Reward.AvailableRewards[0]));
            
            argond.UpdateScore(250000);
            
            Assert.That(argond.GetLatestRewardReceived(), Is.EqualTo(Reward.AvailableRewards[5]));
        }

        [Test]
        public void TestLatestRewardSmallStep()
        {
            User argond = new User();

            Assert.That(argond.GetLatestRewardReceived(), Is.Null);

            argond.UpdateScore(20);

            Assert.That(argond.GetLatestRewardReceived(), Is.Null);

            argond.UpdateScore(20);

            Assert.That(argond.GetLatestRewardReceived(), Is.Null);
        }
    }
}