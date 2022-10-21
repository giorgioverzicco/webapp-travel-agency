using webapp_travel_agency.Data;
using webapp_travel_agency.Interfaces;

namespace webapp_travel_agency.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        TravelPackage = new TravelPackageRepository(_db);
        Message = new MessageRepository(_db);
    }

    public ITravelPackageRepository TravelPackage { get; }
    public IMessageRepository Message { get; }
    
    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}