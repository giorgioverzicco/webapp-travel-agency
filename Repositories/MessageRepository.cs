using webapp_travel_agency.Data;
using webapp_travel_agency.Interfaces;
using webapp_travel_agency.Models;

namespace webapp_travel_agency.Repositories;

public class MessageRepository : Repository<Message>, IMessageRepository
{
    private readonly ApplicationDbContext _db;
    
    public MessageRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Message message)
    {
        _db.Messages.Update(message);
    }
}