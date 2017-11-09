namespace ActivityRegister.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ActivityRegister.DbConnection.StatisticDbConnection>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ActivityRegister.DbConnection.StatisticDbConnection context)
        {
            //context.Statistics.Add(new Models.Statistic() { ComputerName = "sada", DateOfRequest = DateTime.Now, MachineId = "dsaaa", RequestModel = "dga", RequestType = "dasda", UserName = "dasda" });
            //context.SaveChanges();
        }
    }
}
