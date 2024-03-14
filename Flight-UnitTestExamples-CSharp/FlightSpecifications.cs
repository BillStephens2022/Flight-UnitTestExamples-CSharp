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

        [Fact]
        public void Avoids_overbooking()
        {
            // Given
            var flight = new Flight(seatCapacity: 3);
            // When
            var error = flight.Book("passenger@gmail.com", 4);

            // Then
            error.Should().BeOfType<OverbookingError>();
        }

        [Fact]
        public void Books_flights_successfully()
        {
            var flight = new Flight(seatCapacity: 3);

            var error = flight.Book("passenger@gmail.com", 1);

            error.Should().BeNull();

        }

        [Fact]
        public void Booking_reduces_the_number_of_seats_2()
        {
            var flight = new Flight(seatCapacity: 6);

            flight.Book("bill@gmail.com", 3);

            flight.RemainingNumberOfSeats.Should().Be(3);
        }

        [Fact]
        public void Booking_reduces_the_number_of_seats_3()
        {
            var flight = new Flight(seatCapacity: 10);

            flight.Book("bill@gmail.com", 2);

            flight.RemainingNumberOfSeats.Should().Be(8);
        }

        [Fact]
        public void Booking_reduces_the_number_of_seats_4()
        {
            var flight = new Flight(seatCapacity: 5);

            flight.Book("bill@gmail.com", 1);

            flight.RemainingNumberOfSeats.Should().Be(4);
        }

    }
}