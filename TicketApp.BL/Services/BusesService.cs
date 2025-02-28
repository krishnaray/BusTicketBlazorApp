using TicketApp.BL.Repositories;
using TicketApp.Model.Entities;

namespace TicketApp.BL.Services
{
    public interface IBusesService {
        Task<List<Bus>> GetAll();
        Task<Bus?> Get(int id);
        Task<Bus?> Create(Bus bus);
        Task<Bus?> Edit(int id, Bus bus);
        Task<bool> DeleteConfirmed(int id);
        bool BusExists(int id);
    }
    public class BusesService (IBusesRepository repository) : IBusesService
    {
        public async Task<List<Bus>> GetAll() => await repository.GetAll();
        public async Task<Bus?> Get(int id) => await repository.Get(id);
        public async Task<Bus?> Create(Bus bus) => await repository.Create(bus);
        public async Task<Bus?> Edit(int id, Bus bus) => await repository.Edit(id, bus);
        public bool BusExists(int id) => repository.BusExists(id);
        public Task<bool> DeleteConfirmed(int id) => repository.DeleteConfirmed(id);
    }
}
