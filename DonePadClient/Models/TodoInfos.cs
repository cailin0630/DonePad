using MongoDB.Bson;
using System;
using MongoDB.Bson.Serialization.Attributes;

namespace DonePadClient.Models
{
    public class TodoInfos
    {
        public ObjectId _id { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime InsertDateTime { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime EstimateDateTime { get; set; }
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime DoneDateTime { get; set; }
        public string UserName { get; set; }
        public bool IsDone { get; set; }
    }
}