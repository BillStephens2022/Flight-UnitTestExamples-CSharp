using Domain;
using FluentAssertions;

namespace Flight_UnitTestExamples_CSharp
{
    public class FlightSpecifications
    {
        // parameterized test - Inline data takes values for the parameters,
        // i.e. seatCapacity is 3, numberOfSeats is 1, and remainingNumberOfSeats is 2.
        [Theory]
        [InlineData(3, 1, 2)]  // inserts values for the parameters in method below - 1st test
        [InlineData(10, 6, 4)]  // inserts values for the parameters in method below - 2nd test
        [InlineData(7, 2, 5)]  // inserts values for the parameters in method below - 3rd test
        public void Booking_reduces_the_number_of_seats(int seatCapacity, int numberOfSeats, int remainingNumberOfSeats)
        {
            var flight = new Flight(seatCapacity: seatCapacity);

            flight.Book("bill@gmail.com", numberOfSeats);

            flight.RemainingNumberOfSeats.Should().Be(remainingNumberOfSeats);   
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