using webapp_travel_agency.Interfaces;

namespace webapp_travel_agency.Repositories;

public interface IUnitOfWork
{
    ITravelPackageRepository TravelPackage { get; }
    IMessageRepository Message { get; }
    Task SaveAsync();
}