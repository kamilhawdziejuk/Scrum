using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Scrum.Models;

namespace Scrum.Controllers
{
    public class PokerController
    {
        [HttpGet("[action]")]
        public IEnumerable<PokerValue> GetPokerValues()
        {
            return List;
        }

        public List<PokerValue> List { get; set; }
    }
}
