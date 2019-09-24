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

                Console.WriteLine(await str1);
                Console.WriteLine(await str2);
                Console.WriteLine(await str3);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static void WaitCancelKey(CancellationTokenSource cts)
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
            Task.Run(work);
        }
    }
}