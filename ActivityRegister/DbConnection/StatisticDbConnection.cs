namespace ActivityRegister.DbConnection
{
    using ActivityRegister.Migrations;
    using ActivityRegister.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class StatisticDbConnection<T> : DbContext where T:class
    {
        
        public StatisticDbConnection()
            : base("StatisticDbConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StatisticDbConnection<T>, Configuration<T>>());
        }

        public IDbSet<IEntity<T>> Entity { get; set; }
    }

}