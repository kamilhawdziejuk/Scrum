using System;

namespace ScrumBlazor.Data
{
    public class Participant
    {
        public int Nr { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Timer { get; set; }

        public double Estimation { get; set; }
    }
}