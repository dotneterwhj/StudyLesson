using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace StackExchangeApi
{
    public class DataStructHash
    {
        private readonly IConnectionMultiplexer _redis;

        public DataStructHash(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public void Show()
        {
            //var servers = _redis.GetEndPoints();

            //foreach (var item in servers)
            //{
            //    Console.WriteLine(item);
            //}

            //var server = _redis.GetServer(servers[0]);

            //server.FlushDatabase(1);

            IDatabase db = _redis.GetDatabase(1);

            #region hset hget hmset hmget command

            // 设置key,   hset hkey id 1
            {
                // hset hkey id 1
                var idset = db.HashSet("hkey", "id", "1");

                // hset hkey name nextload
                var nameset = db.HashSet("hkey", "name", "nextload");

                // hset hkey age 28
                var ageset = db.HashSet("hkey", "age", "28");

                // hget hkey id
                var id = db.HashGet("hkey", "id");

                // hget hkey name
                var name = db.HashGet("hkey", "name");

                // hget hkey age
                var age = db.HashGet("hkey", "age");

                Console.WriteLine($"id:{id},name:{name},age:{age}");
            }

            // 设置多个key,  hmset muitkey id 2 name nextload gender male
            {
                var hashEnty = new HashEntry[] {
                    new HashEntry("id", 2),
                    new HashEntry("name", "nextload"),
                    new HashEntry("gender", "male"),
                };

                // hmset muitkey id 2 name nextload gender male
                db.HashSet("muitkey", hashEnty);

                // hmget hkey id name age
                var getEnty1 = db.HashGetAll("hkey");

                // hmget muitkey id name gender
                var getEnty2 = db.HashGetAll("muitkey");

                foreach (var enty in getEnty1)
                {
                    Console.WriteLine($"{enty.Name}:{enty.Value}");
                }

                foreach (var enty in getEnty2)
                {
                    Console.WriteLine($"{enty.Name}:{enty.Value}");
                }
            }

            #endregion

            #region hsetnx command

            // 是否存在键来设置值  hsetnx hkey id 222  hsetnx hkey height 170  hset hkey name "new name"
            {
                // throw exception Exists is not valid in this context; the permitted values are: Always, NotExists
                // var exists = db.HashSet("hkey", "id", "11", When.Exists);

                // hsetnx hkey id 222
                // 如果key id 的键已经存在，则不会改变其value
                var existskey = db.HashSet("hkey", "id", "222", When.NotExists);

                // hsetnx hkey height 170
                // 如果key id 的键不存在，则改变其value
                var notExists = db.HashSet("hkey", "height", "170", When.NotExists);

                // hset hkey name "new name"
                // 不管key id 的键是否存在，都会改变其value
                var overwrite = db.HashSet("hkey", "name", "new name");
            }

            #endregion

            #region hlen command

            // 获取key中的数量 
            {
                // hlen hkey
                var length = db.HashLength("hkey");
                Console.WriteLine($"hkey中的filed数量为:{length}");
            }

            #endregion

            #region hkeys command

            // 获取key中的所有filed
            {
                // hkeys hkey
                var keys = db.HashKeys("hkey");

                foreach (var key in keys)
                {
                    Console.WriteLine($"hkey中的filed为{key}");
                }
            }

            #endregion

            #region hvals command

            {
                // hvals hkey
                var values = db.HashValues("hkey");

                foreach (var value in values)
                {
                    Console.WriteLine($"hkey中的filed的值为{value}");
                }
            }

            #endregion





        }
    }
}
