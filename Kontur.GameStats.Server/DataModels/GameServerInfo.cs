using LiteDB;

namespace Kontur.GameStats.Server.DataModels
{
  public class GameServerInfo
  {
    [BsonIndex]
    public string endpoint { get; set; }
    public GameServer gameServer { get; set; }
  }
}