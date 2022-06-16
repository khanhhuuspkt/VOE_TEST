using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace HttpClientExample
{

    class Program
    {
        // Tải về và hiện thị thông tin trang tải về, 
        // url là địa chỉ cần tải ví dụ: https://google.com.vn 
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
                        Console.WriteLine($"Tải thành công - statusCode {(int)response.StatusCode} {response.ReasonPhrase}");


                        Console.WriteLine("Starting read data");

                        // Đọc nội dung content trả về
                        string htmltext = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Nhận được {htmltext.Length} ký tự");
                        Console.WriteLine();
                        return htmltext;
                    }
                    else
                    {
                        Console.WriteLine($"Lỗi - statusCode {response.StatusCode} {response.ReasonPhrase}");
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
        static void Main(string[] args)
        {
            try
            {
                var htmltask = GetWebContent("http://api.weatherstack.com/current?access_key=610acf4c1d203448cd6f671955c5e8aa&query=30076");
                htmltask.Wait(); // Chờ tải xong
                                 // Hoặc wait htmltask; nếu chuyển Main thành async 
                var myDeserializedClass = JsonConvert.DeserializeObject<Root>(htmltask.Result);
                Console.WriteLine(myDeserializedClass.current.observation_time);

                //var html = htmltask.Result;
                //Console.WriteLine(html != null ? html.Substring(0, 255) : "Lỗi");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
