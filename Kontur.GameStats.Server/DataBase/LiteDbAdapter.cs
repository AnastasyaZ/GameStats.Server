using System;
using System.Collections.Generic;
using Kontur.GameStats.Server.DataModels;
using LiteDB;

namespace Kontur.GameStats.Server.Database
{
  public sealed class LiteDbAdapter : IDatabaseAdapter
  {
    private readonly LiteDatabase database;

    private LiteCollection<GameServerInfo> servers => database.GetCollection<GameServerInfo>();
    private LiteCollection<MatchInfo> matches => database.GetCollection<MatchInfo>();

    public LiteDbAdapter(string filename)
    {
      database = new LiteDatabase(filename);
    }

    public void AddServerInfo(GameServerInfo server)
    {
        using (var tr = database.BeginTrans())
        {
          servers.Upsert(server.endpoint, server);
          tr.Commit();
        }
    }

    public void AddMatchInfo(MatchInfo match)
    {
      throw new NotImplementedException();
    }

    public GameServerInfo GetServerInfo(string endpoint)
    {
      return servers.FindOne(x => x.endpoint == endpoint);
    }

    public MatchInfo GetMatchInfo(string endpoint, DateTime timestamp)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<GameServerInfo> GetServers()
    {
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