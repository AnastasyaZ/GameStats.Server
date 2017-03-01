using LiteDB;

namespace Kontur.GameStats.Server.DataBase
{
    public class LiteDbFacade : IDatabaseFacade
    {
        public static LiteDatabase Database;

        static LiteDbFacade()
        {
            Database = new LiteDatabase("MyDatabase.db");//TODO Say No To Hardcode!!
        }
    }
}