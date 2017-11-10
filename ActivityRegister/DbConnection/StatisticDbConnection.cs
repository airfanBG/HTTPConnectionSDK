namespace ActivityRegister.DbConnection
{
    using ActivityRegister.Migrations;
    using ActivityRegister.Models;
    using System.Data.Entity;

    public class StatisticDbConnection : DbContext 
    {
        
        public StatisticDbConnection()
            : base("StatisticDbConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StatisticDbConnection, Configuration>());
            
        }

        public IDbSet<EntityStatistic> Entity { get; set; }
    }

}