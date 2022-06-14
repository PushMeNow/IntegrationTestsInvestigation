using System;
using System.Linq.Expressions;
using System.Net.Http;
using IntegrationTestsInvestigation.ApiClient;

namespace IntegrationTestsInvestigation.Tests
{
    public class FixtureBase<TApiClient> : IDisposable
        where TApiClient : ClientBase
    {
        private static readonly Type ApiType = typeof(TApiClient);
        private static readonly Type HttpType = typeof(HttpClient);
        private static readonly Func<TApiClient> Creator = GetCreator();

        private TApiClient _client;

        public TApiClient Client => _client ?? Creator.Invoke();

        private static Func<TApiClient> GetCreator()
        {
            var http = Expression.Constant(new HttpClient());
            var newExpression = Expression.New(ApiType.GetConstructor(new[]
                                                                      {
                                                                          HttpType
                                                                      }),
                                               http);

            return Expression.Lambda<Func<TApiClient>>(newExpression)
                             .Compile();
        }

        public void Dispose()
        {
            _client?.Dispose();
        }
    }
}