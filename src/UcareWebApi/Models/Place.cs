namespace UcareApp.Models;

using UcareApp.Enums;
using UcareApp.Models;
public class Place {
    
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Adress { get; set; }
    public double Longitude { get; set; }
    public double Latitude { get; set; }
    public ServiceType ServiceType { get; set; }
}