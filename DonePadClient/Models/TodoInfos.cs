using MongoDB.Bson;
using System;

namespace DonePadClient.Models
{
    public class TodoInfos
    {
        public ObjectId _id { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime InsertDateTime { get; set; }
        public DateTime EstimateDateTime { get; set; }
        public DateTime DoneDateTime { get; set; }
        public string UserName { get; set; }
    }
}