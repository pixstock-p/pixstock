using System.IO;
using Katalib.Nc.Entity;
using Microsoft.EntityFrameworkCore;
using Pixstock.Nc.Srv.Infra;
using Pixstock.Nc.Srv.Model;

namespace Pixstock.Nc.Srv.Gateway
{
    /// <summary>
    /// サムネイル情報のデータベース
    /// </summary>
    public class ThumbnailDbContext : KatalibDbContext, IThumbnailDbContext
    {
        IApplicationContext context;

        public DbSet<AppMetaInfo> AppMetaInfos { get; set; }

        public DbSet<Thumbnail> Thumbnails { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="context"></param>
        public ThumbnailDbContext(IApplicationContext context)
        {
            this.context = context;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databaseFilePath = Path.Combine(this.context.DatabaseDirectoryPath, "thumb.db");
            optionsBuilder.UseSqlite("Data Source=" + databaseFilePath);
        }
    }
}