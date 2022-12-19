#nullable enable

using RewardMatic_4000;
using RewardMatic_4000.Infrastructure;

namespace RewardMatic_4000.Model
{
    public class User
    {
        private int _score = 0;
        private int _nextIndex = 0;
        private int _nextScore;

        public User()
        {
            _nextScore = Reward.AvailableRewards[0].ScoreDifferential;
        }

        public int Score
        {
            get { return _score; }
        }

        public void UpdateScore(int update)
        {
            _score += update;

            while (_nextScore < _score)
            {
                _nextIndex++;

                if (_nextIndex >= Reward.AvailableRewards.Length)
                {
                    break;
                }

                _nextScore += Reward.AvailableRewards[_nextIndex].ScoreDifferential;
            }
        }

        public Reward? GetRewardInProgress()
        {
            if (_nextIndex < Reward.AvailableRewards.Length)
            {
                return Reward.AvailableRewards[_nextIndex];
            }

            return null;
        }

        public Reward? GetLatestRewardReceived()
        {
            if (_nextIndex - 1 >= 0)
            {
                return Reward.AvailableRewards[_nextIndex - 1];
            }

            return null;
        }
    }
}