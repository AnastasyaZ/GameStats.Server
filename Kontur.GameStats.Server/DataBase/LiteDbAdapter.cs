using System;
using System.Collections.Generic;
using Kontur.GameStats.Server.DataModels;
using LiteDB;

namespace Kontur.GameStats.Server.Database
{
  public sealed class LiteDbAdapter : IDatabaseAdapter, IDisposable
  {
    private readonly LiteDatabase database;

    private LiteCollection<GameServerInfo> servers => database.GetCollection<GameServerInfo>();
    private LiteCollection<MatchInfo> matches => database.GetCollection<MatchInfo>();

    public LiteDbAdapter(string filename)
    {
      database = new LiteDatabase(filename);
    }

    public void UpsertServerInfo(GameServerInfo server)
    {
        using (var tr = database.BeginTrans())
        {
          servers.Upsert(server.endpoint, server);
          tr.Commit();
        }
    }

    public void AddMatchInfo(MatchInfo match)
    {
      using (var tr=database.BeginTrans())
      {
        matches.Insert(match);
        tr.Commit();
      }
    }

    public GameServerInfo GetServerInfo(string endpoint)
    {
      return servers.FindOne(x => x.endpoint == endpoint);
    }

    public MatchInfo GetMatchInfo(string endpoint, DateTime timestamp)
    {
      return matches.FindOne(x => x.endpoint == endpoint && x.timestamp == timestamp);
    }

    public IEnumerable<GameServerInfo> GetServers()
    {
      return servers.FindAll();
    }

    public IEnumerable<MatchInfo> GetMatches(string endpoint)
    {
      return matches.Find(x => x.endpoint == endpoint);
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