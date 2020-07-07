using System;
using System.Collections.Generic;


namespace WebApp.Models
{
    public enum Type
    {
        circuit,
        stay,
        accommodation,
        transport,
        others
    }
    public enum Currency
    {
        EUR,
        RON,
        USD
    }
    public class Reservation
    {
        public long Id { get; set; }

        public long Sum { get; set; }

        public string Location { get; set; }

        public DateTime AddedOn { get; set; }

        public Currency Currency { get; set; }

        public Type Type { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public bool Documents { get; set; }

        public User AddedBy { get; set; }

        public List<Remarks> Remarks { get; set; }
    }
}

