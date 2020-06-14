using System;
using StackExchange.Redis;

namespace StackExchangeApi
{
    class Program
    {
        static void Main(string[] args)
        {

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");

            #region String
            //{
            //    DataStructString dataStructString = new DataStructString(redis);

            //    dataStructString.Show();
            //}
            #endregion

            #region Hash

            {
                DataStructHash dataStructHash = new DataStructHash(redis);

                dataStructHash.Show();
            }

            #endregion
        }
    }
}
