using System.Linq;

namespace Kontur.GameStats.Server.DataModels
{
  public static class ComparisonExtensions
  {
    public static bool Equal(this GameServerInfo target, GameServerInfo other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(target, other)) return true;
      return string.Equals(target.endpoint, other.endpoint)/* && target.gameServer.Equal(other.gameServer)*/;
    }

    public static bool Equal(this GameServer target, GameServer other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(target, other)) return true;
      if (!string.Equals(target.name, other.name)) return false;
      return target.gameModes.Zip(other.gameModes, string.Equals).All(x => x);
    }
  }
}