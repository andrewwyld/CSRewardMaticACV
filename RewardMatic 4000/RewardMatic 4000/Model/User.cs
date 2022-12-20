#nullable enable

using RewardMatic_4000.Infrastructure;
using System.Collections.Generic;

namespace RewardMatic_4000.Model
{
    public class User
    {
        private long _score = 0;
        private int _nextIndex = 0;
        private int _nextScore;

        private readonly IList<RewardGroup> _rewardGroups;
        private int _currentGroupIndex;

        public User(IList<RewardGroup>  rewardGroups)
        {
            _rewardGroups = rewardGroups;
            _nextScore = _rewardGroups[0].GetRewardByIndex(0).ScoreDifferential;
            _currentGroupIndex = 0;
        }

        public long Score
        {
            get { return _score; }
        }

        public void UpdateScore(long update)
        {
            checked
            {
                _score += update;
            }

            while (_nextScore <= _score)
            {
                _nextIndex++;

                if (_currentGroupIndex >= _rewardGroups.Count ||
                    _nextIndex >= _rewardGroups[_currentGroupIndex].GetRewardsCount())
                {
                    _currentGroupIndex++;
                    _nextIndex = 0;
                }

                if (_currentGroupIndex >= _rewardGroups.Count)
                {
                    break;
                }

                Reward? reward = _rewardGroups[_currentGroupIndex].GetRewardByIndex(_nextIndex);
                if (reward == null)
                {
                    break;
                }

                _nextScore += reward.ScoreDifferential;
            }
        }

        public Reward? GetRewardInProgress()
        {
            if (_currentGroupIndex < _rewardGroups.Count && 
                _nextIndex < _rewardGroups[_currentGroupIndex].GetRewardsCount())
            {
                return _rewardGroups[_currentGroupIndex].GetRewardByIndex(_nextIndex);
            }

            return null;
        }

        public Reward? GetLatestRewardReceived()
        {
            if (_nextIndex == 0 && _currentGroupIndex >= 1)
            {
                int index = _rewardGroups[_currentGroupIndex - 1].GetRewardsCount() - 1;
                return _rewardGroups[_currentGroupIndex - 1].GetRewardByIndex(index);
            }
            else if (_nextIndex > 0)
            {
                return _rewardGroups[_currentGroupIndex].GetRewardByIndex(_nextIndex - 1);
            }

            return null;
        }

        public RewardGroup? GetRewardGroupInProgress()
        {
            if (_currentGroupIndex < _rewardGroups.Count)
            {
                return _rewardGroups[_currentGroupIndex];
            }

            return null;
        }

        public RewardGroup? GetLatestRewardGroupReceived()
        {
            if (_currentGroupIndex == 0 && _nextIndex == 0)
            {
                return null;
            }
            else if (_currentGroupIndex > 0 && _nextIndex == 0)
            {
                return _rewardGroups[_currentGroupIndex - 1];
            }
            else if (_currentGroupIndex < _rewardGroups.Count)
            { 
                return _rewardGroups[_currentGroupIndex];
            }

            return null;
        }

        public RewardGroup? GetCompleteRewardGroupReceived()
        {
            if (_currentGroupIndex == 0)
            {
                return null;
            }

            return _rewardGroups[_currentGroupIndex - 1];
        }
    }
}