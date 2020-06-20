using System;
using StackExchange.Redis;

namespace StackExchangeApi
{
    class Program
    {
        static void Main(string[] args)
        {

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("121.36.204.34");

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

            {
                DataStructSet dataStructSet = new DataStructSet(redis);

                dataStructSet.Show();
            }

            #endregion
        }
    }
}
