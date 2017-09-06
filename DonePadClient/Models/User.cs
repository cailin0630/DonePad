using System.Net.Mime;
using System.Windows.Controls;
using System.Windows.Media;
using MongoDB.Bson;

namespace DonePadClient.Models
{
    public class User
    {
        public ObjectId _id { get; set; }
        public byte[] Image { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int Permission { get; set; }
    }

    public class users
    {
        public decimal userId { get; set; }
        public string image { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public decimal permission { get; set; }
    }
}