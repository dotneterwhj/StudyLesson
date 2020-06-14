using IdentityModel.Client;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ConsoleClinet
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var client = new HttpClient();

            var diso = await client.GetDiscoveryDocumentAsync("http://localhost:5000");

            if (diso.IsError)
            {
                Console.WriteLine(diso.Error);
                return;
            }

            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = diso.TokenEndpoint,
                ClientId = "client",

                ClientSecret = "511536EF-F270-4058-80CA-1C89C192F69A",

                Scope = "api1"

            })
            ;

            if(tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            var httpClient = new HttpClient();

            httpClient.SetBearerToken(tokenResponse.AccessToken);

            var response = await httpClient.GetAsync("http://localhost:6001/weatherforecast");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine(response.StatusCode);
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();

                Console.WriteLine(content);
            }

            Console.Read();
        }
    }
}
