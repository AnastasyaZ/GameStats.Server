using System;
using Kontur.GameStats.Server.DataModels.Utilitys;

namespace Kontur.GameStats.Server.DataModels
{
  public class GameServer:IEquatable<GameServer>
  {
    public string name { get; set; }
    public string[] gameModes { get; set; }

    #region equality members

    public bool Equals(GameServer other)
    {
      if (ReferenceEquals(null, other)) return false;
      if (ReferenceEquals(this, other)) return true;
      if(!string.Equals(name, other.name)) return false;
      return Helpers.CompareLists(gameModes, other.gameModes);
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
        return ((name != null ? name.GetHashCode() : 0)*397) ^ (gameModes != null ? gameModes.GetHashCode() : 0);
      }
    }

    #endregion
  }
}