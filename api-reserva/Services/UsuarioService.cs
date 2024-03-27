using api_reserva.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace api_reserva.Services
{
    public class UsuarioService
    {
        private readonly IMongoCollection<Usuario> _usuarioCollection;

        public UsuarioService(
            IOptions<ReservaDeSalasUsbDatabaseSettings> reservaDeSalasUsbDatabaseSettings)
        {
            var mongoClient = new MongoClient(
                reservaDeSalasUsbDatabaseSettings.Value.ConnectionString
            );

            var mongoDatabase = mongoClient.GetDatabase(
                reservaDeSalasUsbDatabaseSettings.Value.DatabaseName
            );

            _usuarioCollection = mongoDatabase.GetCollection<Usuario>(
                reservaDeSalasUsbDatabaseSettings.Value.UsuarioCollectionName
            );
        }

        //CRUD Reserva
        public async Task<List<Usuario>> GetAsync() =>
            await _usuarioCollection.Find(_ => true).ToListAsync();

        public async Task<Usuario?> GetAsync(string id) =>
            await _usuarioCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

        public async Task CreateAsync(Usuario newUsuario) =>
            await _usuarioCollection.InsertOneAsync(newUsuario);

        public async Task UpdateAsync(string id, Usuario updatedUsuario) =>
            await _usuarioCollection.ReplaceOneAsync(x => x.Id == id, updatedUsuario);

        public async Task RemoveAsync(string id) =>
            await _usuarioCollection.DeleteOneAsync(x => x.Id == id);
    }
}