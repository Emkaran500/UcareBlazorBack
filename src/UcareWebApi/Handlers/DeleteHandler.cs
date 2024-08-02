namespace UcareApp.Handlers;

using UcareApp.Commands;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using UcareApp.Repositories.Base;

public class DeleteHandler : IRequestHandler<DeletePlaceCommand, bool>
{
    private readonly IPlaceRepository placeRepository;

    public DeleteHandler(IPlaceRepository placeRepository)
    {
        this.placeRepository = placeRepository;
    }

    public async Task<bool> Handle(DeletePlaceCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
        {
            throw new ArgumentNullException("Id is empty!");
        }

        await placeRepository.DeleteAsync(request.Id);

        return true;
    }
}