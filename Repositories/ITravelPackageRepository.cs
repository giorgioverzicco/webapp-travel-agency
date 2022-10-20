using webapp_travel_agency.Models;

namespace webapp_travel_agency.Repositories;

public interface ITravelPackageRepository : IRepository<TravelPackage>
{
    void Update(TravelPackage travelPackage);
    Task<List<TravelPackage>> GetByFilter(string? name, int? rating, string? city, string? state, decimal? price);
}