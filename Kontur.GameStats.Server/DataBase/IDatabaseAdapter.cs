using System;
using System.Collections.Generic;
using Kontur.GameStats.Server.DataModels;

namespace Kontur.GameStats.Server.Database
{
  public interface IDatabaseAdapter
  {
    void UpsertServerInfo(GameServerInfo server);
    void AddMatchInfo(MatchInfo match);

    GameServerInfo GetServerInfo(string endpoint);
    MatchInfo GetMatchInfo(string endpoint, DateTime timestamp);

    IEnumerable<GameServerInfo> GetServers();
    IEnumerable<MatchInfo> GetMatches(string endpoint);
  }
}