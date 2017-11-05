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

        public List<Participant> List { get; set; }
    }
}
