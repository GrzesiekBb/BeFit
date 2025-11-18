namespace BeFit.Models
{
    public class ExerciseStatsViewModel
    {
        public string ExerciseName { get; set; } = string.Empty;
        public int ExecutionCount { get; set; }

      
        public int TotalReps { get; set; }

        
        public double AverageLoad { get; set; }

       
        public double MaxLoad { get; set; }
    }
}
