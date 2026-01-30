using Application.Reservations;
using Application.Reservations.CreateReservation.Command;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IReservationRepository _repo;

    public ReservationsController(IMediator mediator, IReservationRepository repo)
    {
        _mediator = mediator;
        _repo = repo;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> Create(
        [FromBody] CreateReservationCommand command,
        CancellationToken ct)
    {
        var id = await _mediator.Send(command, ct);
        return Ok(id);
    }

    [HttpGet]
    public async Task<IActionResult> List(CancellationToken ct)
    {
        var all = await _repo.ListAsync(ct);
        return Ok(all);
    }
}
