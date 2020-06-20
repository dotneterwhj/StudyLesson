using StackExchange.Redis;

namespace StackExchangeApi
{
    public class DataStructSet
    {
        private readonly IConnectionMultiplexer _redis;

        public DataStructSet(IConnectionMultiplexer redis)
        {
            this._redis = redis;
        }

        public void Show()
        {
            var db = _redis.GetDatabase(2);

            #region sadd command

            // 向set中添加元素
            {
                // 添加单个元素 sadd set key 1
                bool set = db.SetAdd("setkey", "1");

                // 添加多个元素并返回添加成功的数量 sadd setkey 1 2 3
                long setAdd = db.SetAdd("setkey", new RedisValue[] { "1", "2", "3" });

                long set2Add = db.SetAdd("setkey", new RedisValue[] { "关羽", "张飞", "赵云", "黄总", "马超" });
            }

            #endregion

            #region smembers command

            // 获取set中的所有元素
            {
                // smembers setkey
                RedisValue[] values = db.SetMembers("setkey");

                System.Console.WriteLine($"setkey中的元素为：");
                foreach (var value in values)
                {
                    System.Console.Write($"{value} ");
                }
            }

            #endregion

            #region srandmember command

            // 随机获取set中的元素
            {
                // 随机获取一个元素 srandmember setkey
                RedisValue value = db.SetRandomMember("setkey");

                System.Console.WriteLine($"setkey中随机获取的元素为：{value}");

                // 随机获取多个元素 srandmember setkey 5
                RedisValue[] values = db.SetRandomMembers("setkey", 5);

                System.Console.WriteLine($"setkey中随机获取的5个元素为：");
                foreach (var item in values)
                {
                    System.Console.Write($"{item} ");
                }
                System.Console.WriteLine();
            }

            #endregion

            #region spop command

            // 随机获取set中的元素，并移除该元素
            {
                // 随机获取一个元素并从set中移除该元素 spop setkey
                RedisValue value = db.SetPop("setkey");

                System.Console.WriteLine($"setkey中随机抛出的元素为：{value}");

                // 随机获取多个元素并从set中移除该些元素 spop setkey 3
                RedisValue[] values = db.SetPop("setkey", 3);

                System.Console.WriteLine($"setkey中随机抛出的3个元素为：");
                foreach (var item in values)
                {
                    System.Console.Write($"{item} ");
                }
                System.Console.WriteLine();
            }

            #endregion

            #region scard command

            // 获取set中的元素个数
            {
                // scard setkey
                long length = db.SetLength("setkey");

                System.Console.WriteLine($"setkey中的个数为：{length}");
            }

            #endregion

            #region sismember command

            // 判断set中是否包含该field
            {
                // sismember setkey 1
                bool ismember = db.SetContains("setkey", "1");

                System.Console.WriteLine($"1 ismember:{ismember}");

                // sismember setkey notextist
                bool ismember2 = db.SetContains("setkey", "notextist");

                System.Console.WriteLine($"notextist ismember:{ismember2}");
            }

            #endregion

            #region srem command

            // 移除set中指定的元素
            {
                // 移除单个指定的元素 srem setkey 1
                bool remove = db.SetRemove("setkey", "1");

                // 移除多个指定的元素 srem setkey 1 2 3
                long removeCount = db.SetRemove("setkey", new RedisValue[] { "1", "2", "3" });
            }

            #endregion

            #region sunion sinter sdiff command

            // 获取set的并集，交集，差集
            {
                db.SetAdd("set1", new RedisValue[] { "one", "two", "three", "four", "five" });
                db.SetAdd("set2", new RedisValue[] { "one", "six", "seven", "eight", "five" });

                // sunion set1 set2
                RedisValue[] unions = db.SetCombine(SetOperation.Union, "set1", "set2");

                System.Console.WriteLine($"set1与set2的并集是：");
                foreach (var value in unions)
                {
                    System.Console.Write($"{value} ");
                }
                System.Console.WriteLine();

                // sinter set1 set2
                RedisValue[] intersects = db.SetCombine(SetOperation.Intersect, "set1", "set2");

                System.Console.WriteLine($"set1与set2的交集是：");
                foreach (var value in intersects)
                {
                    System.Console.Write($"{value} ");
                }
                System.Console.WriteLine();

                // sdiff set1 set2
                RedisValue[] differences = db.SetCombine(SetOperation.Difference, "set1", "set2");

                System.Console.WriteLine($"set1与set2的差集是：");
                foreach (var value in differences)
                {
                    System.Console.Write($"{value} ");
                }
                System.Console.WriteLine();

                // sdiff set2 set1
                RedisValue[] differences2 = db.SetCombine(SetOperation.Difference, "set2", "set1");

                System.Console.WriteLine($"set2与set1的差集是：");
                foreach (var value in differences2)
                {
                    System.Console.Write($"{value} ");
                }
                System.Console.WriteLine();
            }

            #endregion

            #region sunionstore sinterstore sdiffstore command

            // 获取set的并集，交集，差集并存入到新的set中
            {
                db.SetAdd("set1", new RedisValue[] { "one", "two", "three", "four", "five" });
                db.SetAdd("set2", new RedisValue[] { "one", "six", "seven", "eight", "five" });

                // sunionstore set3 set1 set2
                long unions = db.SetCombineAndStore(SetOperation.Union, "set3", "set1", "set2");

                System.Console.WriteLine($"set3中元素有{unions}个，分别是：");
                foreach (var value in db.SetMembers("set3"))
                {
                    System.Console.Write($"{value} ");
                }
                System.Console.WriteLine();

                // sinterstore set4 set1 set2
                long intersects = db.SetCombineAndStore(SetOperation.Intersect, "set4", "set1", "set2");

                System.Console.WriteLine($"set4中元素有{intersects}个，分别是：");
                foreach (var value in db.SetMembers("set4"))
                {
                    System.Console.Write($"{value} ");
                }
                System.Console.WriteLine();

                // sdiffstore set5 set1 set2
                long differences = db.SetCombineAndStore(SetOperation.Difference, "set5", "set1", "set2");

                System.Console.WriteLine($"set5中元素有{differences}个，分别是：");
                foreach (var value in db.SetMembers("set5"))
                {
                    System.Console.Write($"{value} ");
                }
                System.Console.WriteLine();

                // sdiffstore set6 set2 set1
                long differences2 = db.SetCombineAndStore(SetOperation.Difference, "set6", "set2", "set1");

                System.Console.WriteLine($"set6中元素有{differences2}个，分别是：");
                foreach (var value in db.SetMembers("set6"))
                {
                    System.Console.Write($"{value} ");
                }
                System.Console.WriteLine();
            }

            #endregion

            #region smove command

            // 从一个set中移动一个元素到另一个set中
            {
                // smove set1 set2 one
                bool one = db.SetMove("set1", "set2", "one");
                System.Console.WriteLine(one);

                // smove set1 set2 notexist
                bool notexistvalue = db.SetMove("set1", "set2", "notexist");
                System.Console.WriteLine(notexistvalue);
                
                // smove set1 notexistkey notexist
                bool notexistkey = db.SetMove("set1", "notexistkey", "two");
                System.Console.WriteLine(notexistkey);
            }

            #endregion

        }
    }
}