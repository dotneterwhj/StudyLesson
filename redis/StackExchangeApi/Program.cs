using System;
using StackExchange.Redis;

namespace StackExchangeApi
{
    class Program
    {
        static void Main(string[] args)
        {

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("192.168.56.101");

            DataStructString dataStructString = new DataStructString(redis);

            dataStructString.Show();

        }
    }
}
