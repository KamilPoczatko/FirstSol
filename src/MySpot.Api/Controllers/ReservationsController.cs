using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.Enitites;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.AddControllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController: ControllerBase
{
    private static readonly Clock Clock = new Clock();
    private static readonly ReservationsService _service = new(new List<WeeklyParkingSpot>() {
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(Clock.Current()), "P1"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(Clock.Current()), "P2"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(Clock.Current()), "P3"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(Clock.Current()), "P4"),
            new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(Clock.Current()), "P5")});
   

    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> Get() => Ok(_service.GetAllWeekly());

    [HttpGet("{id:guid}")]
    public ActionResult<Reservation> Get(Guid id)
    {
        var reservation = _service.Get(id);


        if (reservation is null)
        {
            NotFound();
        }
        return Ok(reservation);
    }
    


    [HttpPost]
    public ActionResult Post([FromBody]CreateReservation command)
    {
        var IdCreated = _service.Create(command with { ReservationId = Guid.NewGuid() });

        if (IdCreated is null)
        {
            BadRequest();
        }
        return CreatedAtAction(nameof(Get), new {id = IdCreated}, null);
    }

    [HttpPut("{id:guid}")]
    public ActionResult Put(Guid id, ChangeReservationLicensePlate command)
    {
        if (!_service.Update(command with { ReservationId = id}))
        {
            return NotFound();
        }
        
        return NoContent();

    }
    [HttpDelete("{id:guid}")]
    public ActionResult Delete(Guid id)
    {
        if (!_service.Delete(new DeleteReservation(id)))
        {
            return NotFound();
        }

        return NoContent();

    }

}
