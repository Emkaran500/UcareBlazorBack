namespace UcareApp.Handlers;

using MediatR;
using System.Threading.Tasks;
using System.Threading;
using UcareApp.Queries;
using UcareApp.Models;
using UcareApp.Repositories.Base;

public class GetPlaceByIdHandler : IRequestHandler<GetPlaceByIdQuery, Place>
{
    private readonly IPlaceRepository placeRepository;

    public GetPlaceByIdHandler(IPlaceRepository placeRepository)
    {
        this.placeRepository = placeRepository;
    }

    public async Task<Place> Handle(GetPlaceByIdQuery request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
        {
            throw new ArgumentNullException("Id is empty!");
        }

        return await placeRepository.GetByIdAsync(request.Id);
    }
}