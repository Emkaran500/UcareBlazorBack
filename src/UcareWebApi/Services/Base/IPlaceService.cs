namespace UcareApp.Services.Base;

using UcareApp.Models;

public interface IPlaceService
{
    Task<IEnumerable<Place>> GetAllPlacesAsync();
    Task<Place> GetPlaceByIdAsync(Guid? id);
    Task CreateNewPlaceAsync(Place? newPlace);
    Task DeletePlaceAsync(Guid id);
    Task UpdatePlaceAsync(Place? place);

}