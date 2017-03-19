using System;
using System.Collections.Generic;
using Kontur.GameStats.Server.DataModels;
using Kontur.GameStats.Server.Utility;

namespace Kontur.GameStats.Server.Tests
{
  public static class TestData
  {
    #region server

    public static GameServer[] Servers =
    {
      new GameServer
      {
        endpoint = "kontur.ru-1024",
        info = new ServerInfo
        {
          name = "] My P3rfect GameServer [",
          gameModes = new[] {"DM", "TDM"}
        }
      },
      new GameServer
      {
        endpoint = "192.168.35.38-5454",
        info = new ServerInfo
        {
          name = "Local",
          gameModes = new[] {"MM", "DM"}
        }
      },
      new GameServer
      {
        endpoint = "gg.com-4545",
        info = new ServerInfo
        {
          name = "GG",
          gameModes = new[] {"Ex"}
        }
      }
    };

    public static GameServer Server => Servers[0];

    #endregion

    #region Match

    public static MatchInfo[] Matches =
    {
      new MatchInfo
      {
        endpoint = "kontur.ru-1024",
        timestamp = "2017-01-22T15:17:00Z".ParseInUts(),
        result = new MatchResult
        {
          map = "DM-HelloWorld",
          gameMode = "DM",
          fragLimit = 20,
          timeLimit = 20,
          timeElapsed = 12.345678,
          scoreboard = new[]
          {
            new PlayerInfo
            {
              name = "Player1",
              frags = 20,
              kills = 21,
              deaths = 3
            },
            new PlayerInfo
            {
              name = "Player2",
              frags = 2,
              kills = 2,
              deaths = 21
            }
          }
        }
      },
      new MatchInfo
      {
        endpoint = "kontur.ru-1024",
        timestamp = "2017-01-22T15:17:00Z".ParseInUts(),
        result = new MatchResult
        {
          map = "𠳏𠸏𠻗",
          gameMode = "€ɱ☘✿❣",
          fragLimit = 20,
          timeLimit = 20,
          timeElapsed = 12.345678,
          scoreboard = new[]
          {
            new PlayerInfo
            {
              name = "Player1",
              frags = 20,
              kills = 21,
              deaths = 3
            },
            new PlayerInfo
            {
              name = "☘✿❣",
              frags = 2,
              kills = 2,
              deaths = 20
            },
            new PlayerInfo
            {
              name = "✅",
              frags = 2,
              kills = 2,
              deaths = 19
            },
            new PlayerInfo
            {
              name = "㎍",
              frags = 2,
              kills = 2,
              deaths = 18
            },
            new PlayerInfo
            {
              name = "𝕶",
              frags = 2,
              kills = 2,
              deaths = 17
            }
          }
        }
      },
      new MatchInfo
      {
        endpoint = "kontur.ru-1024",
        timestamp = "2020-10-30T00:00:00Z".ParseInUts(),
        result = new MatchResult
        {
          map = "Fury Road",
          gameMode = "Ex",
          fragLimit = 15,
          timeLimit = 10,
          timeElapsed = 7.4564231,
          scoreboard = new[]
          {
            new PlayerInfo
            {
              name = "Max",
              frags = 14,
              kills = 29,
              deaths = 15
            },
            new PlayerInfo
            {
              name = "Nux",
              frags = 15,
              kills = 29,
              deaths = 14
            }
          }
        }
      },
      new MatchInfo
      {
        endpoint = "kontur.ru-1024",
        timestamp = "1988-03-17T00:00:00Z".ParseInUts(),

        result = new MatchResult
        {
          map = "Desolation",
          gameMode = "DM",
          fragLimit = 5,
          timeLimit = 100,
          timeElapsed = 100.06,
          scoreboard = new[]
          {
            new PlayerInfo
            {
              name = "player1",
              frags = 0,
              kills = 6,
              deaths = 6
            }
          }
        }
      },
      new MatchInfo
      {
        endpoint = "kontur.ru-1024",
        timestamp = "2017-03-17T00:00:00Z".ParseInUts(),

        result = new MatchResult
        {
          map = "Desolation",
          gameMode = "DM",
          fragLimit = 0,
          timeLimit = 0,
          timeElapsed = 0.0,
          scoreboard = new PlayerInfo[0]
        }
      },
      new MatchInfo
      {
        endpoint = "kontur.ru-1024",
        timestamp = "2017-03-17T00:00:00Z".ParseInUts(),

        result = new MatchResult
        {
          map = "Recent",
          gameMode = "DM",
          fragLimit = 5,
          timeLimit = 100,
          timeElapsed = 100.06,
          scoreboard = new[]
          {
            new PlayerInfo
            {
              name = "PLAYER1",
              frags = 20,
              kills = 21,
              deaths = 3
            },
            new PlayerInfo
            {
              name = "Player2",
              frags = 2,
              kills = 2,
              deaths = 21
            }
          }
        }
      },
      new MatchInfo
      {
        endpoint = "kontur.ru-1024",
        timestamp = "2017-02-28T23:59:59Z".ParseInUts(),

        result = new MatchResult
        {
          map = "Desolation",
          gameMode = "Ex",
          fragLimit = 42,
          timeLimit = 42,
          timeElapsed = 0,
          scoreboard = new[]
          {
            new PlayerInfo
            {
              name = "Player7",
              frags = 0,
              kills = 0,
              deaths = 0
            },
            new PlayerInfo
            {
              name = "Player2",
              frags = 0,
              kills = 0,
              deaths = 0
            },
            new PlayerInfo
            {
              name = "Player3",
              frags = 0,
              kills = 0,
              deaths = 0
            },
            new PlayerInfo
            {
              name = "Player4",
              frags = 0,
              kills = 0,
              deaths = 0
            },
            new PlayerInfo
            {
              name = "Player5",
              frags = 0,
              kills = 0,
              deaths = 0
            },
            new PlayerInfo
            {
              name = "Player6",
              frags = 0,
              kills = 0,
              deaths = 0
            },
            new PlayerInfo
            {
              name = "PlaYer1",
              frags = 0,
              kills = 0,
              deaths = 0
            },
            new PlayerInfo
            {
              name = "Player8",
              frags = 0,
              kills = 0,
              deaths = 0
            },
            new PlayerInfo
            {
              name = "Player9",
              frags = 0,
              kills = 0,
              deaths = 0
            },
            new PlayerInfo
            {
              name = "Player10",
              frags = 0,
              kills = 0,
              deaths = 0
            }
          }
        }
      },
      new MatchInfo
      {
        endpoint = "gg.com-4545",
        timestamp = "2017-01-22T15:17:00Z".ParseInUts(),
        result = new MatchResult
        {
          map = "DM-Kitchen",
          gameMode = "DM",
          fragLimit = 20,
          timeLimit = 20,
          timeElapsed = 12.345678,
          scoreboard = new[]
          {
            new PlayerInfo
            {
              name = "Player2",
              frags = 20,
              kills = 21,
              deaths = 3
            },
            new PlayerInfo
            {
              name = "Player1",
              frags = 2,
              kills = 2,
              deaths = 21
            }
          }
        }
      },
      new MatchInfo
      {
        endpoint = "192.168.35.38-5454",
        timestamp = "2048-01-22T15:17:00Z".ParseInUts(),
        result = new MatchResult
        {
          map = "DM-1on1-Rose",
          gameMode = "DM",
          fragLimit = 20,
          timeLimit = 20,
          timeElapsed = 12.345678,
          scoreboard = new[]
          {
            new PlayerInfo
            {
              name = "Player2",
              frags = 20,
              kills = 28,
              deaths = 7
            },
            new PlayerInfo
            {
              name = "player1",
              frags = 2,
              kills = 2,
              deaths = 21
            }
          }
        }
      }
    };

    public static MatchInfo Match => Matches[0];

    #endregion

    #region statistic

    public static Dictionary<string, dynamic> ServerStatistic = new Dictionary<string, dynamic>
    {
        {"totalMatchesPlayed", 7},
        {"maximumMatchesPerDay", 2},
        {"averageMatchesPerDay", 0.00032051282},
        {"maximumPopulation", 10},
        {"averagePopulation", 3.142857},
        {"top5GameModes", new [] {"DM", "Ex", "€ɱ☘✿❣"}},
        {"top5Maps", new [] {"Desolation", "DM-HelloWorld", "Fury Road", "Recent", "𠳏𠸏𠻗"}}
    };

    public static Dictionary<string, dynamic> PlayerStatistic = new Dictionary<string, dynamic>
    {
        {"totalMatchesPlayed", 7 },
        {"totalMatchesWon", 4},
        {"favoriteServer", "kontur.ru-1024"},
        {"uniqueServers", 3},
        {"favoriteGameMode", "DM"},
        {"averageScoreboardPercent", 47.6190476},
        {"maximumMatchesPerDay", 3},
        {"averageMatchesPerDay", 0.00032051282},
        {"lastMatchPlayed", "2048-01-22T15:17:00Z"},
        {"killToDeathRatio", 1.280701754}
    };

    #endregion

    #region reports

    public static IList<DateTime> Timestamps = new[]
    {
      "2048-01-22T15:17:00Z".ParseInUts(),
      "2020-10-30T00:00:00Z".ParseInUts(),
      "2017-03-17T00:00:00Z".ParseInUts(),
      "2017-03-17T00:00:00Z".ParseInUts(),
      "2017-02-28T23:59:59Z".ParseInUts(),
      "2017-01-22T15:17:00Z".ParseInUts(),
      "2017-01-22T15:17:00Z".ParseInUts(),
      "2017-01-22T15:17:00Z".ParseInUts(),
      "1988-03-17T00:00:00Z".ParseInUts()
    };

    public static PlayerInfo[] Scoreboard =
    {
      new PlayerInfo
      {
        name = "player1",
        kills = 7,
        deaths = 3
      },
      new PlayerInfo
      {
        name = "player2",
        kills = 15,
        deaths = 9
      },
      new PlayerInfo
      {
        name = "player3",
        kills = 1,
        deaths = 18
      }
    };

    public static ServerReportInfo[] BestServers =
    {
      new ServerReportInfo
      {
        endpoint = "192.168.35.38-5454",
        name = "Local",
        averageMatchesPerDay = 1
      },
      new ServerReportInfo
      {
        endpoint = "kontur.ru-1024",
        name = "] My P3rfect GameServer [",
        averageMatchesPerDay = 0.00032051282
      },
      new ServerReportInfo
      {
        endpoint = "gg.com-4545",
        name = "GG",
        averageMatchesPerDay = 1.0/11322
      }
    };

    #endregion
  }
}