using System;
using System.Collections.Generic;
using Kontur.GameStats.Server.DataModels;
using LiteDB;

namespace Kontur.GameStats.Server.Database
{
    public sealed class LiteDbAdapter : IDatabaseAdapter
    {
        private readonly LiteDatabase database;

        public LiteDbAdapter(string filename)
        {
            database = new LiteDatabase(filename);
        }

        public void AddServerInfo(GameServerInfo server)
        {
            var servers = database.GetCollection<GameServerInfo>("GameServerInfo"); //TODO remove hardcode
            servers.Insert(server);
        }

        public void AddMatchInfo(MatchInfo match)
        {
            throw new NotImplementedException();
        }

        public GameServerInfo GetServerInfo(string endpoint)
        {
            var servers = database.GetCollection<GameServerInfo>("GameServerInfo"); //TODO remove hardcode
            return servers.FindOne(x => x.endpoint == endpoint);
        }

        public MatchInfo GetMatchInfo(string endpoint, DateTime timestamp)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GameServerInfo> GetServers()
        {
            var servers = database.GetCollection<GameServerInfo>("GameServerInfo"); //TODO remove hardcode
            return servers.FindAll();
        }

        public IEnumerable<MatchInfo> GetMatches(string endpoint)
        {
            throw new NotImplementedException();
        }

        #region Dispose

        public void Dispose()
        {
            DisposeManagedResources();
        }

        private void DisposeManagedResources()
        {
            database.Dispose();
        }

        #endregion
    }
}