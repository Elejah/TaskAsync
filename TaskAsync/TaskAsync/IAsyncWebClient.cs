using System.Threading;
using System.Threading.Tasks;

namespace TaskAsync
{
    interface IAsyncWebClient
    {
        Task<string> DownloadAsync(string url, CancellationToken ct);
    }
}