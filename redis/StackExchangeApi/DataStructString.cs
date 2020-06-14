using StackExchange.Redis;

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
            IDatabase db = _redis.GetDatabase();

            #region set get command

            {
                var key = "name";

                var set = db.StringSet(key, "浪客行");

                var value = db.StringGet(key);

                System.Console.WriteLine($"key:{key},value:{value}");

            }

            #endregion


        }

    }
}