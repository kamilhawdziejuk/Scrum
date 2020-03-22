using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ScrumBlazor.Data
{
    public class DailyService
    {
        public DailyService()
        {
            //List = ReadParticipantsFromFile();
            List = new List<Participant>();
            List.Add(new Participant() { Name = "test user", Timer = 2});
        }

        public async Task<Participant[]> GetParticipants(DateTime startDate)
        {
            var rng = new Random();
            return List.ToArray();
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