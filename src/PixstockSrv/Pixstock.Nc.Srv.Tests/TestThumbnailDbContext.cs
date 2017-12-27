using System;
using System.IO;
using Katalib.Nc.Entity;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Pixstock.Nc.Srv.Gateway;
using Pixstock.Nc.Srv.Infra;
using Pixstock.Nc.Srv.Model;

namespace Pixstock.Nc.Srv.Tests
{
    /// <summary>
    /// 単体試験用のデータベースコンテキスト
    /// </summary>
    /// <remarks>
    /// このコンテキストを使用すると、データ格納先がIn-Memoryのデータベースとなる。
    /// 格納したデータの有効期限は、インスタンスがDisposeされるまでとなります。
    /// </remarks>
    public class TestThumbnailDbContext : ThumbnailDbContext
    {
        public TestThumbnailDbContext(IApplicationContext context) : base(context)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);
            optionsBuilder.UseSqlite(connection);
        }
    }
}