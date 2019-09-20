using System;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAsync
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("отправлено на загрузку");
            await MainAsync();
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
                var str1 = Aclient.DownloadAsync("http://ftp.byfly.by/test/100Mb.txt", token);
                var str2 = Aclient.DownloadAsync("http://ftp.byfly.by/test/100Mb.txt", token);
                var str3 = Aclient.DownloadAsync("http://ftp.byfly.by/test/100Mb.txt", token);
                WaitCancelKey(cts);

                Console.WriteLine(str1.GetAwaiter().GetResult());
                Console.WriteLine(str2.GetAwaiter().GetResult());
                Console.WriteLine(str3.GetAwaiter().GetResult());
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
                    var key = Console.ReadKey();
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