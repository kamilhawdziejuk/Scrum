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

        public double Estimation { get; set; }

        public bool Disabled
        {
            get { return this.Estimation.Equals(NotEstimated); }
        }

        public string EstimationStr
        {
            get { return Disabled ? "Not Estimated" : this.Estimation.ToString(); }
            set
            {
                try
                {
                    this.Estimation = Convert.ToDouble(value);
                }
                catch (Exception ex)
                {

                }
            }
        }
    }
}