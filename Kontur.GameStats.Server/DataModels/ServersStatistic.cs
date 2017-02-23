namespace Kontur.GameStats.Server.DataModels
{
    public class ServersStatistic
    {
        public int totalMatchesPlayed { get; set; }
        public int maximumMatchesPerDay { get; set; }
        public double averageMatchesPerDay { get; set; }
        public int maximumPopulation { get; set; }
        public double averagePopulation { get; set; }
        public string[] Top5GameModels { get; set; }
        public string[] top5Maps { get; set; }
    }
}