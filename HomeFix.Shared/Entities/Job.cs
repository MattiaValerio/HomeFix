using System.ComponentModel.DataAnnotations;
using HomeFix.Shared.Enums;

namespace HomeFix.Shared.Entities;

public class Job
{
    [Key]
    public Guid Id { get; set; }
    public Request Request { get; set; }
    public DateTime Appointment { get; set; }
    public Tecnichian Tecnic { get; set; }
    public JobStatus JobStatus { get; set; }

    public Job(Request request, Tecnichian tecnichian, DateTime appointment)
    {
        Id = new Guid();
        Request = request;
        Appointment = appointment;
        Tecnic = tecnichian;
        JobStatus = JobStatus.NotAssigned;
    }

    public Job()
    {
        Id = new Guid();
    }
}