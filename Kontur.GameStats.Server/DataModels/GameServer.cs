using System;
using LiteDB;

namespace Kontur.GameStats.Server.DataModels
{
  public class GameServer:IEquatable<GameServer>
  {
    [BsonIndex, BsonId]
    public string endpoint { get; set; }
    public ServerInfo info { get; set; }

    #region equality members

    public bool Equals(GameServer other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      return string.Equals(endpoint, other.endpoint) && info.Equals(other.info);
    }

    public override bool Equals(object obj)
    {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((GameServer) obj);
    }

    public override int GetHashCode()
    {
      unchecked
      {
        return ((endpoint != null ? endpoint.GetHashCode() : 0)*397) ^ (info != null ? info.GetHashCode() : 0);
      }
    }

    #endregion
  }
}