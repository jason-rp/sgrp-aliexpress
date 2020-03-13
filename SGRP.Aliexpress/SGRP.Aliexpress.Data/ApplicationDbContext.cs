using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using SGRP.Aliexpress.Data.Entities;

namespace SGRP.Aliexpress.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }



        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Product>().ToTable("Product");
        }

    }

    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        private static string _connectionString;

        public ApplicationDbContext CreateDbContext()
        {
            return CreateDbContext(null);
        }

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            //IConfigurationRoot configuration = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile(@Directory.GetCurrentDirectory() + "/../ConsoleApp1/appsettings.json")
            //    .Build();
            //var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            //var connectionString = configuration.GetConnectionString("DatabaseConnection");
            //builder.UseSqlServer(connectionString);
            //return new ApplicationDbContext(builder.Options);
            if (string.IsNullOrEmpty(_connectionString))
            {
                LoadConnectionString();
            }

            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(_connectionString);

            return new ApplicationDbContext(builder.Options);
        }

        private static void LoadConnectionString()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);

            var configuration = builder.Build();

            _connectionString = configuration.GetConnectionString("DatabaseConnection");
        }
    }
}
