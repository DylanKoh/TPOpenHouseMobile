using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using static TPOpenHouseMobile.GlobalClass;
using Newtonsoft.Json;

namespace TPOpenHouseMobile
{
    public class WebApi
    {
        HttpClient httpClient = new HttpClient();
        string url = "http://10.0.2.2:53641/";
        
        public WebApi()
        {
            //httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
        }

        public async Task<string> Create(string urlName, string JsonData)
        {
            var combinedUrl = url + urlName;
            var content = new StringContent(JsonData, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(combinedUrl, content);
            var result = response.Content.ReadAsStringAsync().Result;
            Console.WriteLine(result);
            return result;
        }

        public async Task<string> Login(string urlName)
        {
            var combinedUrl = url + urlName;
            var response = await httpClient.PostAsync(combinedUrl, new StringContent(""));
            var result = response.Content.ReadAsStringAsync().Result;
            return result;
        }
    }
}
