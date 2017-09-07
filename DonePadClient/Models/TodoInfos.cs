using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    
    public class todoinfo
    {
        [Key]
        //[Property("PrimaryKey")]
        public int infoId { get; set; }

        public string title { get; set; }
        public string text { get; set; }

        public DateTime insertDateTime { get; set; }

        public DateTime estimateDateTime { get; set; }

        public DateTime doneDateTime { get; set; }
        public string userName { get; set; }
        public bool isDone { get; set; }
    }
}