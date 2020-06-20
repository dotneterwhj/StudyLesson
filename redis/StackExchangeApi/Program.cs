using System;
using StackExchange.Redis;

namespace StackExchangeApi
{
    class Program
    {
        static void Main(string[] args)
        {

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("121.36.204.34");

            //IServer server = redis.GetServer("121.36.204.34","6379");

            //server.FlushAllDatabases();

            #region String
            //{
            //    DataStructString dataStructString = new DataStructString(redis);

            //    dataStructString.Show();
            //}
            #endregion

            #region Hash

            // {
            //     DataStructHash dataStructHash = new DataStructHash(redis);

            //     dataStructHash.Show();
            // }

            #endregion

            #region Set

            // {
            //     DataStructSet dataStructSet = new DataStructSet(redis);

            //     dataStructSet.Show();
            // }

            #endregion

            #region ZSet

            // {
            //     DataStructZSet dataStructZSet = new DataStructZSet(redis);

            //     dataStructZSet.Show();
            // }

            #endregion

            #region List

            {
                DataStructList dataStructList = new DataStructList(redis);

                dataStructList.Show();
            }

            #endregion
        }
    }
}
