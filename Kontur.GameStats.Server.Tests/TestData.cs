using System.Collections.Generic;
using Kontur.GameStats.Server.DataModels;
using Kontur.GameStats.Server.DataModels.Utility;

namespace Kontur.GameStats.Server.Tests
{
  public static class TestData
  {
    #region server

    public static GameServerInfo[] Servers =
    {
      new GameServerInfo
      {
        endpoint = "kontur.ru-1024",
        gameServer = new GameServer
        {
          name = "] My P3rfect GameServer [",
          gameModes = new[] {"DM", "TDM"}
        }
      },
      new GameServerInfo
      {
        endpoint = "192.168.35.38-5454",
        gameServer = new GameServer
        {
          name = "LocalServer",
          gameModes = new[] {"MM", "DM"}
        }
      },
      new GameServerInfo
      {
        endpoint = "geeks-games.com-2048",
        gameServer = new GameServer
        {
          name = "GG",
          gameModes = new[] {"Ex"}
        }
      }
    };

    public static GameServerInfo Server => Servers[0];

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
            new PlayerResult
            {
              name = "Player1",
              frags = 20,
              kills = 21,
              deaths = 3
            },
            new PlayerResult
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
            new PlayerResult
            {
              name = "Player1",
              frags = 20,
              kills = 21,
              deaths = 3
            },
            new PlayerResult
            {
              name = "☘✿❣",
              frags = 2,
              kills = 2,
              deaths = 21
            },
            new PlayerResult
            {
              name = "✅",
              frags = 2,
              kills = 2,
              deaths = 21
            },
            new PlayerResult
            {
              name = "㎍",
              frags = 2,
              kills = 2,
              deaths = 21
            },
            new PlayerResult
            {
              name = "𝕶",
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
            new PlayerResult
            {
              name = "Max",
              frags = 14,
              kills = 29,
              deaths = 15
            },
            new PlayerResult
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
            new PlayerResult
            {
              name = "Forever alone",
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
          scoreboard = new PlayerResult[0]
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
            new PlayerResult
            {
              name = "PLAYER1",
              frags = 20,
              kills = 21,
              deaths = 3
            },
            new PlayerResult
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
            new PlayerResult
            {
              name = "PlaYer1",
              frags = 0,
              kills = 0,
              deaths = 0
            },
            new PlayerResult
            {
              name = "Player2",
              frags = 0,
              kills = 0,
              deaths = 0
            },
            new PlayerResult
            {
              name = "Player3",
              frags = 0,
              kills = 0,
              deaths = 0
            },
            new PlayerResult
            {
              name = "Player4",
              frags = 0,
              kills = 0,
              deaths = 0
            },
            new PlayerResult
            {
              name = "Player5",
              frags = 0,
              kills = 0,
              deaths = 0
            },
            new PlayerResult
            {
              name = "Player6",
              frags = 0,
              kills = 0,
              deaths = 0
            },
            new PlayerResult
            {
              name = "Player7",
              frags = 0,
              kills = 0,
              deaths = 0
            },
            new PlayerResult
            {
              name = "Player8",
              frags = 0,
              kills = 0,
              deaths = 0
            },
            new PlayerResult
            {
              name = "Player9",
              frags = 0,
              kills = 0,
              deaths = 0
            },
            new PlayerResult
            {
              name = "Player10",
              frags = 0,
              kills = 0,
              deaths = 0
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
        {"averageMatchesPerDay", 0.0005874454},
        {"maximumPopulation", 10},
        {"averagePopulation", 3.142857},
        {"top5GameModes", new [] {"DM", "Ex", "€ɱ☘✿❣"}},
        {"top5Maps", new [] {"Desolation", "DM-HelloWorld", "Fury Road", "Recent", "𠳏𠸏𠻗"}}
    };

    #endregion
  }
}