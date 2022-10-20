using webapp_travel_agency.Models;

namespace webapp_travel_agency.Repositories;

public interface ITravelPackageRepository : IRepository<TravelPackage>
{
    void Update(TravelPackage travelPackage);
}