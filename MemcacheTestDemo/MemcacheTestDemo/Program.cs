using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached.Results;
using System;
using System.Collections.Generic;
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
            mcConfig.AddServer("119.29.176.32:11111");
            using (MemcachedClient client =new MemcachedClient(mcConfig))
            {
                client.Store(Enyim.Caching.Memcached.StoreMode.Set, "name", "haiyi",TimeSpan.FromSeconds(30));
                   var name =  client.Get<string>("name");
                 Console.WriteLine(name);
                /*调试  模式*/
                //IStoreOperationResult result = client.ExecuteStore(Enyim.Caching.Memcached.StoreMode.Set, "name", "haiyi", TimeSpan.FromSeconds(30));
                //Console.WriteLine( result.StatusCode+"  is success: "+result.Success+ "  InnerResult" + result.InnerResult);
                //  var getResult =  client.ExecuteGet<string>("name");
                //Console.WriteLine(getResult.InnerResult+"  statuCode:"+getResult.StatusCode+"  success:"+getResult.Success+ "  value=" +getResult.Value); //+getResult.Exception.Message
            }
            Console.Read();
        }
    }
}
