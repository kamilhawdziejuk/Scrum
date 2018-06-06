using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Scrum.Models;

namespace Scrum.Controllers
{
    [Route("api/[controller]")]
    public class PokerController : Controller
    {
        public PokerController()
        {
            List = new List<PokerValue>();
            var participants = new List<string>() { "Arek", "Hubert", "Kamil", "Michal", "Lukasz", "Steve" };
            foreach (var p in participants)
            {
                List.Add(new PokerValue() { Name = p, Points = 0 });
            }
        }

        public List<PokerValue> List { get; set; }

        [HttpGet("[action]")]
        public IEnumerable<PokerValue> GetPokerValues()
        {
            return List;
        }
    }
}