using Microsoft.EntityFrameworkCore;
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

    public Task<List<TravelPackage>> GetByFilter(
        string? name, int? rating, string? city, string? state, decimal? price)
    {
        IQueryable<TravelPackage> query = _db.TravelPackages;

        if (name is not null)
        {
            query = query.Where(x => x.Name.ToLower().Contains(name));
        }
        
        if (rating is not null)
        {
            query = query.Where(x => x.Rating >= rating);
        }
        
        if (city is not null)
        {
            query = query.Where(x => x.City.ToLower().Contains(city));
        }
        
        if (state is not null)
        {
            query = query.Where(x => x.State.ToLower().Contains(state));
        }
        
        if (price is not null)
        {
            query = query.Where(x => x.PricePerAdult >= price);
        }

        return query.ToListAsync();
    }
}