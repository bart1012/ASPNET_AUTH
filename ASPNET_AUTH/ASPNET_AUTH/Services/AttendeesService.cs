using ASPNET_AUTH.Models;

namespace ASPNET_AUTH.Services
{
    public class AttendeesService
    {
        private AttendeesModel _model;
        public AttendeesService(AttendeesModel model)
        {
            _model = model;
        }

        public List<Attendee> GetAttendeesAtEvent(int eventId)
        {
            return _model.GetAttendeesByEventId(eventId);
        }

        public List<Attendee> GetAttendeesByUserId(int id)
        {
            return _model.GetAttendeesByUserId(id);

        }

        public Attendee? GetAttendeesByAttendeeId(int id)
        {
            return _model.GetAttendeesByAttendeeId(id);
        }

        public bool AddAttendee(Attendee a)
        {
            return _model.AddAttendee(a);
        }
    }
}
