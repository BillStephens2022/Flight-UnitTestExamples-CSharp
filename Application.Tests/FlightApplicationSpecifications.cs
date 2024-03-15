using FluentAssertions;
using System.Security.Cryptography;
using Data;
using Domain;
using Microsoft.EntityFrameworkCore;
using Application.Tests;

namespace Application.Tests
{
    public class FlightApplicationSpecifications
    {
        [Theory]
        [InlineData("m@m.com", 2)]
        [InlineData("a@a.com", 2)]
        public void Books_flights(string passengerEmail, int numberOfSeats)
        {
            var entities = new Entities(new DbContextOptionsBuilder<Entities>()
                .UseInMemoryDatabase("Flights")
                .Options);

            var flight = new Flight(3);
            entities.Flights.Add(flight);

            var bookingService = new BookingService(entities: entities);

            bookingService.Book(new BookDto(
                flightId: flight.Id,
                passengerEmail: passengerEmail,
                numberOfSeats: numberOfSeats
                ));
            bookingService.FindBookings(flight.Id).Should().ContainEquivalentOf(
                new BookingRm(passengerEmail, numberOfSeats)
                );
        }
    }
}

namespace Application
{
    public class BookingService
    {
        public Entities Entities { get; set; }
        public BookingService(Entities entities)
        {
            Entities = entities;
        }
        public void Book(BookDto bookDto)
        {
            var flight = Entities.Flights.Find(bookDto.FlightId);
            flight.Book(bookDto.PassengerEmail, bookDto.NumberOfSeats);
            Entities.SaveChanges();
        }

        public IEnumerable<BookingRm> FindBookings(Guid flightId)
        {
            return Entities.Flights
                 .Find(flightId)
                 .BookingList
                 .Select(booking => new BookingRm(
                     booking.Email,
                     booking.NumberOfSeats
                     ));
        }
    }
}

namespace Application.Tests
{
  

    
}