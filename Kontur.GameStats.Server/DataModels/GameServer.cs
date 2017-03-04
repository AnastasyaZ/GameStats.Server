using System;

namespace Kontur.GameStats.Server.DataModels
{
    public class GameServer : IEquatable<GameServer>
    {
        public string name { get; set; }
        public string[] gameModes { get; set; }

        #region equality members

        public bool Equals(GameServer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(name, other.name) && Equals(gameModes, other.gameModes);
        }

        #endregion
    }
}