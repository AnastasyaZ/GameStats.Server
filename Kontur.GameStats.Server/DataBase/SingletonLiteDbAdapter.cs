using System;
using System.Collections.Generic;
using Kontur.GameStats.Server.DataModels;

namespace Kontur.GameStats.Server.Database
{
    public class SingletonLiteDbAdapter : IDatabaseAdapter
    {
        private static readonly LiteDbAdapter Database;

        static SingletonLiteDbAdapter()
        {
            Database = new LiteDbAdapter("MyDatabase.db");//TODO Say No To Hardcode!!
        }

        public void AddServerInfo(GameServerInfo server) => Database.AddServerInfo(server);

        public void AddMatchInfo(MatchInfo match) => Database.AddMatchInfo(match);

        public GameServerInfo GetServerInfo(string endpoint) => Database.GetServerInfo(endpoint);

        public MatchInfo GetMatchInfo(string endpoint, DateTime timestamp) => Database.GetMatchInfo(endpoint, timestamp);

        public IEnumerable<GameServerInfo> GetServers() => Database.GetServers();

        public IEnumerable<MatchInfo> GetMatches(string endpoint) => Database.GetMatches(endpoint);

        #region Dispose

        public void Dispose()
        {
            DisposeManagedResources();
        }

        private void DisposeManagedResources()
        {
            Database.Dispose();
        }

        #endregion
    }
}