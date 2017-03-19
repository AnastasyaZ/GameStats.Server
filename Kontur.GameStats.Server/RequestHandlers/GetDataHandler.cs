using System;
using System.Linq;
using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.DataModels;

namespace Kontur.GameStats.Server.RequestHandlers
{
  public class GetDataHandler
  {
    private readonly IDatabaseAdapter database;

    public GetDataHandler(IDatabaseAdapter database)
    {
      this.database = database;
    }

    public ServerInfo GetGameServer(string endpoint)
    {
      return database.GetServerInfo(endpoint)?.info;
    }

    public MatchResult GetMatchResult(string endpoint, DateTime timestamp)
    {
      return database.GetMatchInfo(endpoint, timestamp)?.result;
    }

    public GameServer[] GetGameServers()
    {
      return database.GetServers().ToArray();
    }
  }
}