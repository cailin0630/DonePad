using MongoDB.Bson;

namespace DonePadClient.Models
{
    public class User
    {
        public ObjectId _id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Permission { get; set; }
    }
}