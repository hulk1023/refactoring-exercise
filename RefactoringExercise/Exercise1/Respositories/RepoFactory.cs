using System.Collections.Concurrent;
using sg.com.titansoft.TiDALBase.Entity;
using sg.com.titansoft.TiDALBase.Factory;

namespace com.sbobet.sports.Respositories
{
    public class RepoFactory
    {
        public static void InitDbContext(string connectionString, string project)
        {
            var dbContexts = new ConcurrentDictionary<string, TiDbContext>();
            dbContexts[DbRole.ApplicationSettingDb] = new TiDbContext(connectionString)
            {
                DbRole = DbRole.ApplicationSettingDb
            };
            TiDbContextFactory.InitDbConfigDictionary(dbContexts);
            TiDbContextFactory.LoadDbConfig(dbContexts[DbRole.ApplicationSettingDb], project);
            TiDbContextFactory.GetDbConfig(DbRole.ApplicationSettingDb);
        }

        private static readonly string _SportBookDbName  = "SportsbookDB";

        private static Repo _sportsBookDb;
        public static Repo SportsBookDb
        {
            get => _sportsBookDb ?? (_sportsBookDb =
                       new Repo(TiDbContextFactory.GetDbConfig(_SportBookDbName), nameof(SportsBookDb)));
            protected set => _sportsBookDb = value;
        }

    }
}
