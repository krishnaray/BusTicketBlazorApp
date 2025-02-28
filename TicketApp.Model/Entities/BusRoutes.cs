using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketApp.Model.Entities
{
    public class BusRoutes
    {
        public int RouteID { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public decimal DistanceKM { get; set; }
    }
}
