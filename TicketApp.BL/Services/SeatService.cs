using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TicketApp.BL.Repositories;
using TicketApp.Model.Entities;

namespace TicketApp.BL.Services
{
    public interface ISeatService
    {

        Task<List<BusSeat>> GetAll();
        Task<List<BusSeat>> GetBusSeats(int busNumber);
        Task<BusSeat?> Get(int id);
        Task<BusSeat?> Get(int busNumber, string row, string col);
        Task<BusSeat?> Get(int busId, int seatIndex);
        Task<List<BusSeat?>?> GetRow(int busId, string row);
        Task<List<BusSeat?>?> GetCol(int busId, string col);
        Task<BusSeat?> Create(BusSeat seat);
        Task<bool> Create(BusSeat[] bus);
        Task<BusSeat?> Edit(int id, BusSeat seat);
        Task<bool> DeleteConfirmed(int id);
        bool Exists(int id);
    }
    public class SeatService(ISeatRepository repository) : ISeatService
    {
        public async Task<BusSeat?> Create(BusSeat seat)  { 
            if(seat == null) return null;
            
            var res = repository.Get(seat.BusID, seat.SeatIndex);
            if (res != null)
            {
                var seatRes = res.Result;
                if (seatRes != null)
                    return seatRes;
            }
            res = repository.Get(seat.BusID, seat.Row, seat.Col);

            if (res != null)
            {
                var seatRes = res.Result;
                if (seatRes != null)
                    return seatRes;
            }

            return await repository.Create(seat); 
        }

        public Task<bool> Create(BusSeat[] seats) => repository.Create(seats);

        public async Task<bool> DeleteConfirmed(int id) => await repository.DeleteConfirmed(id);

        public async Task<BusSeat?> Edit(int id, BusSeat seat)  {
            if (seat == null) return null;
            var res = repository.Get(seat.BusID, seat.SeatIndex);
            if (res != null && res.Result != null) {
                if (res.Result.SeatID != id) return null;
            }
            res = repository.Get(seat.BusID, seat.Row, seat.Col);
            if (res != null && res.Result != null)
            {
                if (res.Result.SeatID != id) return null;
            }
            return await repository.Edit(id, seat); 
        }

        public bool Exists(int id) => repository.Exists(id);

        public Task<BusSeat?> Get(int id) => repository.Get(id);

        public Task<BusSeat?> Get(int busNumber, string row, string col) => repository.Get(busNumber, row, col);

        public Task<BusSeat?> Get(int busId, int seatIndex) => repository.Get(busId, seatIndex);

        public Task<List<BusSeat>> GetAll() => repository.GetAll();

        public Task<List<BusSeat>> GetBusSeats(int busNumber) => repository.GetBusSeats(busNumber);

        public Task<List<BusSeat?>?> GetCol(int busId, string col)
        {
            throw new NotImplementedException();
        }

        public Task<List<BusSeat?>?> GetRow(int busId, string row)
        {
            throw new NotImplementedException();
        }
    }
}
