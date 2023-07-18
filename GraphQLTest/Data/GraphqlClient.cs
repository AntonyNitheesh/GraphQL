using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;

namespace GraphQLTest.Data
{
    public class GraphqlClient
    {
        public readonly GraphQLHttpClient _graphQLHttpClient;
        public GraphqlClient()
        {
            if (_graphQLHttpClient == null)
            {
                _graphQLHttpClient = GetGraphQlApiClient();
            }
        }

        public GraphQLHttpClient GetGraphQlApiClient()
        {
            var endpoint = "https://antony-graphql-test.hasura.app/v1/graphql";

            var httpClientOption = new GraphQLHttpClientOptions
            {
                EndPoint = new Uri(endpoint)
            };
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("x-hasura-admin-secret", "TBR5O2VgSMqqtY3TwSNp6M0FWWi9zx3Viu9veGpTvXfR6l1igohqymlBFuu8nl8k");
            return new GraphQLHttpClient(httpClientOption, new NewtonsoftJsonSerializer(), httpClient);
        }
    }
}
