using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace ScrumBlazor.Data
{
    public class DailyService
    {
        public async Task<Participant[]> GetParticipants(Team team, bool shuffle = true)
        {
            var list = new List<Participant>();
            if (team?.Members != null && team.Members.Count > 0)
            {
                for (int i = 0; i < team.Members.Count; i++)
                {
                    Member member = team.Members[i];
                    var p = new Participant()
                    {
                        Name = member.Name,
                        Nr = i,
                        Timer = 0,
                        Id = member.Id,
                        Estimate = member.Estimate,
                        Joined = member.StoryPoint != 0
                    };
                    list.Add(p);
                }

                if (shuffle)
                {
                    Shuffle<Participant>(list);
                }
                
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Nr = i;
                }
            }

            return list.ToArray();
        }

        public string GetQuestionOfADay()
        {
            var questions = new List<string>();
            questions.Add("What is the character from Friends movie that you identify most?");
            questions.Add("Cats or dogs? Why?");
            questions.Add("Where do you come from? Where did you grow up?");
            questions.Add("One thing that not many people know about me is..?");
            questions.Add("Who would you like to go with on a dinner with?");
            questions.Add("How do you like to spend your free time?");
            questions.Add("What does make you calm?");
            questions.Add("How does your dream day look like?");
            questions.Add("The strangest thing you have eaten is..");
            questions.Add("Which tool do you like to use in your job?");
            questions.Add("If you would have unlimited amount of money - how would you use it?");
            questions.Add("At what website do you waste too much time?");
            questions.Add("How many open tabs do you have currently in your browser?");
            questions.Add("If you have time machine - which time would you get back to?");
            questions.Add("What title would have a movie about you?");
            questions.Add("What music do you like to listen?");
            questions.Add("Which superpower would you like to have?");
            questions.Add("What features do you value from the other person?");
            questions.Add("What great book would you recommend?");
            questions.Add("Whats on your bucket list?");
            questions.Add("What’s your favorite movie?");
            questions.Add("What would people be surprised to know about you?");
            questions.Add("What’s your favorite vacation spot and why?");
            questions.Add("What do you wish you’d learned a long time before you actually learned it?");
            questions.Add("What’s your favorite hobby?");
            questions.Add("What’s your favorite way to spend time outside?");
            questions.Add("What’s the most valuable career advice you’ve ever received?");
            questions.Add("What movie could you watch over and over again and never get tired of?");
            questions.Add("If you didn’t work, how would you spend your time?");
            questions.Add("What's the furthest away from home you've ever been?");
            Random rand = new Random();
            int index = rand.Next(0, questions.Count);
            return questions[index];
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