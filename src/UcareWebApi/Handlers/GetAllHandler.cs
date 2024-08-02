namespace UcareApp.Handlers;

using MediatR;
using System.Threading.Tasks;
using System.Threading;
using UcareApp.Queries;
using UcareApp.Models;
using UcareApp.Services.Base;

public class GetAllPlacesHandler : IRequestHandler<GetAllPlacesQuery, IEnumerable<Place>>
{
    private readonly IPlaceService placeService;

    public GetAllPlacesHandler(IPlaceService placeService)
    {
        this.placeService = placeService;
    }

    public async Task<IEnumerable<Place>> Handle(GetAllPlacesQuery request, CancellationToken cancellationToken)
    {
        return await this.placeService.GetAllPlacesAsync();
    }
}