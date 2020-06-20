using StackExchange.Redis;

namespace StackExchangeApi
{

    // 其他与set类似，zset就是排序的set
    public class DataStructZSet
    {
        private readonly IConnectionMultiplexer _redis;

        public DataStructZSet(IConnectionMultiplexer redis)
        {
            this._redis = redis;
        }

        public void Show()
        {
            var db = _redis.GetDatabase(3);

            #region zadd command

            // 设置zset
            {
                // zadd zsetkey 80 "樱木花道" 设置成功并返回true
                bool v1 = db.SortedSetAdd("zsetkey", "樱木花道", 80);
                System.Console.WriteLine($"zadd zsetkey 80 樱木花道: {v1}");

                // zadd zsetkey XX 81 樱木花道 设置成功但返回false
                bool v11 = db.SortedSetAdd("zsetkey", "樱木花道", 81, When.Exists);
                System.Console.WriteLine($"zadd zsetkey XX 81 樱木花道: {v11}");

                // zadd zsetkey NX 85 流川枫 设置成功并返回true
                bool v2 = db.SortedSetAdd("zsetkey", "流川枫", 85, When.NotExists);
                System.Console.WriteLine($"zadd zsetkey NX 85 流川枫: {v2}");

                // zadd zsetkey XX 86 流川枫 设置成功但返回false
                bool v21 = db.SortedSetAdd("zsetkey", "流川枫", 86, When.Exists);
                System.Console.WriteLine($"zadd zsetkey XX 86 流川枫: {v21}");

                // zadd zsetkey XX 82 宫城良田 设置未成功并返回false
                bool v3 = db.SortedSetAdd("zsetkey", "宫城良田", 82, When.Exists);
                System.Console.WriteLine($"zadd zsetkey XX 82 宫城良田: {v3}");

                // zadd zsetkey NX 83 宫城良田  设置成功并返回true
                bool v31 = db.SortedSetAdd("zsetkey", "宫城良田", 83, When.NotExists);
                System.Console.WriteLine($"zadd zsetkey NX 83 宫城良田: {v31}");

                // zadd zsetkey 84 三井寿 83 赤木刚宪
                long counts = db.SortedSetAdd("zsetkey", new SortedSetEntry[] { new SortedSetEntry("三井寿", 84), new SortedSetEntry("赤木刚宪", 83) });
            }

            #endregion

            #region zrange command

            // 获取zset中的元素
            {
                // zrange zsetkey 0 -1 withscores
                SortedSetEntry[] sorteds = db.SortedSetRangeByRankWithScores("zsetkey");

                foreach (var item in sorteds)
                {
                    System.Console.WriteLine($"value:{item.Element},socre:{item.Score}");
                }

                // zrange zsetkey 2 2
                RedisValue[] values = db.SortedSetRangeByRank("zsetkey", 2, 2);

                foreach (var item in values)
                {
                    System.Console.WriteLine($"value:{item}");
                }
            }

            #endregion

            #region zrangebyscore command

            // 根据指定的分数范围来获取指定的元素
            {
                // 获取分数在81-82之间的元素跳过0个取1个
                // ZRANGEBYSCORE zsetkey 81 82 limit 0 1  
                RedisValue[] values1 = db.SortedSetRangeByScore("zsetkey", 81, 82, Exclude.None, Order.Ascending, 0, 1);

                // ZRANGEBYSCORE zsetkey (81 (82 limit 0 1  
                RedisValue[] values2 = db.SortedSetRangeByScore("zsetkey", 81, 82, Exclude.Both, Order.Ascending, 0, 1);

                // ZRANGEBYSCORE zsetkey (81 82 limit 0 1 
                RedisValue[] values3 = db.SortedSetRangeByScore("zsetkey", 81, 82, Exclude.Start, Order.Ascending, 0, 1);

                // ZRANGEBYSCORE zsetkey 81 (82 limit 0 1 
                RedisValue[] values4 = db.SortedSetRangeByScore("zsetkey", 81, 82, Exclude.Stop, Order.Ascending, 0, 1);

                // ZRANGEBYSCORE zsetkey 81 82 withscores limit 0 1  
                SortedSetEntry[] sorteds = db.SortedSetRangeByScoreWithScores("zsetkey", 81, 82, Exclude.None, Order.Ascending, 0, 1);
            }

            #endregion

            #region zrangebylex command  todo 未理解

            // 更加zset中元素的值范围获取元素
            {
                // zrangelex zsetkey 樱 赤
                RedisValue[] values = db.SortedSetRangeByValue("zsetkey");
                foreach (var item in values)
                {
                    System.Console.WriteLine($"zrangelex zsetkey 樱 赤: {item}");
                }

            }

            #endregion

            #region zscore command

            // 获取zset中某个元素的score
            {
                // zscore zsetkey 樱木花道
                double? score = db.SortedSetScore("zsetkey", "樱木花道");

                System.Console.WriteLine($"樱木花道score:{score}");
            }

            #endregion

            #region zrank command

            // 获取zset中某个元素的rank排名
            {
                // zrank zsetkey 樱木花道
                long? rank1 = db.SortedSetRank("zsetkey", "樱木花道");

                System.Console.WriteLine($"樱木花道rank:{rank1}");

                // zrank zsetkey 流川枫
                long? rank2 = db.SortedSetRank("zsetkey", "流川枫");

                System.Console.WriteLine($"流川枫rank:{rank2}");
            }

            #endregion
        }
    }
}