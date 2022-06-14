using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace IntegrationTestsInvestigation.ApiClient
{
    public abstract class ClientBase : IDisposable
    {
        private readonly HttpClient _client;
        
        protected ClientBase()
        {
            _client = new HttpClient();
        }

        public Task<HttpRequestMessage> CreateHttpRequestMessageAsync(CancellationToken token)
        {
            return Task.FromResult(new HttpRequestMessage());
        }

        public Task<HttpClient> CreateHttpClientAsync(CancellationToken token)
        {
            return Task.FromResult(_client);
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}