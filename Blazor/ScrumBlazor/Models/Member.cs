using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrumBlazor.Data
{
    public class Member
    {
        public Guid Id { get; set; }

        [ForeignKey("Id")]
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
        public double StoryPoint { get; set; }

        public int SummaryTime { get; set; }

        public int DailyAmount { get; set; }
    }
}