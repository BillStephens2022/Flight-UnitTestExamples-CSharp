using FluentAssertions;
using System.Security.Cryptography;
using Data;
using Domain;

namespace Application.Tests
{
    public class FlightApplicationSpecifications
    {
        [Fact]
        public void Books_flights()
        {
            var entities = new Entities();
            var flight = new Flight(3);
            entities.Flights.Add(flight);

            var bookingService = new BookingService(entities: entities);

            bookingService.Book(new BookDto(
                flightId: flight.Id,
                passengerEmail: "bob@email.com",
                numberOfSeats: 2
                ));
            bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(
                new BookingRm(passengerEmail: "bob@email.com", numberOfSeats: 2)
                );
        }
    }

    public class BookingService
    {

        public BookingService(Entities entities)
        {
            
        }
        public void Book(BookDto bookDto)
        {

        }

        public IEnumerable<BookingRm> FindBookings(Guid flightId)
        {
            return new[]
            {
                new BookingRm( "bob@email.com", numberOfSeats: 2)
            };
        }
    }

    public class BookDto
    {
        public BookDto(Guid flightId, string passengerEmail, int numberOfSeats)
        {
            
        }
    }

    public class BookingRm
    {
        public string PassengerEmail { get; set; }
        public int NumberOfSeats { get; set; }  

        public BookingRm(string passengerEmail, int numberOfSeats)
        {
            PassengerEmail = passengerEmail;
            NumberOfSeats = numberOfSeats;
        }
    }
}