using System.Threading.Tasks;
using ProEventos.Application.Dtos;

namespace ProEventos.Application.Contracts
{
    public interface IEventService
    {
        Task<EventDto> AddEvents(EventDto model);
        Task<EventDto> UpdateEvent(int eventId, EventDto model);
        Task<bool> DeleteEvent(int eventId);

        Task<EventDto[]> GetAllEventsAsync(bool includeSpeakers = false);
        Task<EventDto[]> GetAllEventsBySubjectAsync(string subject, bool includeSpeakers = false);
        Task<EventDto> GetEventByIdAsync(int eventId, bool includeSpeakers = false);
    }
}