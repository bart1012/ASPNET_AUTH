using ASPNET_AUTH.Models;

namespace ASPNET_AUTH.Services
{
    public class EventsService
    {
        private EventsModel _eventsModel;

        public EventsService(EventsModel model)
        {
            _eventsModel = model;
        }

        public List<Event> GetAllEvents()
        {
            return _eventsModel.GetAllEvents();
        }

        public Event? GetEventByID(int id)
        {
            return _eventsModel.GetEventByID(id);
        }
    }
}
