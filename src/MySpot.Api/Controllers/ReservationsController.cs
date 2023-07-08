using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Models;
namespace MySpot.Api.AddControllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController: ControllerBase
{
    private int _id = 1;
    static private readonly List<Reservation> reservations = new()
    {
        new Reservation{ Date = DateTime.UtcNow.AddDays(1).Date, Id = 0, EmployeeName="Roman", LicensePlate ="RMI123", ParkingSpotName = "P1" }
    };

    static private readonly List<string> _parkingSpotNames = new()
    {
        "P1", "P2", "P3", "P4", "P5"
    };

    [HttpGet("get")]
    public ActionResult<IEnumerable<Reservation>> Get() => Ok(reservations);

    [HttpGet("{id:int}")]
    public ActionResult<Reservation> Get(int id)
    {
        var reservation = reservations.SingleOrDefault(x => x.Id == id);
        
        if (reservation is null)
        {
            NotFound();
        }
        return Ok(reservation);
    }
    


    [HttpPost]
    public ActionResult Post([FromBody]Reservation reservation)
    {
        if(_parkingSpotNames.All(x => x != reservation.ParkingSpotName))
        {
            return BadRequest();
        }
        reservation.Date = DateTime.UtcNow.AddDays(1).Date;

        var reservationAlreadyExists = reservations.Any(x => x.ParkingSpotName == reservation.ParkingSpotName
                                                        && x.Date.Date == reservation.Date.Date);

        if(reservationAlreadyExists)
        {
            return BadRequest();
        }

        reservation.Id = _id;
        _id++;
        reservations.Add(reservation);
        return CreatedAtAction(nameof(Get), new {id = reservation.Id}, null);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Reservation reservation)
    {
        var existingReservation = reservations.SingleOrDefault(x => x.Id == id);

        if (existingReservation is null)
        {
            return NotFound();
        }

        existingReservation.LicensePlate = reservation.LicensePlate; 
        
        return NoContent();

    }
    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var existingReservation = reservations.SingleOrDefault(x => x.Id == id);

        if (existingReservation is null)
        {
            return NotFound();
        }

        reservations.Remove(existingReservation);
        return NoContent();

    }

}
