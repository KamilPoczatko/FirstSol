using Microsoft.AspNetCore.Mvc;
using MySpot.Application.Commands;
using MySpot.Core.Enitites;
using MySpot.Application.Services;
using MySpot.Core.ValueObjects;

namespace MySpot.Api.AddControllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController: ControllerBase
{
    private readonly IReservationsService _reservationsService;
    public ReservationsController(IReservationsService reservationsService)
    {
        _reservationsService = reservationsService;
    }
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Reservation>>> Get()
    {
        var result = await _reservationsService.GetAllWeeklyAsync();
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Reservation>> Get(Guid id)
    {
        var reservation = await _reservationsService.GetAsync(id);


        if (reservation is null)
        {
            NotFound();
        }
        return Ok(reservation);
    }
    


    [HttpPost]
    public async Task<ActionResult> Post([FromBody]CreateReservation command)
    {
        var IdCreated = await _reservationsService.CreateAsync(command with { ReservationId = Guid.NewGuid() });

        if (IdCreated is null)
        {
            BadRequest();
        }
        return CreatedAtAction(nameof(Get), new {id = IdCreated}, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> Put(Guid id, ChangeReservationLicensePlate command)
    {
        if (!await _reservationsService.UpdateAsync(command with { ReservationId = id}))
        {
            return NotFound();
        }
        
        return NoContent();

    }
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        if (! await _reservationsService.DeleteAsync(new DeleteReservation(id)))
        {
            return NotFound();
        }

        return NoContent();

    }

}
