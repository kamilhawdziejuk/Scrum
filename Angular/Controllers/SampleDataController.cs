using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace Scrum.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Antra", "Steve", "Hubert", "Lukasz", "Michal", "Arek", "Kamil"
        };

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var part = new Participants();
            part.Randomize();

            IList<WeatherForecast> results = new List<WeatherForecast>();
            foreach (var elem in part.List)
            {
                results.Add(new WeatherForecast() { Name = elem.Name, Summary = "summary" });
            }
            return results;

            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    //DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
            //    //TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //});

            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            //{
            //    //DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
            //    //TemperatureC = rng.Next(-20, 55),
            //    Summary = Summaries[rng.Next(Summaries.Length)]
            //});
        }

        public class WeatherForecast
        {
            //public string DateFormatted { get; set; }
            //public int TemperatureC { get; set; }

            public string Name { get; set; }

            public string Summary { get; set; }

            //public int TemperatureF
            //{
            //    get
            //    {
            //        return 32 + (int)(TemperatureC / 0.5556);
            //    }
            //}
        }

        public class Participant
        {
            public string Name;
        }

        public class Participants
        {
            public Participants()
            {
                List = new List<Participant>();
                var participants = new List<string>() { "Antra", "Arek", "Hubert", "Kamil", "Michal", "Lukasz", "Steve" };
                foreach (var p in participants)
                {
                    List.Add(new Participant() { Name = p });
                }
            }

            public void Randomize()
            {
                Shuffle<Participant>(List);
            }

            public List<Participant> List { get; set; }

            private static Random rng = new Random();

            private void Shuffle<T>(IList<T> list)
            {
                int n = list.Count;
                while (n > 1)
                {
                    n--;
                    int k = rng.Next(n + 1);
                    T value = list[k];
                    list[k] = list[n];
                    list[n] = value;
                }
            }
        }
    }
}
