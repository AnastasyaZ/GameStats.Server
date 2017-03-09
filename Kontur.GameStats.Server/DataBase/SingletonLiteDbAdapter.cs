using System;
using System.Collections.Generic;
using Kontur.GameStats.Server.DataModels;

namespace Kontur.GameStats.Server.Database
{
  public class SingletonLiteDbAdapter : IDatabaseAdapter
  {
    /// <summary>
    /// Рекомендация автора по работе в LiteDB.
    /// Так можно потому что она thread-safe и одновременно умеет поддерживать только одно соединение с файлом. 
    /// Создание одного экземпляра позволяет не тратить время на подключение и использовать закешированные данные.
    /// </summary>
    private static readonly LiteDbAdapter Database;

    static SingletonLiteDbAdapter()
    {
      Database = new LiteDbAdapter("MyDatabase.db");//TODO Say No To Hardcode!!
    }

    public void UpsertServerInfo(GameServerInfo server) => Database.UpsertServerInfo(server);

    public void AddMatchInfo(MatchInfo match) => Database.AddMatchInfo(match);

    public GameServerInfo GetServerInfo(string endpoint) => Database.GetServerInfo(endpoint);

    public MatchInfo GetMatchInfo(string endpoint, DateTime timestamp) => Database.GetMatchInfo(endpoint, timestamp);

    public IEnumerable<GameServerInfo> GetServers() => Database.GetServers();

    public IEnumerable<MatchInfo> GetMatches(string endpoint) => Database.GetMatches(endpoint);
    
  }
}