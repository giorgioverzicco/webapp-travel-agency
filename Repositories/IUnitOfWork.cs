namespace webapp_travel_agency.Repositories;

public interface IUnitOfWork
{
    ITravelPackageRepository TravelPackage { get; }
    Task SaveAsync();
}