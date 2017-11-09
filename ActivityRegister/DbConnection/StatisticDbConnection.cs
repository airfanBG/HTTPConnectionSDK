namespace ActivityRegister.DbConnection
{
    using ActivityRegister.Migrations;
    using ActivityRegister.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class StatisticDbConnection : DbContext
    {
        
        public StatisticDbConnection()
            : base("StatisticDbConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StatisticDbConnection, Configuration>());
        }

        public IDbSet<Statistic> Statistics { get; set; }
    }

}