using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace WebApplication.Models
{
    [Serializable, BsonIgnoreExtraElements]
    public class ClientInfo
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("ip")]
        [JsonProperty("ip")]
        public string Ip { get; set; } = "";

        [BsonElement("enterTime")]
        public string EnterTime { get; set; }

        [BsonElement("accessTimes")]
        public int AccessTimes { get; set; } = 0;

        [BsonElement("stayTime")]
        public string StayTime { get; set; } = "";


        [BsonElement("latitude")]
        [JsonProperty("latitude")]
        public string Latitude { get; set; }

        [BsonElement("longitude")]
        [JsonProperty("longitude")]
        public string Longitude { get; set; }

        [BsonElement("Country")]
        [JsonProperty("country_name")]
        public string Country { get; set; }

        [BsonElement("Region")]
        [JsonProperty("region_name")]
        public string Region { get; set; }

        [BsonElement("City")]
        [JsonProperty("city_name")]
        public string City { get; set; }

        [BsonElement("InternetProvider")]
        [JsonProperty("as")]
        public string InternetProvider { get; set; }

        [BsonElement("Nume")]
        public string Nume { get; set; }

        public override string ToString()
        {
            return $"{Id}, {Ip}, {AccessTimes}, {StayTime}, {Latitude}, {Longitude}," +
                $"{Country}, {Region}, {City}, {InternetProvider}, {Nume}, {EnterTime}";
        }

        //{"ip":"81.180.211.46","country_code":"RO","country_name":"Romania","region_name":"Iasi","city_name":"Iasi","latitude":47.166684,"longitude":27.600119,"zip_code":"707047","time_zone":"+03:00","asn":"2614","as":"Agentia de Administrare a Retelei Nationale de Informatica pentru Educatie si Cercetare","is_proxy":false}
    }
}