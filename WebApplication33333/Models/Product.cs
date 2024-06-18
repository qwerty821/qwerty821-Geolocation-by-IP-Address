using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApplication.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class Product
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("desc")]
        public string Description { get; set; }
        [BsonElement("price")]
        public decimal Price { get; set; }

        public Product(string name, string desc, decimal price)
        {
            this.Id = ObjectId.GenerateNewId();
            this.Name = name;
            this.Description = desc;
            this.Price = price;
        }
    }
}
