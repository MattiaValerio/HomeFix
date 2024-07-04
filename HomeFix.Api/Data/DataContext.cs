using HomeFix.Shared.Entities;
using HomeFix.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace HomeFix.Api.Data;

public class DataContext : DbContext
{
    private DbContextOptions<DataContext> _options;
    private readonly IConfiguration _conf;

    public DataContext(DbContextOptions<DataContext> options, IConfiguration configuration) : base(options)
    {
        _options = options;
        _conf = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_conf.GetConnectionString("DefaultConnection"));
    }

    // seed the db with same tecnichians
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Job>().HasOne<Tecnichian>();
        modelBuilder.Entity<Job>().HasOne<Request>();

        modelBuilder.Entity<Tecnichian>().HasData(
            new Tecnichian()
            {
                Id = Guid.NewGuid(),
                Name = "Mario",
                Lastname = "Rossi",
                ServiceSpecialization = Service.Plumbing
            },
            new Tecnichian()
            {
                Id = Guid.NewGuid(),
                Name = "Luigi",
                Lastname = "Rossi",
                ServiceSpecialization = Service.Electrician
            });

        modelBuilder.Entity<Request>().HasData(
            new Request()
            {
                Description = "Fix the sink",
                Street = "Via Roma 1",
                City = "Roma",
                ServiceType = new List<Service> { Service.Plumbing },
                RequestStatus = RequestStatus.NotAssigned
            },
            new Request()
            {
                Description = "Fix the light",
                Street = "Via Roma 2",
                City = "Roma",
                ServiceType = new List<Service> { Service.Electrician },
                RequestStatus = RequestStatus.NotAssigned
            },
            new Request()
            {
                Description = "Fix the toilette",
                Street = "Via Ligurai 26",
                City = "Prato",
                ServiceType = new List<Service> { Service.Plumbing },
                RequestStatus = RequestStatus.NotAssigned
            }
        );
    }


    public DbSet<Request> Requests { get; set; }
    public DbSet<Tecnichian> Tecnichians { get; set; }
    public DbSet<Job> Jobs { get; set; }
}