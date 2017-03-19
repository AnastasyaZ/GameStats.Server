using System;
using System.Collections.Generic;
using Kontur.GameStats.Server.DataModels;

namespace Kontur.GameStats.Server.Database
{
  public interface IDatabaseAdapter : IDisposable
  {
    void UpsertServerInfo(GameServer server);
    void AddMatchInfo(MatchInfo match);

    GameServer GetServerInfo(string endpoint);
    MatchInfo GetMatchInfo(string endpoint, DateTime timestamp);

    IList<GameServer> GetServers();
    IList<MatchInfo> GetMatches(string endpoint);
    IEnumerable<MatchInfo> GetMatches();
  }
}