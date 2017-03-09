using System;
using LiteDB;

namespace Kontur.GameStats.Server.DataModels
{
  public class GameServerInfo:IEquatable<GameServerInfo>
  {
    [BsonIndex, BsonId]
    public string endpoint { get; set; }
    public GameServer gameServer { get; set; }

    #region equality members

    public bool Equals(GameServerInfo other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(endpoint, other.endpoint) && gameServer.Equals(other.gameServer);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((GameServerInfo) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return ((endpoint != null ? endpoint.GetHashCode() : 0)*397) ^ (gameServer != null ? gameServer.GetHashCode() : 0);
      }
    }

    #endregion
  }
}