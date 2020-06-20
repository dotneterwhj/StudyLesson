using StackExchange.Redis;

namespace StackExchangeApi
{
    public class DataStructList
    {
        private readonly IConnectionMultiplexer _redis;

        public DataStructList(IConnectionMultiplexer redis)
        {
            this._redis = redis;
        }

        public void Show()
        {
            var db = _redis.GetDatabase(4);

            #region lpush command

            // lpush listkey a b c d e
            {
                long length1 = db.ListLeftPush("listkey", "a");
                long length2 = db.ListLeftPush("listkey", "b");
                long length3 = db.ListLeftPush("listkey", "c");
                long length4 = db.ListLeftPush("listkey", "d");
                long length5 = db.ListLeftPush("listkey", "e");
            }

            #endregion

            #region rpush command

            // rpush listkey a b c d e
            {
                long length1 = db.ListRightPush("listkey", "a");
                long length2 = db.ListRightPush("listkey", "b");
                long length3 = db.ListRightPush("listkey", "c");
                long length4 = db.ListRightPush("listkey", "d");
                long length5 = db.ListRightPush("listkey", "e");
            }

            #endregion

            #region lpop command

            // 移除list中的第一个元素并返回
            {
                // lpop listkey
                RedisValue value = db.ListLeftPop("listkey");
            }

            #endregion

            #region rpop command

            // 移除list中的最后一个元素并返回
            {
                // rpop listkey
                RedisValue value = db.ListRightPop("listkey");
            }

            #endregion

            #region lrange command

            // 获取list中的元素
            {
                // lrange listkey 0 -1
                RedisValue[] values = db.ListRange("listkey");

                foreach (var item in values)
                {
                    System.Console.Write($"{item} ");
                }
                System.Console.WriteLine();
            }

            #endregion

            #region rpoplpush command

            // 移除list1中的最后一个元素并添加到list2中的第一个元素
            {
                // rpoplpush list1 list2
                RedisValue value = db.ListRightPopLeftPush("list1", "list2");
            }

            #endregion

        }
    }
}