using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ViajaNet.JobApplication.Application.Core;

namespace ViajaNet.JobApplication.Infrastructure.SqlServer
{
    public class AnalyticsContext : DbContext
    {
        private readonly string _connectionString;

        public DbSet<AnalyticsEntity> Analytics { get; set; }

        public AnalyticsContext(string connectionString)
        {
            if (connectionString == null)
            {
                throw new ArgumentNullException(nameof(connectionString));
            }

            if (connectionString.Length == 0)
            {
                throw new ArgumentException("Connection string can not be empty.", nameof(connectionString));
            }

            this._connectionString = connectionString;

            base.Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entity = modelBuilder.Entity<AnalyticsEntity>();

            entity.ToTable("Analytics").HasIndex(options => options.Id);

            entity.Property(options => options.Id);
            entity.Property(options => options.IP).HasColumnName("Ip").IsRequired();
            entity.Property(options => options.PageName).IsRequired();
            entity.Property(options => options.VendorName).IsRequired();
            entity.Property(options => options.VendorVersion).IsRequired();
            entity.Property(options => options.CreatedAt).HasDefaultValueSql("GETDATE()");

            // Serialize into JSON to simplify things up.
            entity.Property(options => options.Parameters)
                .HasColumnName("Parameters")
                .HasConversion(
                        options => JsonConvert.SerializeObject(options),
                        options => JsonConvert.DeserializeObject<Dictionary<string, List<string>>>(options)
                );

            entity.HasIndex(options => new { options.IP, options.PageName }).HasName("Idx_Ip_PageName");

            entity.HasIndex(options => new { options.IP, options.PageName }).HasName("Unq_Idx_Ip_PageName")
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
