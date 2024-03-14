namespace Domain
{
    public class Flight
    {
        // note: with no access modifier explicitly stated, this is effectively private
        // The 2 lines below essentially ensure that the Booking List is read-only
        List<Booking> bookingList = new();
        public IEnumerable<Booking> BookingList => bookingList;
        public List<Booking> BookingList { get; set; } = new List<Booking>();
        public int RemainingNumberOfSeats { get; set; }
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
    }
}
