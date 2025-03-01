using TicketApp.BL.Services;
using TicketApp.Model.Entities;

namespace TicketBlazorApp.Utils
{
    public static class SeatUtils
    {
        public static async Task<List<BusSeat>?> CheckSeats(ISeatService service, List<BusSeat>? seats, Bus? bus, bool deleteFromDB) {
            if (bus != null && seats != null)
            {
                seats = seats.OrderBy(s => s.SeatNumber).ToList();
                if (seats.Count > bus.TotalSeats){
                    if (deleteFromDB)
                        await DeleteExtraSeats(service, seats, bus.TotalSeats, seats.Count - 1);
                    seats.RemoveRange(bus.TotalSeats, seats.Count - bus.TotalSeats);
                }
                else if (seats.Count < bus.TotalSeats){
                    await AddRemaningSeats(service, bus, seats.Count, bus.TotalSeats - 1, seats);
                    seats = await service.GetBusSeats(bus.BusID);
                }
            }
            return seats;
        }
        public static async Task DeleteExtraSeats(ISeatService SeatService, List<BusSeat>? Seats, int start, int end)
        {
            if (Seats != null)
            {
                for (int i = start; i <= end && i < Seats.Count; i++)
                {
                    if (Seats[i] == null) continue;
                    await SeatService.DeleteConfirmed(Seats[i].SeatID);
                }
            }
        }
        public static async Task AddRemaningSeats(ISeatService SeatService, Bus? Bus, int startIndex, int endIndex, List<BusSeat>? seats)
        {
            if (Bus == null) return;
            List<BusSeat> newSeats = new List<BusSeat>();
            for (int i = 0; i < Bus.TotalSeats; i++)
            {
                if (seats.Count > i)
                {
                    if (seats[i] == null)
                        addSeatAt(i);
                }
                else
                    addSeatAt(i);
            }
            await SeatService.Create(newSeats.ToArray());

            void addSeatAt(int ind)
            {
                int col = Bus.Cols > 0 ? ind % Bus.Cols : 1;
                int row = Bus.Cols > 0 ? ind / Bus.Cols : 1;
                newSeats.Add(new BusSeat
                {
                    BusID = Bus.BusID,
                    SeatIndex = ind,
                    Row = row.ToString(),
                    Col = col.ToString(),
                    SeatType = "CHAIR",
                    Status = "AVBL"
                });
            }
        }
    }
}
