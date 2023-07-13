using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MySpot.Api.Services;
using MySpot.Api.Commands;
using Shouldly;
using MySpot.Api.ValueObjects;
using MySpot.Api.Enitites;

namespace MySpot.Api.Tests.Services
{
    public class ReservationServiceTests
    {
        #region Arrange
        private static readonly Clock _clock = new Clock();

        private readonly ReservationsService _reservationService;

        private readonly List<WeeklyParkingSpot> _weeklyParkingSpots;

        public ReservationServiceTests()
        {
            _weeklyParkingSpots = new()
            {
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000001") , new Week(_clock.Current()), "P1"),
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000002") , new Week(_clock.Current()), "P2"),
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000003") , new Week(_clock.Current()), "P3"),
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000004") , new Week(_clock.Current()), "P4"),
                new WeeklyParkingSpot(Guid.Parse("00000000-0000-0000-0000-000000000005") , new Week(_clock.Current()), "P5")
            };
            _reservationService = new ReservationsService(_weeklyParkingSpots);
        }
        #endregion

        [Fact]
        public void given_rservation_for_not_taken_date_create_reservation_should_succeed()
        {
            //Arrange
            var parkingSpot = _weeklyParkingSpots.First(); 
            var command = new CreateReservation(parkingSpot.Id, Guid.NewGuid(), DateTime.UtcNow.AddMinutes(5), "John Doe", "RMI 123");

            //ACT

            var reservationId = (ReservationId)_reservationService.Create(command);

            //Assert
            reservationId.ShouldNotBeNull();

            //Assert.Equal(reservationId, command.ReservationId);
            reservationId.ShouldBe(command.ReservationId);


        }
    }
}
