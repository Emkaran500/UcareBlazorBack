namespace UcareApp.Commands;

using MediatR;
using UcareApp.Models;

public class UpdatePlaceCommand : IRequest<bool>
{
    public Place? Place { get; set; }

    public UpdatePlaceCommand(Place? place)
    {
        this.Place = place;
    }
}