using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
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
        [Key]
        public int userId { get; set; }
        public Byte[] image { get; set; }
        public string userName { get; set; }
        public string passWord { get; set; }
        public int permission { get; set; }
    }
    
}