using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_reserva.Models
{
    public class ReservaDeSalasUsbDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string ReservaCollectionName { get; set; } = null!;

        public string SalaCollectionName { get; set; } = null!;

        public string PeriodoCollectionName { get; set; } = null!;

        public string UsuarioCollectionName { get; set; } = null!;
    }
}