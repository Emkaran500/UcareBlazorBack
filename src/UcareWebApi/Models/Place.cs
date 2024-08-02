namespace UcareApp.Models;

using UcareApp.Enums;
using UcareApp.Models;
public class Place {
    
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Adress { get; set; }
    public string? Longitude { get; set; }
    public string? Latitude { get; set; }
    public ServiceType ServiceType { get; set; }
    public WeekDayEnum[] WorkingDays { get; set; } = [];
    public string? PhotoUrl { get; set; }
    public double Rating { get; set; }
}