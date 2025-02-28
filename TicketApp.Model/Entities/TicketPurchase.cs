using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketApp.Model.Entities
{
    internal class TicketPurchase
    {
        public int TicketID { get; set; }
        public int BookingID { get; set; }
        public int SeatID { get; set; }
        public decimal Price { get; set; }
        public string PurchaseStatus { get; set; }
    }
}
