using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace LanguageFeatures.Models
{
    public class MyAsyncMethods
    {
        public static Task<long?> GetPageLengths()
        {
            HttpClient client = new HttpClient();
            var httpTask = client.GetAsync("http://apress.com");
            return httpTask.ContinueWith((Task<HttpResponseMessage> antecedent) =>
            {
                return antecedent.Result.Content.Headers.ContentLength;
            });
        }

        // Using the async and await keyword
        public async static Task<long?> GetPageLengths2()
        {
            HttpClient client = new HttpClient();
            var httpMessage = await client.GetAsync("http://apress.com");
            return httpMessage.Content.Headers.ContentLength;
        }

        // Non-Asynchronous Enumerable
        public static async Task<IEnumerable<long?>> GetPageLengths3(List<string> output, params string[] urls)
        {
            List<long?> results = new List<long?>();
            HttpClient client = new HttpClient();

            foreach (string url in urls)
            {
                output.Add($"Started request for {url}");
                var httpMessage = await client.GetAsync($"http://{url}");
                results.Add(httpMessage.Content.Headers.ContentLength);
                output.Add($"Complete request for {url}");
            }
            return results;
        }

        // Asynchronous Enumerable
        public static async IAsyncEnumerable<long?> GetPageLengths4(List<string> output, params string[] urls)
        {
            HttpClient client = new HttpClient();
            foreach (string url in urls)
            {
                output.Add($"Started request for {url}");
                var httpMessage = await client.GetAsync($"http://{url}");
                output.Add($"Completed request for {url}");
                yield return httpMessage.Content.Headers.ContentLength;
            }
        }
    }
}