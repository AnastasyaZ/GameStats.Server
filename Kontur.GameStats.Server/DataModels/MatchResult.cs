namespace Kontur.GameStats.Server.DataModels
{
    public class MatchResult
    {
        public string map { get; set; }
        public string gameModel { get; set; }
        public int fragLimit { get; set; }
        public int timeLimit { get; set; }
        public double timeElapsed { get; set; }
        public PlayersResult[] scoreboard { get; set; }
    }
}