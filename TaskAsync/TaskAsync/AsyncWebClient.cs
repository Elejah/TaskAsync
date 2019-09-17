using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace TaskAsync
{
    class AsyncWebClient : IAsyncWebClient
    {
        public async Task<string> DownloadAsync(string url, CancellationToken ct)
        {
            var client = new WebClient();
            using (ct.Register(() => client.CancelAsync()))
            {
                var data = await DownloadTaskAsync(client, url, ct);

                return data;
            }
        }

        public Task<string> DownloadTaskAsync(WebClient client, string url, CancellationToken ct)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            client.DownloadStringCompleted += (ctk, e) =>
                {
                    if (e.Cancelled)
                    {
                        tcs.TrySetCanceled();
                    }
                    if (e.Error != null)
                    {
                        tcs.TrySetException(e.Error);
                    }
                    else tcs.TrySetResult(e.Result);
                };
            client.DownloadStringAsync(new Uri(url),ct);

            return tcs.Task;
        }
    }
}