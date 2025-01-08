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

        public bool AddSpeaker(Speaker speaker)
        {
            var allSpeakers = GetAllSpeakers();

            if (allSpeakers.Any(s => s.Id == speaker.Id)) return false;

            if (String.IsNullOrEmpty(speaker.Name)) return false;

            EventsModel eventsModel = new EventsModel();
            var allEvents = eventsModel.GetAllEvents();

            if (!allEvents.Any(e => e.Id == speaker.EventId)) return false;

            allSpeakers.Add(speaker);

            File.WriteAllText("Data/speakers.json", JsonSerializer.Serialize<List<Speaker>>(allSpeakers));
            return true;
        }

        public bool RemoveSpeaker(int id)
        {
            var allSpeakers = GetAllSpeakers();

            Speaker? speakerToRemove = allSpeakers.FirstOrDefault(s => s.Id == id);
            if (speakerToRemove == null) return false;

            allSpeakers.Remove(speakerToRemove);

            File.WriteAllText("Data/speakers.json", JsonSerializer.Serialize<List<Speaker>>(allSpeakers));
            return true;
        }

        public bool UpdateSpeaker(Speaker newSpeaker)
        {
            var allSpeakers = GetAllSpeakers();
            Speaker? speakerToUpdate = allSpeakers.FirstOrDefault(s => s.Id == newSpeaker.Id);

            if (speakerToUpdate == null) return false;

            bool removeAttempt = RemoveSpeaker(speakerToUpdate.Id);

            if (!removeAttempt) return false;

            bool addAttempt = AddSpeaker(newSpeaker);

            if (!addAttempt)
            {
                AddSpeaker(speakerToUpdate);
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}
