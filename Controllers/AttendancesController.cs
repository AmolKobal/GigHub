using GigHub.Dtos;
using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _context;

        public AttendancesController()
        {
            _context = new ApplicationDbContext();
        }

        [AcceptVerbs("GET", "POST")]
        public IHttpActionResult Attend(AttendanceDto dto)
        {
            var userId = User.Identity.GetUserId();

            if (_context.Attendances.Any(a => a.AttendeeId == userId && a.GigId == dto.GigId))
            {
                //var gig = _context.Gigs.First(g => g.Id == dto.GigId);

                var gigs = _context.Gigs
                    .Include(g => g.Artist)
                    .Include(g => g.Genre)
                    .Where(g => g.Id == dto.GigId);

                var gig = gigs.First();   
                var schedule = gig.Datetime.ToString("dd MMM yyyy, hh:mm tt");
                //try
                //{
                //    //var ganre = _context.Genres.First(g => g.Id == gig.GenreId);
                //    //var artist = _context.Users.First(u => u.Id == gig.ArtistId);

                //    //return BadRequest($"Already planned to attend {artist.Name}'s {ganre.Name} at {gig.Venue} on {schedule}");
                //}
                //catch { }

                //return BadRequest($"Already planned to attend the Gig at {gig.Venue} on {schedule}");
                return BadRequest($"Already planned to attend <b>{gig.Artist.Name}</b>'s <strong><i>{gig.Genre.Name}</i></strong> at {gig.Venue} on <u>{schedule}</u>");
            }

            var attendance = new Attendance
            {
                GigId = dto.GigId,
                AttendeeId = userId
            };

            _context.Attendances.Add(attendance);
            _context.SaveChanges();

            return Ok();
        }
    }
}
