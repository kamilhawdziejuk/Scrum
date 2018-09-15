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
            var participants = new List<string>() { "Arek", "Hubert", "Kamil", "Michal", "Lukasz", "Steve", "Piotr" };
            if (List == null)
            {
                List = new List<PokerValue>();
                foreach (var p in participants)
                {
                    List.Add(new PokerValue() { name = p, points = 0, editable = true });
                }
            }
        }

        public static List<PokerValue> List { get; set; }

        [HttpGet("[action]")]
        public IEnumerable<PokerValue> GetPokerValues()
        {
            return List;
        }

        [HttpPost("[action]")]
        public ActionResult SetPokerValue([FromBody] PokerValue someVar)
        {
            if (someVar != null)
            {
                Update(someVar);
                return Json("Success");
            }
            else
            {
                return Json("An Error Has occoured");
            }
        }

        private void Update(PokerValue input)
        {
            foreach (var elem in List)
            {
                if (elem.name != input.name) continue;
                elem.points = input.points;
                break;
            }
        }
    }
}