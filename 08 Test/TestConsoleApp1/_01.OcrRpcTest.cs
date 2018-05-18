using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CookComputing.XmlRpc;

namespace TestConsoleApp1
{
    public class _01OcrRpcTest
    {
        public bool Send()
        {

            string server = "192.168.102.188";
            string port = "8888";
            string user = "share";
            string pwd = "Test123";


            Stopwatch sw = new Stopwatch();
            sw.Start();
            IStateName proxy = (IStateName)XmlRpcProxyGen.Create(typeof(IStateName));
            sw.Stop();
            Console.WriteLine($"连接耗时:{sw.Elapsed}");

            var paht = $@"\\192.168.102.188\share\testx\AI201701010";
            if (!Directory.Exists(paht))
            {
                Directory.CreateDirectory(paht);
            }


            sw.Reset();
            sw.Start();
            Parallel.For(1, 17, i =>
            {
                Console.WriteLine(i);
                File.Copy($@"C:\Users\sh179\Desktop\testx\{i}.jpg", $@"\\192.168.102.188\share\testx\AI201701010\{i}.jpg", true);
                string message = proxy.getTest($"{i}.jpg");
                Console.WriteLine($"{i} return:{message}");
            });



            //string message = proxy.getTest("123.jpg");
            //Console.WriteLine($"return:{message}");


            sw.Stop();
            Console.WriteLine($"发送耗时:{sw.Elapsed}");


            return false;
        }

    }




    [XmlRpcUrl("http://192.168.102.188:8888")]
    public interface IStateName : IXmlRpcProxy
    {
        [XmlRpcMethod("getTest")]
        string getTest(string name);
    }


}


