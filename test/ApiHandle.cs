using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace practise
{
    class ApiSetHandle
    {
        public static async Task<string> GetWebContent(string url)
        {
            using (var httpClient = new HttpClient())
            {
                Console.WriteLine($"Starting connect {url}");
                try
                {
                    HttpResponseMessage response = await httpClient.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine($"Get API succcessfully - statusCode {(int)response.StatusCode} {response.ReasonPhrase}");
                        Console.WriteLine($"Starting read data");
                        string htmltext = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Got {htmltext.Length} charaters");
                        return htmltext;
                    }
                    else
                    {
                        Console.WriteLine($"Error - statusCode {response.StatusCode} {response.ReasonPhrase}");
                        return null;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
        }
    }
}
