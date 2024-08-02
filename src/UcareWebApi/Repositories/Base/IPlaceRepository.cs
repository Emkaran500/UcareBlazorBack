namespace UcareApp.Repositories.Base;

using UcareApp.Models;
public interface IPlaceRepository
{
    Task<IEnumerable<Place>> GetAllAsync();
    Task<Place> GetByIdAsync(Guid? id);
    Task CreateAsync(Place? newPlace);
    Task DeleteAsync(Guid? id);
    Task<long> UpdateAsync(Place? place);


}