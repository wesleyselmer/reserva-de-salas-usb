using api_reserva.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace api_reserva.Services
{
    public class PeriodoService
    {
        private readonly IMongoCollection<Periodo> _periodoCollection;

        public PeriodoService(
            IOptions<ReservaDeSalasUsbDatabaseSettings> reservaDeSalasUsbDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                reservaDeSalasUsbDatabaseSettings.Value.ConnectionString
            );

            var mongoDatabase = mongoClient.GetDatabase(
                reservaDeSalasUsbDatabaseSettings.Value.DatabaseName
            );

            _periodoCollection = mongoDatabase.GetCollection<Periodo>(
                reservaDeSalasUsbDatabaseSettings.Value.PeriodoCollectionName
            );
        }

        //CRUD Reserva
        public async Task<List<Periodo>> GetAsync() =>
            await _periodoCollection.Find(_ => true).ToListAsync();

        public async Task<Periodo?> GetAsync(string id) =>
            await _periodoCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Periodo newPeriodo) =>
            await _periodoCollection.InsertOneAsync(newPeriodo);

        public async Task UpdateAsync(string id, Periodo updatedPeriodo) =>
            await _periodoCollection.ReplaceOneAsync(x => x.Id == id, updatedPeriodo);

        public async Task RemoveAsync(string id) =>
            await _periodoCollection.DeleteOneAsync(x => x.Id == id);
   }
}