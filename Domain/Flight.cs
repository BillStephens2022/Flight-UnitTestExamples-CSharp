namespace Domain
{
    public class Flight
    {
        // note: with no access modifier explicitly stated, this is effectively private
        // The 2 lines below essentially ensure that the Booking List is read-only
        List<Booking> bookingList = new();
        public IEnumerable<Booking> BookingList => bookingList;
        public int RemainingNumberOfSeats { get; set; }
        public Guid Id { get; }

        public Flight(int seatCapacity)
        {
            RemainingNumberOfSeats = seatCapacity;
        }

        public object? Book(string passengerEmail, int numberOfSeats)
        {
            if (numberOfSeats > this.RemainingNumberOfSeats)
                return new OverbookingError();
            RemainingNumberOfSeats -= numberOfSeats;
            bookingList.Add(new Booking(passengerEmail, numberOfSeats));
            return null;        
        }

        public object? CancelBooking(string passengerEmail, int numberOfSeats)
        {
            if (!bookingList.Any(booking => booking.Email == passengerEmail))
                return new BookingNotFoundError();

            RemainingNumberOfSeats += numberOfSeats;

            return null;
        }
    }
}
