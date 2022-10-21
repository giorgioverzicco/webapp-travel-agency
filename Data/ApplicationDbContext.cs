using Microsoft.EntityFrameworkCore;
using webapp_travel_agency.Models;

namespace webapp_travel_agency.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(
        DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }

    public virtual DbSet<TravelPackage> TravelPackages { get; set; } = default!;
    public virtual DbSet<Message> Messages { get; set; } = default!;
}