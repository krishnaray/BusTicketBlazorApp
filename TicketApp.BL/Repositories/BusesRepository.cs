using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TicketApp.Model.Entities;
using TicketBlazorApp.Data;

namespace TicketApp.BL.Repositories
{
    public interface IBusesRepository
    {
        Task<List<Bus>> GetAll();
        Task<List<Bus>> GetAll(string number);
        Task<Bus?> Get(int id);
        Task<Bus?> Get(string number);
        Task<Bus?> Create(Bus buses);
        Task<bool> Create(Bus[] bus);
        Task<Bus?> Edit(int id, Bus bus);
        Task<bool> DeleteConfirmed(int id);
        bool BusExists(int id);
    }
    public class BusesRepository(ApplicationDbContext dbContext) : IBusesRepository
    {
        public async Task<List<Bus>> GetAll() => await dbContext.Buses.ToListAsync();
        public async Task<Bus?> Get(int id) => await dbContext.Buses.FindAsync(id);
        public async Task<Bus?> Create(Bus bus)
        {
            if (bus == null) return null;
            try
            {
                var res = await dbContext.Buses.AddAsync(bus);
                await dbContext.SaveChangesAsync();
                return res.Entity;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<bool> Create(Bus[] buses)
        {
            if (buses == null || buses.Length == 0) return false;
            try
            {
                await dbContext.Buses.AddRangeAsync(buses);
                await dbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<Bus?> Edit(int id, Bus bus)
        {
            if (id != bus.BusID) return null;

            if (bus != null)
            {
                try
                {
                    dbContext.Buses.Update(bus);
                    var res =  await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusExists(bus.BusID)) return null;
                    else
                        throw;
                }

                catch (ObjectDisposedException)
                {
                    throw;
                }
            }
            return bus;
        }

        public async Task<bool> DeleteConfirmed(int id)
        {
            var bus = await Get(id);
            if (bus != null)
            {
                try
                {
                    dbContext.Buses.Remove(bus);
                    await dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BusExists(bus.BusID)) return false;
                    else
                        throw;
                }
                return true;
            }
            return false;
        }
        public bool BusExists(int id) => dbContext.Buses.Any(e => e.BusID == id);

        public Task<Bus?> Get(string number) => dbContext.Buses.FirstOrDefaultAsync(b => b.BusNumber == number);

        public async Task<List<Bus>> GetAll(string number)
        {
            return await dbContext.Buses.Where(b => b.BusNumber.ToUpper().Contains(number.ToUpper())).ToListAsync();
        }

    }
}
