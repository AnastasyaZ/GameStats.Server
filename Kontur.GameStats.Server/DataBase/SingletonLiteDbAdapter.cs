using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
      var directory = ConfigurationManager.AppSettings["database_directory"];
      var filename = ConfigurationManager.AppSettings["database_filename"];

      var exists = Directory.Exists(directory);
      if (!exists)
        Directory.CreateDirectory(directory);

      var path = Path.Combine(directory, filename);

      Database = new LiteDbAdapter(path);
    }

    public void UpsertServerInfo(GameServerInfo server) => Database.UpsertServerInfo(server);

    public void AddMatchInfo(MatchInfo match) => Database.AddMatchInfo(match);

    public GameServerInfo GetServerInfo(string endpoint) => Database.GetServerInfo(endpoint);

    public MatchInfo GetMatchInfo(string endpoint, DateTime timestamp) => Database.GetMatchInfo(endpoint, timestamp);

    public IEnumerable<GameServerInfo> GetServers() => Database.GetServers();

    public IEnumerable<MatchInfo> GetMatches(string endpoint) => Database.GetMatches(endpoint);


    #region Dispose

    public void Dispose()
    {
      DisposeManagedResources();
    }

    private void DisposeManagedResources()
    {
      Database.Dispose();
    }

    #endregion
  }
}