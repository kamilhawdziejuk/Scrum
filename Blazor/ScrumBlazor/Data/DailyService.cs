using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ScrumBlazor.Data
{
    public class DailyService
    {
        public async Task<Participant[]> GetParticipants(Team team)
        {
            var list = new List<Participant>();
            if (team != null && team.Members != null && team.Members.Count > 0)
            {
                for (int i = 0; i < team.Members.Count; i++)
                {
                    Participant p = new Participant()
                    {
                        Name = team.Members[i].Name,
                        Nr = i,
                        Timer = 0,
                        Id = team.Members[i].Id,
                    };
                    list.Add(p);
                }

                Shuffle<Participant>(list);

                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Nr = i;
                }
            }

            return list.ToArray();
        }


        [HttpGet("[action]")]
        public IEnumerable<Participant> RandomizeParticipants()
        {
            Shuffle<Participant>(List);
            return List;
        }

        public List<Participant> List { get; set; }

        private static readonly Random rng = new Random();

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