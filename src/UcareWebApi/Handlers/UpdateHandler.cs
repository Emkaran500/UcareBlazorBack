namespace UcareApp.Handlers;

using UcareApp.Commands;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using UcareApp.Services.Base;

public class UpdateHandler : IRequestHandler<UpdatePlaceCommand, bool>
{
    private readonly IPlaceService placeService;

    public UpdateHandler(IPlaceService placeService)
    {
        this.placeService = placeService;
    }

    public async Task<bool> Handle(UpdatePlaceCommand request, CancellationToken cancellationToken)
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
        if (request.Place.Latitude < -90 || request.Place.Latitude > 90)
        {
            throw new ArgumentException("Latitude cannot be less than -90 or more than 90 degrees!");
        }
        if (request.Place.Longitude < -180 || request.Place.Longitude > 180)
        {
            throw new ArgumentException("Longitude cannot be less than -180 or more than 180 degrees!");
        }
        if (/*request.Place.WorkingDays.Count() <= 0*/ false)
        {
            throw new ArgumentException("Must be at least one working day!");
        }
        if (/*request.Place.Maintenances.Count() <= 0*/ false)
        {
            throw new ArgumentException("Must be at least one type of maintenance!");
        }

        await this.placeService.UpdatePlaceAsync(request.Place);

        return true;
    }
}