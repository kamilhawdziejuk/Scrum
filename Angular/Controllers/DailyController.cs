﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace Scrum.Controllers
{
    [Route("api/[controller]")]
    public class DailyController : Controller
    {
        public DailyController()
        {
            List = new List<Participant>();
            var participants = new List<string>() { "Antra", "Arek", "Hubert", "Kamil", "Michal", "Lukasz", "Steve" };
            foreach (var p in participants)
            {
                List.Add(new Participant() { Name = p });
            }
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

        public class Participant
        {
            public string Name { get; set; }
            public string Summary { get; set; }
        }
    }
}