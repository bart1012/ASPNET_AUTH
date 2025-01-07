using System.Text.Json;

namespace ASPNET_AUTH.Models
{
    public class SpeakersModel
    {
        public List<Speaker> GetAllSpeakers()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var list = JsonSerializer.Deserialize<List<Speaker>>(File.ReadAllText("Data/speakers.json"), options);
            return list;
        }

        public List<Speaker> GetSpeakersByEventId(int id)
        {
            List<Speaker> result = new List<Speaker>();
            var allSpeakers = GetAllSpeakers();
            foreach (var a in allSpeakers)
            {
                if (a.EventId == id)
                {
                    result.Add(a);
                }
            }

            return result;

        }
    }
}
