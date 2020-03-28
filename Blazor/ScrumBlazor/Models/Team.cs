using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScrumBlazor.Data
{
    public class Team
    {
        [Key]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string TaskName { get; set; }

        public List<Member> Members { get; set; }

        public DateTime CreatedTime { get; set; }

        public int DailyAmount { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}