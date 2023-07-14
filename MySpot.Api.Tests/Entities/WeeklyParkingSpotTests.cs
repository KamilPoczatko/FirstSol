using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MySpot.Core.Enitites;
using MySpot.Core.ValueObjects;
using MySpot.Core.Exceptions;
using Shouldly;

namespace MySpot.Api.Tests.Entities
{
    public class WeeklyParkingSpotTests
    {
        #region Arrange

        private readonly Date _now;
        private readonly WeeklyParkingSpot _weeklyParkingSpot;
        public WeeklyParkingSpotTests()
        {

            _now = new Date(new DateTime(2022, 08, 10));
            _weeklyParkingSpot = new WeeklyParkingSpot(Guid.NewGuid(), new Week(_now), "P1");
        }
        #endregion

        //[Fact]
        [Theory]
        [InlineData("2023-07-11")]
        [InlineData("2023-07-21")]
        [InlineData("2023-05-11")]
        [InlineData("2023-08-30")]
        public void given_invalid_date_add_reservation_should_fail(string dateString)
        {
            // Arrange
            
            var invalidDate = DateTime.Parse(dateString);            
            var reservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id,"Romana Zupa", "RMI XYZ", new Date(invalidDate));

            // ACT
            var exception = Record.Exception(()=> _weeklyParkingSpot.AddReservation(reservation, _now));
            // Assert

            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<InvalidReservationDateException>();

            //Assert.NotNull(exception);
            //Assert.IsType<InvalidReservationDateException>(exception);
        }

        [Fact]
        public void given_reservation_for_already_existing_date_add_reservation_should_fall()
        {
            //Arrange
            var reservationDate = _now.AddDay(1);
            var reservation     = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "Romana Zupa", "RMI XYZ", reservationDate);
            var nextReservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "Romana Zupa", "RMI XYZ", reservationDate);
            _weeklyParkingSpot.AddReservation(reservation, reservationDate);

            //ACT
            var exception = Record.Exception(() => _weeklyParkingSpot.AddReservation(nextReservation, reservationDate));

            //Assert
            exception.ShouldNotBeNull();
            exception.ShouldBeOfType<ParkingSpotAlreadyReservatedException>();

        }

        [Fact]
        public void given_rservation_for_not_taken_date_add_reservation_should_succeed()
        {
            //Arrange
            var reservationDate = _now.AddDay(1);
            var rservation = new Reservation(Guid.NewGuid(), _weeklyParkingSpot.Id, "Romana Zupa", "RMI XYZ", reservationDate);

            //ACT
            _weeklyParkingSpot.AddReservation(rservation, _now);

            //Assert
            _weeklyParkingSpot.Reservations.ShouldHaveSingleItem();

        }
    }
}
