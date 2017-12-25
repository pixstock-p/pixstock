using System;
using System.IO;
using Katalib.Nc.Entity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Pixstock.Nc.Srv.Infra;
using Pixstock.Nc.Srv.Model;

namespace Pixstock.Nc.Srv.Gateway
{
    public class AppDbContext : KatalibDbContext, IAppDbContext
    {
        IApplicationContext context;

        public DbSet<AppMetaInfo> AppMetaInfos { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Content> Contents { get; set; }

        public DbSet<FileMappingInfo> FileMappingInfos { get; set; }

        public DbSet<Label> Labels { get; set; }

        public AppDbContext(IApplicationContext context)
        {
            this.context = context;
        }

        protected IApplicationContext Context { get => context; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databaseFilePath = Path.Combine(this.context.DatabaseDirectoryPath, "pixstock.db");
            optionsBuilder.UseSqlite("Data Source=" + databaseFilePath);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Label2Content>()
                .HasKey(t => new { t.LabelId, t.ContentId });

            modelBuilder.Entity<Label2Content>()
                .HasOne(pt => pt.Content)
                .WithMany(p => p.Labels)
                .HasForeignKey(pt => pt.ContentId);

            modelBuilder.Entity<Label2Content>()
                .HasOne(pt => pt.Label)
                .WithMany(t => t.Contents)
                .HasForeignKey(pt => pt.LabelId);
        }
    }
}