namespace UcareApp.Handlers;

using MediatR;
using System.Threading.Tasks;
using System.Threading;
using UcareApp.Queries;
using UcareApp.Models;
using UcareApp.Repositories.Base;

public class GetAllPlacesHandler : IRequestHandler<GetAllPlacesQuery, IEnumerable<Place>>
{
    private readonly IPlaceRepository placeRepository;

    public GetAllPlacesHandler(IPlaceRepository placeRepository)
    {
        this.placeRepository = placeRepository;
    }

    public async Task<IEnumerable<Place>> Handle(GetAllPlacesQuery request, CancellationToken cancellationToken)
    {
        return await this.placeRepository.GetAllAsync();
    }
}