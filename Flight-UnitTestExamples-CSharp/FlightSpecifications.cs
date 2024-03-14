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
        public void Remembers_bookings()
        {
            var flight = new Flight(seatCapacity: 150);
            flight.Book(passengerEmail: "a@b.com", numberOfSeats: 4);
            flight.BookingList.Should().ContainEquivalentOf(new Booking("a@b.com", 4));
        }

        [Theory]
        [InlineData(3, 1, 1, 3)]
        [InlineData(4, 2, 2, 4)]
        [InlineData(7, 5, 4, 6)]
        public void Canceling_booking_frees_up_the_seats(
            int initialCapacity,
            int numberOfSeatsToBook,
            int numberOfSeatsToCancel,
            int remainingNumberOfSeats
            )
        {
            // given
            var flight = new Flight(initialCapacity);
            flight.Book(passengerEmail: "bob@email.com", numberOfSeats: numberOfSeatsToBook);

            // when
            flight.CancelBooking(passengerEmail: "bob@email.com", numberOfSeats: numberOfSeatsToCancel);

            // then
            flight.RemainingNumberOfSeats.Should().Be(remainingNumberOfSeats);
        }

        [Fact]
        public void Doesnt_cancel_bookings_for_passengers_who_have_not_booked()
        {
            var flight = new Flight(3);
            var error = flight.CancelBooking(passengerEmail: "bob@email.com", 2);
            error.Should().BeOfType<BookingNotFoundError>();
        }

        [Fact]
        public void Returns_null_when_successfully_cancels_a_booking()
        {
            var flight = new Flight(3);
            flight.Book(passengerEmail: "bob@email.com", numberOfSeats: 1);
            var error = flight.CancelBooking(passengerEmail: "bob@email.com", 1);
            error.Should().BeNull();
        }

    }
}