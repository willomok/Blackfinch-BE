using Microsoft.EntityFrameworkCore;
using LendingPlatform.Models;
using System;

namespace LendingPlatform.Data
{
    public class LoanDbContext : DbContext
    {
        public DbSet<LoanApplication> LoanApplications { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            DotNetEnv.Env.Load();

            string connectionString = Environment.GetEnvironmentVariable("DATABASE_CONNECTION_STRING") 
                                    ?? throw new Exception("DATABASE_CONNECTION_STRING is not set or empty.");

            optionsBuilder.UseMySql(
                connectionString,
                new MySqlServerVersion(new Version(8, 0, 2))
            );
        }

    }
}
