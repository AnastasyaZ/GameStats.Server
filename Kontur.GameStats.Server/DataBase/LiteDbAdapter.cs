using System;
using System.Collections.Generic;
using Kontur.GameStats.Server.DataModels;
using LiteDB;

namespace Kontur.GameStats.Server.Database
{
  public sealed class LiteDbAdapter : IDatabaseAdapter
  {
    private readonly LiteDatabase database;

    private LiteCollection<GameServerInfo> getServers => database.GetCollection<GameServerInfo>("GameServerInfo"); //TODO remove hardcode
    private LiteCollection<MatchInfo> getMatches => database.GetCollection<MatchInfo>("MatchInfo");

    private readonly object lockObj = new object();

    public LiteDbAdapter(string filename)
    {
      database = new LiteDatabase(filename);
    }

    public void AddServerInfo(GameServerInfo server)
    {
      lock (lockObj)
      {
        var servers = getServers;
        servers.Delete(x => x.endpoint == server.endpoint);
        servers.Insert(server);
      }
    }

    public void AddMatchInfo(MatchInfo match)
    {
      throw new NotImplementedException();
    }

    public GameServerInfo GetServerInfo(string endpoint)
    {
      return getServers.FindOne(x => x.endpoint == endpoint);
    }

    public MatchInfo GetMatchInfo(string endpoint, DateTime timestamp)
    {
      throw new NotImplementedException();
    }

    public IEnumerable<GameServerInfo> GetServers()
    {
      return getServers.FindAll();
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