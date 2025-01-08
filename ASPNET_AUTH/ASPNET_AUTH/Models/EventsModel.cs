using System.Text.Json;

namespace ASPNET_AUTH.Models
{
    public class EventsModel
    {
        public List<Event> GetAllEvents()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            var list = JsonSerializer.Deserialize<List<Event>>(File.ReadAllText("Data/events.json"), options);
            return list;
        }

        public Event? GetEventByID(int id)
        {
            var list = GetAllEvents();
            return list.FirstOrDefault(x => x.Id == id);
        }

        public bool AddEvent(Event newEvent)
        {
            var allEvents = GetAllEvents();
            if (allEvents.Any(x => x.Id == newEvent.Id)) return false;

            if (String.IsNullOrEmpty(newEvent.Title)) return false;

            if (newEvent.Date <  DateTime.Now) return false;

            if (String.IsNullOrEmpty(newEvent.Venue)) return false;

            if (newEvent.MaximumCapacity < 2) return false;

            newEvent.CurrentAttendance = 0;

            allEvents.Add(newEvent);

            File.WriteAllText("Data/events.json", JsonSerializer.Serialize(allEvents));
            return true;
        }
    }
}
