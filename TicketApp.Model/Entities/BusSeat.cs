using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketApp.Model.Entities
{
    public class BusSeat
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SeatID { get; set; }

        [Required]
        public int BusID { get; set; }
        [Required]
        public int SeatIndex { get; set; }
        [Required]
        public string Row { get; set; }
        [Required]
        public string Col { get; set; }
        public string SeatType { get; set; }
        public string Status { get; set; }

        public string SeatNumber => $"({SeatIndex})-{Row}-{Col}";
    }
}
