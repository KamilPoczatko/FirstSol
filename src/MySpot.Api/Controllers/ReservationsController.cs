using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.Enitites;
using MySpot.Api.Services;

namespace MySpot.Api.AddControllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController: ControllerBase
{
    private readonly ReservationsService _service = new();
   

    [HttpGet("get")]
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
