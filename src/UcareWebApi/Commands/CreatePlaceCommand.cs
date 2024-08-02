namespace UcareApp.Commands;

using MediatR;
using UcareApp.Models;

public class CreatePlaceCommand : IRequest<bool>
{
    public Place? Place { get; set; }

    public CreatePlaceCommand(Place? place)
    {
        this.Place = place;
    }
}