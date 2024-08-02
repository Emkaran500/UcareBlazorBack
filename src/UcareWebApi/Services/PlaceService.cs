namespace UcareApp.Services;

using UcareApp.Repositories.Base;
using UcareApp.Services.Base;
using UcareApp.Models;
using System.Threading.Tasks;
using System.Collections.Generic;


public class PlaceService :  IPlaceService
{
    private readonly IPlaceRepository placeRepository; 
    public PlaceService(IPlaceRepository placeRepository)
    {
        this.placeRepository = placeRepository;
    }    
    public async Task<IEnumerable<Place>> GetAllPlacesAsync(){
        return await this.placeRepository.GetAllAsync();
    }

    public async Task<Place> GetPlaceByIdAsync(Guid? id){
        ArgumentNullException.ThrowIfNull(id);
        
        return await this.placeRepository.GetByIdAsync(id.Value);
    }

    public async Task CreateNewPlaceAsync(Place? newPlace)
    {
        ArgumentNullException.ThrowIfNull(newPlace);

        await this.placeRepository.CreateAsync(newPlace);
    }

    public async Task DeletePlaceAsync(Guid id)
    {
        ArgumentNullException.ThrowIfNull(id);

        await this.placeRepository.DeleteAsync(id);
    }

    public async Task UpdatePlaceAsync(Place? place)
    {
        ArgumentNullException.ThrowIfNull(place);

        await this.placeRepository.UpdateAsync(place);
    }
}