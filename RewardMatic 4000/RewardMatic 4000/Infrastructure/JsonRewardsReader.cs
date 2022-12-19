using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace RewardMatic_4000.Infrastructure
{
    public class JsonRewardsReader : IRewardsReader
    {
        private readonly string _jsonPath;

        public JsonRewardsReader(string jsonPath)
        { 
            _jsonPath= jsonPath;
        }

        public IList<RewardGroup> Read()
        {
            using (StreamReader reader = new(_jsonPath))
            {
                string json = reader.ReadToEnd();
                JsonSerializerOptions options = new JsonSerializerOptions();
                options.PropertyNameCaseInsensitive = true;
                return JsonSerializer.Deserialize<IList<RewardGroup>>(json, options);
            }
        }
    }
}
