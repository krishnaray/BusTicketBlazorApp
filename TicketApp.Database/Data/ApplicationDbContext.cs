using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketApp.Model;
using TicketApp.Model.Entities;

namespace TicketBlazorApp.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Bus> Buses { get; set; }
        public DbSet<BusSeat> BusSeats { get; set; }
    }
    
}
