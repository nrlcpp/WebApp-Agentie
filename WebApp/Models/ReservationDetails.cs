using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class ReservationDetails
    {
        public long Id { get; set; }

        //   public string Description { get; set; }

        public long Sum { get; set; }

        public string Location { get; set; }

        public DateTime AddedOn { get; set; }

        public Currency Currency { get; set; }

        public Type Type { get; set; }

        public DateTime DepartureTime { get; set; }

        public DateTime ArrivalTime { get; set; }

        public bool Documents { get; set; }

        public List<RemarksForReservationsDetails> Remarks { get; set; }

        public static ReservationDetails FromReservation(Reservation reservation)
        {
            return new ReservationDetails
            {
                Id = reservation.Id,
                Sum = reservation.Sum,
                Location = reservation.Location,
                AddedOn = reservation.AddedOn,
                Currency = reservation.Currency,
                Type = reservation.Type,
                DepartureTime = reservation.DepartureTime,
                ArrivalTime = reservation.ArrivalTime,
                Documents = reservation.Documents,
                Remarks = reservation.Remarks.Select(o => RemarksForReservationsDetails.FromRemarks(o)).ToList()

            };
        }
    }
}


