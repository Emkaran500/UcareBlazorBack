using MediatR;
using UcareApp.Models;

namespace UcareApp.Queries;

public class GetAllPlacesQuery : IRequest<IEnumerable<Place>> {}