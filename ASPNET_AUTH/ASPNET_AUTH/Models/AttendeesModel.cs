using Microsoft.Extensions.Options;
using System.Text.Json;

namespace ASPNET_AUTH.Models
{
    public class AttendeesModel
    {
        public List<Attendee> GetAllAttendees()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var list = JsonSerializer.Deserialize<List<Attendee>>(File.ReadAllText("Data/attendees.json"), options);
            return list;
        }

        public List<Attendee> GetAttendeesByEventId(int id)
        {
            List<Attendee> result = new List<Attendee>();
            var allAttendees = GetAllAttendees();
            foreach (var a in allAttendees)
            {
                if (a.EventId == id)
                {
                    result.Add(a);
                }
            }

            return result;
                
        }

        public bool AddAttendee(Attendee a)
        {
            EventsModel eventsModel = new EventsModel();

            var allAttendees = GetAllAttendees();
            var allEvents = eventsModel.GetAllEvents();

            if (!allEvents.Any(e => e.Id == a.EventId)) return false;

            allAttendees.Add(a);
            File.WriteAllText("Data/attendees.json", JsonSerializer.Serialize<List<Attendee>>(allAttendees));
            return true;

        }
    }
}
