using Kontur.GameStats.Server.Database;
using Kontur.GameStats.Server.DataModels;

namespace Kontur.GameStats.Server.RequestHandlers
{
  public class PutDataHandler
  {
    private readonly IDatabaseAdapter database;

    public PutDataHandler(IDatabaseAdapter database)
    {
      this.database = database;
    }

    public void PutServer(GameServer server)
    {
      database.UpsertServerInfo(server);
    }

    public bool TryPutMatch(MatchInfo match)
    {
      if (database.GetServerInfo(match.endpoint) == null) return false;

      database.AddMatchInfo(match);
      return true;
    }
  }
}