using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

namespace ConsoleApp3
{
    internal class WorkflowStep
    {
        public int order { get; set; }
        public string title { get; set; }
    }

    internal class Workflow
    {
        public string title { get; set; }
        public List<WorkflowStep> workflow_steps { get; set; }
    }

    internal class Data
    {
        public List<Workflow> workflow { get; set; }
    }

    internal class RootObject
    {
        public Data data { get; set; }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            RootObject myObject;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://vxrcontent.s3-website.us-east-2.amazonaws.com");
                client.DefaultRequestHeaders.Add("User-Agent", "Anything");
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync("coffeeapp/json/coffee-app-workflow.json").Result;
                response.EnsureSuccessStatusCode();
                var stringBody = response.Content.ReadAsStringAsync().Result;
                myObject = JsonConvert.DeserializeObject<RootObject>(stringBody);
            }

            Console.WriteLine(myObject.ToString());
            Console.ReadLine();
        }
    }
}
