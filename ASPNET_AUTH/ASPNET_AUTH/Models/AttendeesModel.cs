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

        public List<Attendee> GetAttendeesByUserId(int id)
        {
            List<Attendee> result = new List<Attendee>();
            var allAttendees = GetAllAttendees();
            foreach (var a in allAttendees)
            {
                if (a.UserId == id)
                {
                    result.Add(a);
                }
            }

            return result;

        }

        public Attendee? GetAttendeesByAttendeeId(int id)
        {
            var allAttendees = GetAllAttendees();
            foreach (var a in allAttendees)
            {
                if (a.Id == id)
                {
                    return a;
                }
            }

            return null;
        }

        public bool AddAttendee(Attendee a)
        {
            EventsModel eventsModel = new EventsModel();

            var allAttendees = GetAllAttendees();
            var allEvents = eventsModel.GetAllEvents();

            Event? attendingEvent = allEvents.FirstOrDefault(e => e.Id == a.EventId);
            if (attendingEvent == null) return false;

            if (attendingEvent.CurrentAttendance + 1 > attendingEvent.MaximumCapacity) return false;

            a.Id = allAttendees.Count + 1;
            attendingEvent.CurrentAttendance++;
            bool updateAttempt = eventsModel.UpdateEvent(attendingEvent);

            if (!updateAttempt) return false;

            allAttendees.Add(a);
            File.WriteAllText("Data/attendees.json", JsonSerializer.Serialize<List<Attendee>>(allAttendees));
            return true;
        }

        


    }
}
