using System;
using System.Collections.Generic;
using Kontur.GameStats.Server.DataModels;

namespace Kontur.GameStats.Server.Database
{
  public interface IDatabaseAdapter : IDisposable
  {
    void UpsertServerInfo(GameServerInfo server);
    void AddMatchInfo(MatchInfo match);

    GameServerInfo GetServerInfo(string endpoint);
    MatchInfo GetMatchInfo(string endpoint, DateTime timestamp);

    IList<GameServerInfo> GetServers();
    IList<MatchInfo> GetMatches(string endpoint);
    IList<MatchInfo> GetMatches();
    IList<MatchInfo> GetRecentMatches(int count);
  }
}