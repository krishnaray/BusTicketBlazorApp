using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketApp.Model.Entities;
using TicketBlazorApp.Data;

namespace TicketApp.BL.Repositories
{
    public interface ISeatRepository
    {
        Task<List<BusSeat>> GetAll();
        Task<List<BusSeat>> GetBusSeats(int busId);
        Task<BusSeat?> Get(int id);
        Task<BusSeat?> Get(int busId, string row, string col);
        Task<BusSeat?> Get(int busId, int seatIndex);
        Task<List<BusSeat>> GetRow(int busId, string row);
        Task<List<BusSeat>> GetCol(int busId, string col);
        Task<BusSeat?> Create(BusSeat seat);
        Task<bool> Create(BusSeat[] seats);
        Task<BusSeat?> Edit(int id, BusSeat seat);
        Task<bool> DeleteConfirmed(int id);
        bool Exists(int id);
    }
    public class SeatRepository(ApplicationDbContext dbContext) : ISeatRepository
    {
        public async Task<List<BusSeat>> GetAll() => await dbContext.BusSeats.ToListAsync();

        public bool Exists(int id) => dbContext.BusSeats.Any(e => e.SeatID == id);

        public async Task<BusSeat?> Create(BusSeat seat)
        {
            if (seat == null) return null;
            try
            {
                var res = await dbContext.BusSeats.AddAsync(seat);
                await dbContext.SaveChangesAsync();
                return res.Entity;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<bool> Create(BusSeat[] seats)
        {
            if(seats == null || seats.Length <= 0) return false;
            try
            {
                await dbContext.BusSeats.AddRangeAsync(seats);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<BusSeat?> Edit(int id, BusSeat seat)
        {
            if (id != seat.BusID) return null;

            if (seat != null)
            {
                try
                {
                    dbContext.BusSeats.Update(seat);
                    var res = await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Exists(seat.SeatID)) return null;
                    else
                        throw;
                }

                catch (ObjectDisposedException)
                {
                    throw;
                }
            }
            return seat;
        }
        public async Task<bool> DeleteConfirmed(int id)
        {
            var seat = await Get(id);
            if (seat != null)
            {
                try
                {
                    dbContext.BusSeats.Remove(seat);
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Exists(seat.SeatID)) return false;
                    else
                        throw;
                }
                return true;
            }
            return false;
        }


        public async Task<BusSeat?> Get(int id) => await dbContext.BusSeats.FindAsync(id);

        public async Task<BusSeat?> Get(int busId, string row, string col) => 
            await dbContext.BusSeats.FirstOrDefaultAsync(
                e => e.BusID == busId && e.Row == row && e.Col == col);

        public async Task<List<BusSeat>> GetBusSeats(int busId) => 
            await dbContext.BusSeats.Where(e => e.BusID == busId).ToListAsync();

        public async Task<BusSeat?> Get(int busId, int seatIndex) => 
            await dbContext.BusSeats.FirstOrDefaultAsync(
                e => e.BusID == busId && e.SeatIndex == seatIndex);

        public async Task<List<BusSeat>> GetRow(int busId, string row) =>
            await dbContext.BusSeats.Where(e => e.BusID == busId && e.Row == row).ToListAsync();

        public async Task<List<BusSeat>> GetCol(int busId, string col) =>
            await dbContext.BusSeats.Where(e => e.BusID == busId && e.Col == col).ToListAsync();

    }
}
