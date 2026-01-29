using Application.Reservations.CreateReservation;
using Application.Reservations;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly CreateReservationHandler _create;
    private readonly IReservationRepository _repo;

    public ReservationsController(CreateReservationHandler create, IReservationRepository repo)
    {
        _create = create;
        _repo = repo;
    }

    [HttpPost]
    public async Task<ActionResult<CreateReservationResponse>> Create(
        [FromBody] CreateReservationRequest request,
        CancellationToken ct)
    {
        var result = await _create.HandleAsync(request, ct);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> List(CancellationToken ct)
    {
        var all = await _repo.ListAsync(ct);
        return Ok(all);
    }
}
