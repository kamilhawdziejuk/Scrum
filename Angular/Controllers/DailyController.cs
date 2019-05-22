using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using Scrum.Models;

namespace Scrum.Controllers
{
    [Route("api/[controller]")]
    public class DailyController : Controller
    {
        public DailyController()
        {
            List = ReadParticipantsFromFile();
        }

        private List<Participant> ReadParticipantsFromFile()
        {
            var list = new List<Participant>();
            using (StreamReader sr = new StreamReader("participants.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    list.Add(new Participant() { Name = line, Timer = 0 });
                }
            }
            return list;
        }

        [HttpGet("[action]")]
        public IEnumerable<Participant> RandomizeParticipants()
        {
            Shuffle<Participant>(List);
            return List;
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
