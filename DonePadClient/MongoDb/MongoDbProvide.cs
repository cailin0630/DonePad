using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using DonePadClient.Models;
using MongoDB.Bson;

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
       
        public static void DeleteOne<T>(Expression<Func<T, bool>> predicate)
        {
            var col = _db.GetCollection<T>(typeof(T).Name);
            col.DeleteOne(predicate);
        }
        public static void Update<T>(FilterDefinition<T> f,UpdateDefinition<T> u)
        {
            var col = _db.GetCollection<T>(nameof(T));
          
            col.UpdateOne(f,u);
        }
    }
}