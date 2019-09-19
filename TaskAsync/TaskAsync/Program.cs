using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAsync
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("отправлено на загрузку");
            MainAsync().GetAwaiter().GetResult();
            Console.WriteLine("end");
            Console.ReadLine();
        }

        static async Task MainAsync()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            CancellationToken token = cts.Token;
            AsyncWebClient Aclient = new AsyncWebClient();
            try
            {
                var str1 =await Aclient.DownloadAsync("http://ftp.byfly.by/test/", token);
                var str2 =await Aclient.DownloadAsync("http://ftp.byfly.by/test/", token);
                var str3 =await Aclient.DownloadAsync("http://ftp.byfly.by/test/", token);
                await WaitCancelKey(cts);
                Console.WriteLine(str1);
                Console.WriteLine(str2);
                Console.WriteLine(str3);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static async Task WaitCancelKey(CancellationTokenSource cts)
        {
            Console.WriteLine("Press b to stop");
            bool flag = true;
            Action work = () =>
            {
                do
                {
                    var key = Console.ReadKey(true);
                    if (key.KeyChar == 'b')
                    {
                        flag = false;
                        cts.Cancel();
                    }
                } while (flag);
            };
            await Task.Run(work);
        }
    }
}