using webapp_travel_agency.Data;
using webapp_travel_agency.Models;

namespace webapp_travel_agency.Repositories;

public class TravelPackageRepository : Repository<TravelPackage>, ITravelPackageRepository
{
    private readonly ApplicationDbContext _db;

    public TravelPackageRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(TravelPackage travelPackage)
    {
        _db.TravelPackages.Update(travelPackage);
    }
}