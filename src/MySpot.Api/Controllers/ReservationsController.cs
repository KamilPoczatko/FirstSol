using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;
using MySpot.Api.Services;

namespace MySpot.Api.AddControllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController: ControllerBase
{
    private readonly ReservationsService _service = new();
   

    [HttpGet("get")]
    public ActionResult<IEnumerable<Reservation>> Get() => Ok(_service.GetAll());

    [HttpGet("{id:int}")]
    public ActionResult<Reservation> Get(int id)
    {
        var reservation = _service.Get(id);


        if (reservation is null)
        {
            NotFound();
        }
        return Ok(reservation);
    }
    


    [HttpPost]
    public ActionResult Post([FromBody]Reservation reservation)
    {
        int? IdCreated = _service.Create(reservation);

        if (IdCreated is null)
        {
            BadRequest();
        }
        return CreatedAtAction(nameof(Get), new {id = IdCreated}, null);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Reservation reservation)
    {
        if (!_service.Update(id, reservation))
        {
            return NotFound();
        }
        
        return NoContent();

    }
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        if (!_service.Delete(id))
        {
            return NotFound();
        }

        return NoContent();

    }

}
