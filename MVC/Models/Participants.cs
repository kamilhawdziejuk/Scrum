using System;
using System.Collections;
using System.Collections.Generic;

namespace ScrumMvc.Models
{
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
