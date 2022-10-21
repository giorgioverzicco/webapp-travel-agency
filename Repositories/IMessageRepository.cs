using webapp_travel_agency.Models;
using webapp_travel_agency.Repositories;

namespace webapp_travel_agency.Interfaces;

public interface IMessageRepository : IRepository<Message>
{
    void Update(Message message);
}