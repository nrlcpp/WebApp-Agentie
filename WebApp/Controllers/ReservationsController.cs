using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Dto;
using WebApp.Models;
using WebApp.Models.Collections;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationsDbContext _context;

        public ReservationsController(ReservationsDbContext context)
        {
            _context = context;
        }

        // GET: api/Reservations
        /// <summary>
        /// Gets a list of all the reservations.
        /// </summary>
        /// <param name="from">Filter reservations added from this date time (inclusive). Leave empty for no lower limit</param>
        /// <param name="to">Filter bookings add up to this date time (inclusive). Leave empty for no upper limit.</param>
        /// <param name="page">The page of results, starting from 0.</param>
        /// <param name="itemsPerPage">The number of reservationss to display per page</param>
        /// <returns>A list of reservation objects.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationWithNumberOfRemarks>>> GetReservations(
            [FromQuery] DateTime? from = null,
            [FromQuery] DateTime? to = null,
            [FromQuery] int page = 0,
            [FromQuery] int itemsPerPage = 15)

        {
            //var identity = User.Identity;

            //Filters results by date
            IQueryable<Reservation> result = _context.Reservations;

                if (from != null)
                {
                    result = result.Where(r => from <= r.DepartureTime);

                }
                if (to != null)
                {
                    result = result.Where(r => r.DepartureTime <= to);
                }

            var resultList = await result
            .OrderByDescending(r => r.Sum)
            .Include(r => r.Remarks)
            .Skip(page * itemsPerPage)
            .Take(itemsPerPage)
            .Select(r => ReservationWithNumberOfRemarks.FromReservation(r))
            .ToListAsync();

            var paginatedList = new PaginatedList<ReservationWithNumberOfRemarks>(page, await result.CountAsync(), itemsPerPage);
            paginatedList.Items.AddRange(resultList);

            return Ok(paginatedList);

                //return await _context.Reservations.ToListAsync();

        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDetails>> GetReservation(long id)
        {
            var reservation = await _context
                .Reservations
                .Include(r => r.Remarks)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return ReservationDetails.FromReservation(reservation);
        }
            //    var reservation = await _context.Reservations
            //        .Include(r => r.Remarks)
            //        .Select(r => new ReservationDtoDetail()
            //        {
            //            Id = r.Id,
            //            Sum = r.Sum,
            //            Location = r.Location,
            //            AddedOn = r.AddedOn,
            //            Currency = r.Currency,
            //            Type = r.Type,
            //            DepartureTime = r.DepartureTime,
            //            ArrivalTime = r.ArrivalTime,
            //            Documents = r.Documents,

            //            Remarks = r.Remarks.Select(o => new RemarksDtoDetail()
            //            {
            //                Id = o.Id,
            //                Agent = o.Agent,
            //                Content = o.Content,
            //                //Importance = o.Importance,
            //            })
            //        }).SingleOrDefaultAsync(r => r.Id == id);

            //    if (reservation == null)
            //    {
            //        return NotFound();
            //    }

            //    return Ok(reservation);
            //}

            // PUT: api/Reservations/5
            // To protect from overposting attacks, enable the specific properties you want to bind to, for
            // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
            [HttpPut("{id}")]
        public async Task<IActionResult> PutReservation(long id, Reservation reservation)
        {
            if (id != reservation.Id)
            {
                return BadRequest();
            }

            _context.Entry(reservation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ReservationExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Reservations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Reservation>> PostReservation(Reservation reservation)
        {
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetReservation", new { id = reservation.Id }, reservation);
        }

        // DELETE: api/Reservations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Reservation>> DeleteReservation(long id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation == null)
            {
                return NotFound();
            }

            _context.Reservations.Remove(reservation);
            await _context.SaveChangesAsync();

            return reservation;
        }

        private bool ReservationExists(long id)
        {
            return _context.Reservations.Any(e => e.Id == id);
        }
    }
}
