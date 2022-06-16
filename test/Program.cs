using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace practise
{

    class Program
    {

        public static void TestApp(Root myDeserializedClass)
        {
            try
            {
                if (Regex.IsMatch(myDeserializedClass.current.weather_descriptions[0], ".*rain.*", RegexOptions.IgnoreCase))
                {
                    Console.WriteLine("Output : you cannot go outside, It's raining");
                }
                else
                {
                    Console.WriteLine("Output : you can go outside, The weather is well");
                    if (myDeserializedClass.current.wind_speed > 15)
                    {
                        Console.WriteLine("Output : You can fly your kite");
                    }
                }
                if (myDeserializedClass.current.uv_index > 3)
                {
                    Console.WriteLine("Output : don't foget to wear The sunscreen");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static void Main(string[] args)
        {
            string serviceUrl = "http://api.weatherstack.com/current?access_key=610acf4c1d203448cd6f671955c5e8aa&query=30076";
            try
            {
                var htmltask = ApiSetHandle.GetWebContent(serviceUrl);
                htmltask.Wait();
                var myDeserializedClass = JsonConvert.DeserializeObject<Root>(htmltask.Result);
                Program.TestApp(myDeserializedClass );
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
