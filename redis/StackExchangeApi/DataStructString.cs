using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StackExchangeApi
{
    public class DataStructString
    {
        private readonly IConnectionMultiplexer _redis;

        public DataStructString(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public void Show()
        {
            var servers = _redis.GetEndPoints();

            foreach (var item in servers)
            {
                Console.WriteLine(item);
            }

            var server = _redis.GetServer(servers[0]);

            server.FlushDatabase(0);

            IDatabase db = _redis.GetDatabase(0);

            #region set get mset mget command

            // 设置单个key set name "浪客行"
            {
                var key = "name";

                // set name "浪客行"
                var set = db.StringSet(key, "浪客行");

                // get name
                var value = db.StringGet(key);

                System.Console.WriteLine($"key:{key},value:{value}");

            }

            // 设置多个key mset firstName "浪" middleName "客" lastName "行"
            {
                var keyValues = new List<KeyValuePair<RedisKey, RedisValue>>();

                keyValues.Add(new KeyValuePair<RedisKey, RedisValue>("firstName", "浪"));
                keyValues.Add(new KeyValuePair<RedisKey, RedisValue>("middleName", "客"));
                keyValues.Add(new KeyValuePair<RedisKey, RedisValue>("lastName", "行"));

                // mset firstName "浪" middleName "客" lastName "行"
                var mset = db.StringSet(keyValues.ToArray());

                // mget firstName middleName lastName name notexistkey
                var values = db.StringGet(new RedisKey[] { "firstName", "middleName", "lastName", "name", "notexistkey" });

                foreach (var value in values)
                {
                    System.Console.WriteLine(value);
                }
            }

            // 设置key 并指定过期时间  set expirename value ex 1
            {
                // set expirename value ex 1
                db.StringSet("expirename", "会过期的值", TimeSpan.FromSeconds(1));

                Console.WriteLine($"刚设置：{db.StringGet("expirename")}");

                Task.Delay(1000).Wait();

                Console.WriteLine($"3秒后：{db.StringGet("expirename")}");

            }

            // 只有当key存在时才设置key set key value1 xx
            // 只有当key不存在时才设置key set key value nx
            {

                // set key value1 xx
                var set = db.StringSet("key", "value1", when: When.Exists);

                Console.WriteLine(set);

                // set key value nx
                var set2 = db.StringSet("key", "value2", when: When.NotExists);

                Console.WriteLine(set2);
            }

            #endregion

            #region getset commad

            // 设置新值，并返回旧值  getset name "new value"
            {
                // getset name "new value"
                var oldvalue = db.StringGetSet("name", "new value");

                Console.WriteLine($"oldValue:{oldvalue},newValue:{db.StringGet("name")}");
            }

            #endregion

            #region append command

            // 在原有的值上增加内容 append name " hello"
            {
                // append name " hello"
                var length = db.StringAppend("name", " hello");

                Console.WriteLine(db.StringGet("name"));
            }

            #endregion

            #region incr command

            // 值自增1  incr count
            {
                // set count 10
                var set = db.StringSet("count", 10);
                
                // incr count
                var count1 = db.StringIncrement("count");

                // incr count
                var count2 = db.StringIncrement("count");

                // incr count
                var count3 = db.StringIncrement("count");

                Console.WriteLine($"count1:{count1},count2:{count2},count3:{count3}");
            }

            #endregion

            #region decr command

            // 值自减1 decr count
            {
                // decr count
                var count1 = db.StringDecrement("count");

                // decr count
                var count2 = db.StringDecrement("count");

                // decr count
                var count3 = db.StringDecrement("count");

                Console.WriteLine($"count1:{count1},count2:{count2},count3:{count3}");
            }

            #endregion

        }

    }
}