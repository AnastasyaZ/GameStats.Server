using System;

namespace Kontur.GameStats.Server.DataModels
{
    public class GameServerInfo : IEquatable<GameServerInfo>
    {
        public string endpoint { get; set; }
        public GameServer gameServer { get; set; }

        #region equality members

        public bool Equals(GameServerInfo other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(endpoint, other.endpoint) && gameServer.Equals(other.gameServer);
        }

        #endregion
    }
}