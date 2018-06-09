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
                List.Add(new PokerValue() { name = p, points = 0, editable = true});
            }
        }

        public List<PokerValue> List { get; set; }

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
                return Json("Success");
            }
            else
            {
                return Json("An Error Has occoured");
            }
        }
    }
}