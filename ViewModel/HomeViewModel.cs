using GigHub.Models;
using System.Collections.Generic;

namespace GigHub.ViewModel
{
    public class HomeViewModel
    {
        public IEnumerable<Gig> UpcomingGigs { get; set; }
        public bool ShowActions { get; set; }
    }
}