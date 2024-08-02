using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UcareApp.Models;
using MediatR;
using UcareApp.Queries;
using UcareApp.Commands;

namespace UcareApp.Presentation.Controllers;

public class PlaceController : Controller
{

    private readonly IMediator mediator;
    public PlaceController(IMediator mediator)
    {
        this.mediator = mediator;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var getAllPlacesQuery = new GetAllPlacesQuery();
        var places = await this.mediator.Send(getAllPlacesQuery);

        return View(places);
    }

    [HttpGet("[controller]/[action]")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var getPlaceByIdQuery = new GetPlaceByIdQuery(id);
        var place = await this.mediator.Send(getPlaceByIdQuery);

        return base.View("OnePlace", place);
    }

    [Authorize(Roles = "admin")]
    [HttpGet("[action]", Name = "CreatePlace")]
    public IActionResult Create()
    {
        return base.View();
    }

    [Authorize(Roles = "admin")]
    [HttpPost("[controller]/[action]")]
    public async Task<IActionResult> Create(Place newPlace)
    {
        var createPlaceCommand = new CreatePlaceCommand(newPlace);
        var isCreated = await this.mediator.Send(createPlaceCommand);

        if (isCreated)
        {
            return base.RedirectToAction("Index");
        }
        else
        {
            return base.BadRequest();
        }
    }

    [Authorize(Roles = "admin")]
    [HttpGet("[action]/{id}", Name = "UpdatePlace")]
    public async Task<IActionResult> Update(Guid id)
    {
        var getPlaceByIdQuery = new GetPlaceByIdQuery(id);
        var place = await this.mediator.Send(getPlaceByIdQuery);

        return base.View(place);
    }

    [Authorize(Roles = "admin")]
    [HttpGet("api/[controller]/[action]", Name = "UpdatePlaceApi")]
    public async Task<IActionResult> Update(Place? place)
    {
        var updatePlaceCommand = new UpdatePlaceCommand(place);
        var isUpdated = await mediator.Send(updatePlaceCommand);

        if (isUpdated)
        {
            return base.RedirectToAction("index");
        }
        else
        {
            return base.BadRequest();
        }
    }
    
    [Authorize(Roles = "admin")]
    [HttpGet("api/[controller]/[action]", Name = "DeletePlaceApi")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var deletePlaceCommand = new DeletePlaceCommand(id);
        var isDeleted = await mediator.Send(deletePlaceCommand);

        if (isDeleted)
        {
            return base.RedirectToAction("index");
        }
        else
        {
            return base.BadRequest();
        }
    }
}
