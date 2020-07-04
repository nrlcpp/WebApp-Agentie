using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;
using Type = WebApp.Models.Type;

namespace WebApp.Dto
{
    public class ReservationWithNumberOfRemarks
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

        public long NumberOfRemarks { get; set; }

        public static ReservationWithNumberOfRemarks FromReservation (Reservation reservation)
        {
            return new ReservationWithNumberOfRemarks
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
                NumberOfRemarks = reservation.Remarks.Count
            };
        }
    }
}
