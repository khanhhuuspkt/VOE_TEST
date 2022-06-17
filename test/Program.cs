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
        public static void PractiseApp(Root myDeserializedClass, int windSpeedLimit, int uvIndexLimit)
        {
            try
            {
                if (IsRainCheck(myDeserializedClass))
                {
                    Console.WriteLine("Output : you should not go outside, It's raining");
                }
                else
                {
                    Console.WriteLine("Output : you should go outside, The weather is well");
                    if (myDeserializedClass.current.wind_speed > windSpeedLimit)
                    {
                        Console.WriteLine("Output : You can fly your kite");
                    }
                }
                if (myDeserializedClass.current.uv_index > uvIndexLimit)
                {
                    Console.WriteLine("Output : don't foget to wear The sunscreen");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public static bool IsRainCheck(Root myDeserializedClass)
        {
            return Regex.IsMatch(myDeserializedClass.current.weather_descriptions[0], ".*rain.*", RegexOptions.IgnoreCase);
        }

        static void Main(string[] args)
        {
            string serviceUrl = "http://api.weatherstack.com/current?access_key=610acf4c1d203448cd6f671955c5e8aa&query=30076";
            int windSpeedLimit = 15;
            int uvIndexLimit = 3;
            try
            {
                var htmltask = ApiGetHandle.GetApiContent(serviceUrl);
                htmltask.Wait(); // to join the getting API thread
                var myDeserializedClass = JsonDeserializingConvert.ConvertDe(htmltask.Result);// to save api response string data to myDeserializedClass
                Program.PractiseApp(myDeserializedClass, windSpeedLimit, uvIndexLimit);// main behaviors
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
