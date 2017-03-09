using LiteDB;

namespace Kontur.GameStats.Server.DataModels
{
  public class GameServerInfo
  {
    [BsonIndex, BsonId]
    public string endpoint { get; set; }
    public GameServer gameServer { get; set; }

    public override string ToString()
    {
      return $"endpoint: {endpoint}";
    }
  }
}