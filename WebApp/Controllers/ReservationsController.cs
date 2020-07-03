using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Dto;
using WebApp.Models;

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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Reservation>>> GetReservations(
            [FromQuery] DateTime? from = null,
            [FromQuery] DateTime? to = null)

        {
                //Filters results by date
                IQueryable<Reservation> result = _context.Reservations;

                //if (from != null)
                //{
                //    result = result.Where(e => from <= e.Date);

                //}
                //if (to != null)
                //{
                //    result = result.Where(e => e.Date <= to);
                //}

                if (from != null)
                {
                    result = result.Where(e => from <= e.DepartureTime);

                }
                if (to != null)
                {
                    result = result.Where(e => e.DepartureTime <= to);
                }

                var resultList = await result.ToListAsync();
                return resultList;

                //return await _context.Reservations.ToListAsync();

        }

        // GET: api/Reservations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Reservation>> GetReservation(long id)
        {
            var reservation = await _context.Reservations
                .Include(r => r.Remarks)
                .Select(r => new ReservationDtoDetail()
                {
                    Id = r.Id,
                    Sum = r.Sum,
                    Location = r.Location,
                    AddedOn = r.AddedOn,
                    Currency = r.Currency,
                    Type = r.Type,
                    DepartureTime = r.DepartureTime,
                    ArrivalTime = r.ArrivalTime,
                    Documents = r.Documents,

                    Remarks = r.Remarks.Select(o => new RemarksDtoDetail()
                    {
                        Id = o.Id,
                        Agent = o.Agent,
                        Content = o.Content,
                        //Importance = o.Importance,
                    })
                }).SingleOrDefaultAsync(r => r.Id == id);

            if (reservation == null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

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
