using api_reserva.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace api_reserva.Services
{
    public class SalaService
    {
        private readonly IMongoCollection<Sala> _salaCollection;

        public SalaService(
            IOptions<ReservaDeSalasUsbDatabaseSettings> reservaDeSalasUsbDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                reservaDeSalasUsbDatabaseSettings.Value.ConnectionString
            );

            var mongoDatabase = mongoClient.GetDatabase(
                reservaDeSalasUsbDatabaseSettings.Value.DatabaseName
            );

            _salaCollection = mongoDatabase.GetCollection<Sala>(
                reservaDeSalasUsbDatabaseSettings.Value.SalaCollectionName
            );
        }

        //CRUD Reserva
        public async Task<List<Sala>> GetAsync() =>
            await _salaCollection.Find(_ => true).ToListAsync();

        public async Task<Sala?> GetAsync(string id) =>
            await _salaCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Sala newSala) =>
            await _salaCollection.InsertOneAsync(newSala);

        public async Task UpdateAsync(string id, Sala updatedSala) =>
            await _salaCollection.ReplaceOneAsync(x => x.Id == id, updatedSala);

        public async Task RemoveAsync(string id) =>
            await _salaCollection.DeleteOneAsync(x => x.Id == id);
    }
}