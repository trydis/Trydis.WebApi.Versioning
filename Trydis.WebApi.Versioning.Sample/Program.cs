using System.Net.Http.Headers;
using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;

namespace Trydis.WebApi.Versioning.Sample
{
    class Program
    {
        static void Main()
        {
            const string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(baseAddress))
            {
                // Create HttpCient and make a request to api/values 
                var client = new HttpClient();

                var usersV1Request = new HttpRequestMessage(HttpMethod.Get, baseAddress + "users");
                var usersV1 = new MediaTypeWithQualityHeaderValue("application/vnd.versioningsample.user-v1+json");
                usersV1Request.Headers.Accept.Add(usersV1);
                var response = client.SendAsync(usersV1Request).Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.WriteLine("\n");

                var usersV2Request = new HttpRequestMessage(HttpMethod.Get, baseAddress + "users");
                var usersV2 = new MediaTypeWithQualityHeaderValue("application/vnd.versioningsample.user-v2+json");
                usersV2Request.Headers.Accept.Add(usersV2);
                response = client.SendAsync(usersV2Request).Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.WriteLine("\n");

                var userV1Request = new HttpRequestMessage(HttpMethod.Get, baseAddress + "users/1");
                var userV1 = new MediaTypeWithQualityHeaderValue("application/vnd.versioningsample.user-v1+json");
                userV1Request.Headers.Accept.Add(userV1);
                response = client.SendAsync(userV1Request).Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                Console.WriteLine("\n");

                var userV2Request = new HttpRequestMessage(HttpMethod.Get, baseAddress + "users/1");
                var userV2 = new MediaTypeWithQualityHeaderValue("application/vnd.versioningsample.user-v2+json");
                userV2Request.Headers.Accept.Add(userV2);
                response = client.SendAsync(userV2Request).Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }

            Console.ReadLine(); 
        }
    }
}
