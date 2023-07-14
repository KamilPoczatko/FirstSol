using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using MySpot.Application.Services;
using MySpot.Application.Commands;
using Shouldly;
using MySpot.Core.ValueObjects;
using MySpot.Core.Enitites;
using MySpot.Api.Tests.Shared;
using MySpot.Infractructure.Repositories;
using MySpot.Core.Repositories;

namespace MySpot.Api.Tests.Services
{
    public class ReservationServiceTests
    {
        #region Arrange
        private readonly IClock _clock = new TestClock();

        private readonly IReservationsService _reservationService;
        
        private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;

        public ReservationServiceTests()
        {
            _weeklyParkingSpotRepository = new InMemoryWeeklyParkingSpotRepository(_clock); 
            _reservationService = new ReservationsService(_clock, _weeklyParkingSpotRepository);
        }
        #endregion

        [Fact]
        public void given_rservation_for_not_taken_date_create_reservation_should_succeed()
        {
            //Arrange
            var parkingSpot = _weeklyParkingSpotRepository.GetAll().First(); 
            var command = new CreateReservation(parkingSpot.Id, Guid.NewGuid(), _clock.Current(), "John Doe", "RMI 123");

            //ACT

            var reservationId = (ReservationId)_reservationService.Create(command);

            //Assert
            reservationId.ShouldNotBeNull();

            //Assert.Equal(reservationId, command.ReservationId);
            reservationId.ShouldBe(command.ReservationId);


        }
    }
}
