using System;
using System.Collections.Generic;
using System.Linq;
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
        Task<BusSeat?> Create(BusSeat seat);
        Task<BusSeat?> Edit(int id, BusSeat seat);
        Task<bool> DeleteConfirmed(int id);
        bool Exists(int id);
    }
    public class SeatService(ISeatRepository repository) : ISeatService
    {
        public Task<BusSeat?> Create(BusSeat seat) => repository.Create(seat);

        public Task<bool> DeleteConfirmed(int id) => repository.DeleteConfirmed(id);

        public Task<BusSeat?> Edit(int id, BusSeat seat) => repository.Edit(id, seat);

        public bool Exists(int id) => repository.Exists(id);

        public Task<BusSeat?> Get(int id) => repository.Get(id);

        public Task<BusSeat?> Get(int busNumber, string row, string col) => repository.Get(busNumber, row, col);

        public Task<List<BusSeat>> GetAll() => repository.GetAll();

        public Task<List<BusSeat>> GetBusSeats(int busNumber) => repository.GetBusSeats(busNumber);
    }
}
