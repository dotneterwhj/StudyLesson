using System;
using System.Threading.Tasks;
using StackExchange.Redis;
using System.Collections.Generic;

namespace redisdemo
{
    class Program
    {
        static void Main(string[] args)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("192.168.56.101");

            var db = redis.GetDatabase();

            List<Task> tasks = new List<Task>();

            for (int i = 0; i < 100; i++)
            {
                tasks.Add(
                    Task.Run(() =>
                    {
                        db.StringIncrement("count");
                    })
                );
            }


            var value = db.StringGet("count");

            Console.WriteLine(value);

            Console.Read();
        }
    }
}
