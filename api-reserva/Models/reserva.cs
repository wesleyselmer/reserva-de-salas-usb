using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace api_reserva.Models
{
    public class Reserva
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        [BsonElement("reserva")]
        public int Reserva_id { get; set; }

        public int Sala_id { get; set; }

        public int Periodo_id { get; set; }

        public DateOnly Data { get; set; }

        public string Departamento { get; set; } = null!;

        public string Responsavel { get; set; } = null!;

        public int Status { get; set; }

        public string Observacao { get; set; } = null!;
    }
}