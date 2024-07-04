using System.ComponentModel.DataAnnotations;
using HomeFix.Shared.Enums;

namespace HomeFix.Shared.Entities;

public class Tecnichian()
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } 
    public string Lastname { get; set; } 
    public Service ServiceSpecialization { get; set; }
    private List<DateTime> Appointments { get; set; } = new();
    
    public bool isAvailable(DateTime time)
    {
        return !Appointments.Contains(time.Date);
    }
    
    
}