namespace UcareApp.Commands;

using MediatR;

public class DeletePlaceCommand : IRequest<bool>
{
    public Guid Id { get; set; }

    public DeletePlaceCommand(Guid id)
    {
        this.Id = id;
    }
}