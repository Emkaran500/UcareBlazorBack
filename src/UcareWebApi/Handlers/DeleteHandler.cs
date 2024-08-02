namespace UcareApp.Handlers;

using UcareApp.Commands;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using UcareApp.Services.Base;

public class DeleteHandler : IRequestHandler<DeletePlaceCommand, bool>
{
    private readonly IPlaceService placeService;

    public DeleteHandler(IPlaceService placeService)
    {
        this.placeService = placeService;
    }

    public async Task<bool> Handle(DeletePlaceCommand request, CancellationToken cancellationToken)
    {
        if (request.Id == Guid.Empty)
        {
            throw new ArgumentNullException("Id is empty!");
        }

        await placeService.DeletePlaceAsync(request.Id);

        return true;
    }
}