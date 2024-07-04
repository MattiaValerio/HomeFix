using System.ComponentModel.DataAnnotations;
using HomeFix.Shared.Enums;

namespace HomeFix.Shared.Entities;

public class Request
{
    [Key]
    public Guid Id { get; set; }
    public string Description { get; set; }
    public string Street { get; set; }
    public string City { get; set; }
    public List<Service> ServiceType { get; set; }
    public RequestStatus RequestStatus { get; set; }

    public Request()
    {
        Id = Guid.NewGuid();
        RequestStatus = RequestStatus.NotAssigned;
    }

    public Request(string desc, string street, string city, Service service)
    {
        Id = Guid.NewGuid();
        Description = desc;
        Street = street;
        City  = city;
        ServiceType = new List<Service>{service};
        RequestStatus = RequestStatus.NotAssigned;
    }

    public Request(string desc, string street, string city, List<Service> services)
    {
        Id = Guid.NewGuid();
        Description = desc;
        Street = street;
        City  = city;
        ServiceType = services;
        RequestStatus = RequestStatus.NotAssigned;
    }
    
    public Job AssignJob(Tecnichian tec, DateTime timeApp)
    {
        
        return new Job(this, tec, timeApp);
    }
}