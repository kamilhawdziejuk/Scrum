using System;

namespace ScrumBlazor.Data
{
    public class Participant
    {
        public static double NotEstimated = Double.MaxValue;

        public int Nr { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Timer { get; set; }
        public bool Joined { get; set; }
        public bool Disabled => !Joined;
        public string Estimate { get; set; }
    }
}