using api_reserva.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace api_reserva.Services
{
    public class ReservaService
    {
        private readonly IMongoCollection<Reserva> _reservaCollection;

        public ReservaService(
            IOptions<ReservaDeSalasUsbDatabaseSettings> reservaDeSalasUsbDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                reservaDeSalasUsbDatabaseSettings.Value.ConnectionString
            );

            var mongoDatabase = mongoClient.GetDatabase(
                reservaDeSalasUsbDatabaseSettings.Value.DatabaseName
            );

            _reservaCollection = mongoDatabase.GetCollection<Reserva>(
                reservaDeSalasUsbDatabaseSettings.Value.ReservaCollectionName
            );
        }

        //CRUD Reserva
        public async Task<List<Reserva>> GetAsync() =>
            await _reservaCollection.Find(_ => true).ToListAsync();

        public async Task<Reserva?> GetAsync(string id) =>
            await _reservaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Reserva newReserva) =>
            await _reservaCollection.InsertOneAsync(newReserva);

        public async Task UpdateAsync(string id, Reserva updatedReserva) =>
            await _reservaCollection.ReplaceOneAsync(x => x.Id == id, updatedReserva);

        public async Task RemoveAsync(string id) =>
            await _reservaCollection.DeleteOneAsync(x => x.Id == id);
    }
}