namespace Database
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DbConnectionClass : DbContext
    {
        public DbConnectionClass()
            : base("name=DbConnectionClass")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<Appointments> Appointments { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<ClientEquipments> ClientEquipments { get; set; }
        public virtual DbSet<Clients> Clients { get; set; }
        public virtual DbSet<ExaminerLogs> ExaminerLogs { get; set; }
        public virtual DbSet<GeoLocations> GeoLocations { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Appointments)
                .WithOptional(e => e.AspNetUsers)
                .HasForeignKey(e => e.ApplicationUser_Id);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Clients)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.ApplicationUserID);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.GeoLocations)
                .WithOptional(e => e.AspNetUsers)
                .HasForeignKey(e => e.ApplicationUser_Id);

            modelBuilder.Entity<Clients>()
                .HasMany(e => e.ClientEquipments)
                .WithOptional(e => e.Clients)
                .HasForeignKey(e => e.Client_Id);

            modelBuilder.Entity<Clients>()
                .HasMany(e => e.GeoLocations)
                .WithOptional(e => e.Clients)
                .HasForeignKey(e => e.Client_Id);
        }
    }
}
