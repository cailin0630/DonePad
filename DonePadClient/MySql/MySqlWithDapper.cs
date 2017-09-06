using Dapper;
using DonePadClient.Models;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
using System.Linq;

namespace DonePadClient.MySql
{
    public class MySqlWithDapper
    {
        private static readonly string MysqlCon = $"DataBase='todorelease';Data Source='localhost';User Id='root';Password='';charset='utf8';pooling=true";

        public static MySqlConnection OpenMySqlConnection()
        {
            var connection = new MySqlConnection(MysqlCon);
            connection.Open();
            return connection;
        }

        public static IEnumerable<users> QueryUserList()
        {
            using (var con = OpenMySqlConnection())
            {
                const string queryStr = "select * from users";
                return con.Query<users>(queryStr);
            }
        }

        public static IEnumerable<T> Query<T>()
        {
            using (var con = OpenMySqlConnection())
            {
                var queryStr = $"select * from {typeof(T).Name}";
                return con.Query<T>(queryStr);
            }
        }

        public static users QuerySingleUser(decimal userId)
        {
            using (var con = OpenMySqlConnection())
            {
                const string queryStr = "select * from users where userId=@userId";
                return con.Query<users>(queryStr, new users { userId = userId }).SingleOrDefault();
            }
        }

        public static T QueryFirst<T>(string keyName, object keyValue)
        {
            using (var con = OpenMySqlConnection())
            {
                var queryStr = $"select * from {typeof(T).Name} where {keyName}={keyValue}";
                return con.QueryFirst<T>(queryStr, new { keyValue });
            }
        }

        public static bool InsertNewUser(users user)
        {
            using (var con = OpenMySqlConnection())
            {
                const string queryStr = "insert into users values(@userId,@image,@userName,@passWord,@permission)";
                return con.Execute(queryStr, user) > 0;
            }
        }

        public static bool InsertOne<T>(T obj)
        {
            var properties = obj.GetType().GetProperties();
            var valueStr = "";
            foreach (var property in properties)
            {
                valueStr += $"@{property.Name},";
            }
            valueStr = valueStr.TrimEnd(',');
            using (var con = OpenMySqlConnection())
            {
                var sqlStr = $"insert into {typeof(T).Name} values({valueStr})";
                return con.Execute(sqlStr, obj) > 0;
            }
        }

        public static bool UpdateSingleUser(users user)
        {
            using (var con = OpenMySqlConnection())
            {
                const string queryStr = "update users set image=@image,userName=@userName,passWord=@passWord,permission=@permission where userId=@userId";
                return con.Execute(queryStr, user) > 0;
            }
        }

        public static bool Update<T>(T obj, string keyName, object keyValue)
        {
            var properties = obj.GetType().GetProperties();
            var valueStr = "";
            foreach (var property in properties)
            {
                var name = property.Name;
                valueStr += $"{name}=@{name},";
            }
            valueStr = valueStr.TrimEnd(',');
            using (var con = OpenMySqlConnection())
            {
                var sqlStr = $"update {typeof(T).Name} set {valueStr} where {keyName}={keyValue}";
                return con.Execute(sqlStr, obj) > 0;
            }
        }

        public static bool DeleteSingleUser(decimal userId)
        {
            using (var con = OpenMySqlConnection())
            {
                const string queryStr = "delete from users where userId=@userId";
                return con.Execute(queryStr, new users { userId = userId }) > 0;
            }
        }

        public static bool Delete<T>(string keyName, object keyValue)
        {
            using (var con = OpenMySqlConnection())
            {
                var sqlStr = $"delete from {typeof(T).Name} where {keyName}={keyValue}";
                return con.Execute(sqlStr, new { keyValue }) > 0;
            }
        }
    }
}