using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScrumBlazor.Data
{
    public class Member
    {
        public int Id { get; set; }

        [ForeignKey("Id")]
        public Guid TeamId { get; set; }
        public string Name { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}