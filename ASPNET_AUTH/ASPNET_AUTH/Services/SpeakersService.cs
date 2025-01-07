using ASPNET_AUTH.Models;

namespace ASPNET_AUTH.Services
{
    public class SpeakersService
    {
        private SpeakersModel _Model;
        public SpeakersService(SpeakersModel model)
        {
            _Model = model;
        }

        public List<Speaker> GetSpeakersAtEvent(int eventId)
        {
            return _Model.GetSpeakersByEventId(eventId);
        }
    }
}
