namespace UcareApp.Handlers;

using MediatR;
using System.Threading.Tasks;
using System.Threading;
using UcareApp.Queries;
using UcareApp.Models;
using UcareApp.Services.Base;

public class GetPlaceByIdHandler : IRequestHandler<GetPlaceByIdQuery, Place>
{
    private readonly IPlaceService placeService;

    public GetPlaceByIdHandler(IPlaceService placeService)
    {
        this.placeService = placeService;
    }

    public async Task<Place> Handle(GetPlaceByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
        {
            throw new ArgumentNullException("Id is empty!");
        }

        return await placeService.GetPlaceByIdAsync(request.Id);
    }
}