﻿namespace Domain
{
    public class Flight
    {
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
            BookingList.Add(new Booking(passengerEmail, numberOfSeats));
            return null;
         
        }
    }
}
