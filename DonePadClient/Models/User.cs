using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DonePadClient.Models
{
    public class User
    {
        public ObjectId _id { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int Permission { get; set; }
    }
}
