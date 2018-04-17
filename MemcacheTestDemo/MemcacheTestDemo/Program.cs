using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;
using Enyim.Caching.Memcached.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemcacheTestDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            MemcachedClientConfiguration mcConfig = new MemcachedClientConfiguration();
         // mcConfig.AddServer("119.29.176.32:11111");
            //配置多个服务器
           mcConfig.AddServer("127.0.0.1:11211");
           mcConfig.NodeLocatorFactory = new KetamaNodeLocatorFactory();
            using (MemcachedClient client = new MemcachedClient(mcConfig))
            {
                Random rd = new Random();
               var exprTimeS =  rd.Next(4,9);
                //多服务器问题
                for (int i = 0; i < 20; i++)
                {
                    client.Store(Enyim.Caching.Memcached.StoreMode.Set, "textaaa" + i, "valueaaa" + i, TimeSpan.FromSeconds(exprTimeS));
                }
                for (int i = 0; i < 20; i++)
                {
                    var val = client.Get<string>("textaaa" + i);
                    Console.WriteLine(val);
                }
               

                /*存储单个数据*/
                //client.Store(Enyim.Caching.Memcached.StoreMode.Set, "name", "haiyi", TimeSpan.FromSeconds(30));
                //var name = client.Get<string>("name");
                //Console.WriteLine(name);

                /*存储对象*/
                //client.Store(Enyim.Caching.Memcached.StoreMode.Set,
                //    "person",
                //    new Person() { Id=1,Name="haiyi",Age=19});



                //var p = client.Get<Person>("person");
                //Console.WriteLine(p.Id+" ; "+p.Name+" ; "+p.Age);

                //if (client.Remove("person"))
                //{
                //    Console.WriteLine("删除成功");
                //}
                //
                //计数器 有问题代码 Redis好一些，不用这个
                //client.Store(Enyim.Caching.Memcached.StoreMode.Set, "watch1", 2);
                //IMutateOperationResult res = client.ExecuteIncrement("watch1", 9, 3);
                //if (res.Exception!=null)
                //{
                //    Console.WriteLine(res.Exception.Message);
                //}
                //Console.WriteLine(res.StatusCode+ "  "+res.Success+"  "+res.InnerResult+" "+res.Value );


                /*调试  模式*/
                //IStoreOperationResult result = client.ExecuteStore(Enyim.Caching.Memcached.StoreMode.Set, "name", "haiyi", TimeSpan.FromSeconds(30));
                //Console.WriteLine( result.StatusCode+"  is success: "+result.Success+ "  InnerResult" + result.InnerResult);
                //  var getResult =  client.ExecuteGet<string>("name");
                //Console.WriteLine(getResult.InnerResult+"  statuCode:"+getResult.StatusCode+"  success:"+getResult.Success+ "  value=" +getResult.Value); //+getResult.Exception.Message

                //cas处理并发问题(乐观锁)
                /*
                if(client.Store(Enyim.Caching.Memcached.StoreMode.Set, "gender", "男"))
                {
                    CasResult<string> casRes = client.GetWithCas<string>("gender");
                    Console.WriteLine(" 获取到了：cas="+ casRes.Cas+ "  status："+casRes.StatusCode+ " Result=" + casRes.Result);
                    Console.WriteLine("点击任意键修改这个gender");
                    Console.ReadLine();
                  
                  var res =  client.Cas(StoreMode.Set, "gender","aaa",casRes.Cas);
                    if (res.Result)
                    {
                        Console.WriteLine("成功");
                    }
                    else
                    {
                        Console.WriteLine("被别人改了");
                    }
                }
                Console.Read();
                */
            }
            Console.Read();
        }
    }
}
