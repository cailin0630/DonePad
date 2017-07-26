using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DonePadClient.MongoDb
{
    public class MongoDbProvide
    {
        static MongoDbProvide()
        {
            Init();
        }

        private static MongoClient _mongoClient;
        private static IMongoDatabase _db;
        private static bool _connected;

        private static void Init()
        {
            if (_connected)
                return;
            _mongoClient = new MongoClient("mongodb://localhost:27017");
            _db = _mongoClient.GetDatabase("ToDoDebug10");
            _connected = true;
        }

        private static void CreateTables<T>()
        {
            try
            {
                _db.CreateCollection(nameof(T));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void Insert<T>(T info)
        {
            var col = _db.GetCollection<T>(typeof(T).Name);
            col.InsertOne(info);
        }

        public static List<T> QueryList<T>(Expression<Func<T, bool>> expression)
        {
            var col = _db.GetCollection<T>(typeof(T).Name);
            return col.AsQueryable().Where(expression).ToList();
        }

        public static List<T> QueryList<T>()
        {
            var col = _db.GetCollection<T>(typeof(T).Name);
            return col.AsQueryable().ToList();
        }

        //public static void Update<T>(T info)
        //{
        //    var col = _db.GetCollection<T>(nameof(T));
        //    return col.UpdateOne(a => a.StudentId == studentId, Builders<Student>.Update.Set(a => a.IQ, 90));
        //}
    }
}