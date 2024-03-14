using Domain;
using FluentAssertions;

namespace Flight_UnitTestExamples_CSharp
{
    public class FlightSpecifications
    {
        [Fact]
        public void Booking_reduces_the_number_of_seats()
        {
            var flight = new Flight(seatCapacity: 3);

            flight.Book("bill@gmail.com", 1);

            flight.RemainingNumberOfSeats.Should().Be(2);   
        }
    }
}