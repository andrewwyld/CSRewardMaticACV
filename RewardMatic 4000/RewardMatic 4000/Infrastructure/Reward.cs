using System.Text.Json.Serialization;

namespace RewardMatic_4000.Infrastructure
{
    public class Reward
    {
        // the score you need to attain to get the reward

        // the reward message

        public Reward(int scoreDifferential, string message)
        {
            ScoreDifferential = scoreDifferential;
            Message = message;
        }

        public int ScoreDifferential { get; }

        [JsonPropertyName("name")]
        public string Message { get; }
    }
}