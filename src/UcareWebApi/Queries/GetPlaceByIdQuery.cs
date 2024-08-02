using MediatR;
using UcareApp.Models;

namespace UcareApp.Queries;

public class GetPlaceByIdQuery : IRequest<Place>
{
    public Guid Id { get; set; }

    public GetPlaceByIdQuery(Guid id)
    {
        this.Id = id;
    }
}