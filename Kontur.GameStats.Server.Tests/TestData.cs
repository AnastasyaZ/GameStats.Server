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
          gameModes = new[] {"SM", "OPM", "DM"}
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
      new MatchInfo()
      {
        endpoint = "kontur.ru-1024",
        timestamp = "2017-01-22T15:17:00Z".ParseInUts(),
        result = new MatchResult()
        {
          map = "DM-HelloWorld",
          gameModel = "DM",
          fragLimit = 20,
          timeLimit = 20,
          timeElapsed = 12.345678,
          scoreboard = new[]
          {
            new PlayerResult()
            {
              name = "Player1",
              frags = 20,
              kills = 21,
              deaths = 3
            },
            new PlayerResult()
            {
              name = "Player2",
              frags = 2,
              kills = 2,
              deaths = 21
            }
          }
        }
      },
      new MatchInfo()
      {
        endpoint = "192.168.35.38-5454",
        timestamp = "2014-10-30T00:00:00.00Z".ParseInUts(),
        result = new MatchResult()
        {
          map = "Fury Road",
          gameModel = "Ex",
          fragLimit = 15,
          timeLimit = 10,
          timeElapsed = 7.4564231,
          scoreboard = new[]
          {
            new PlayerResult()
            {
              name = "Max",
              frags = 14,
              kills = 29,
              deaths = 15
            },
            new PlayerResult()
            {
              name = "Nux",
              frags = 15,
              kills = 29,
              deaths = 14
            }
          }
        }
      },
      new MatchInfo()
      {
        endpoint = "192.168.35.38-9090",
        timestamp = "2014-10-30T00:00:00.00Z".ParseInUts(),
      }
    };

    public static MatchInfo Match => Matches[0];

    #endregion
  }
}