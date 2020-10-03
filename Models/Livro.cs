using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace aspnet_mongodb.Models
{
    public class Livro
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public string Titulo { get; set; }

        public string Categoria { get; set; }

        public string Editora { get; set; }

        [BsonElement("Valor")]
        public decimal Preco { get; set; }

        public Autor Autor { get; set; }
    }
}
