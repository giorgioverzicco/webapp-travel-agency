using webapp_travel_agency.Data;

namespace webapp_travel_agency.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;

    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        TravelPackage = new TravelPackageRepository(_db);
    }

    public ITravelPackageRepository TravelPackage { get; }
    
    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}