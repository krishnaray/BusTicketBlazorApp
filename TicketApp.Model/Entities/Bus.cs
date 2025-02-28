using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketApp.Model.Entities
{
    
    public class Bus
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order = 1)]
        public int BusID { get; set; }
        [Required]
        public string? BusNumber { get; set; }
        [Required]
        public string BusType { get; set; }
        [Required]
        public int Rows { get; set; }
        [Required]
        public int Cols { get; set; }
        [Required]
        public int TotalSeats { get; set; }
        [Required]
        public string? OperatorName { get; set; }
        public ICollection<BusSeat?>? BusSeats { get; set; }
    }
}
