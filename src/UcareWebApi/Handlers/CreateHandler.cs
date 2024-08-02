namespace UcareApp.Handlers;

using UcareApp.Commands;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using UcareApp.Repositories.Base;

public class CreateHandler : IRequestHandler<CreatePlaceCommand, bool>
{
    private readonly IPlaceRepository placeRepository;

    public CreateHandler(IPlaceRepository placeRepository)
    {
        this.placeRepository = placeRepository;
    }

    public async Task<bool> Handle(CreatePlaceCommand request, CancellationToken cancellationToken)
    {
        if (request.Place == null)
        {
            throw new ArgumentNullException("Place is empty!");
        }
        if (request.Place.Name == null)
        {
            throw new ArgumentNullException("Name is empty!");
        }
        if (request.Place.Name.Length < 1)
        {
            throw new ArgumentException("Name cannot be less than 1 letter!");
        }
        if (double.TryParse(request.Place.Latitude, out var latitudeValue))
        {
            if (latitudeValue < -90 || latitudeValue > 90)
            {
                throw new ArgumentException("Latitude cannot be less than -90 or more than 90 degrees!");
            }
        }
        else
        {
            throw new ArgumentException("Latitude cannot non numerical!");
        }
        if (double.TryParse(request.Place.Longitude, out var longitudeValue))
        {
            if (longitudeValue < -180 || longitudeValue > 180)
            {
                throw new ArgumentException("Longitude cannot be less than -180 or more than 180 degrees!");
            }
        }
        else
        {
            throw new ArgumentException("Longitude cannot non numerical!");
        }
        if (request.Place.WorkingDays.Count() <= 0)
        {
            throw new ArgumentException("Must be at least one working day!");
        }
        if (request.Place.ServiceType <= Enums.ServiceType.Hair_Salon || request.Place.ServiceType >= Enums.ServiceType.Wellness_and_Day_Spa)
        {
            throw new ArgumentException("Must be at least one type of maintenance!");
        }

        await this.placeRepository.CreateAsync(request.Place);

        return true;
    }
}