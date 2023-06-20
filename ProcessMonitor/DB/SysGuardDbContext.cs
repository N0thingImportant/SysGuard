using System;
using System.Data.Entity;
using System.Data.SQLite;
using System.Data.SQLite.EF6;
using System.IO;

namespace ProcessMonitor.DB
{
    public class SysGuardDbContext : DbContext
    {
        public DbSet<SysFile> SysFiles { get; set; }
        public DbSet<SusArg> SusArgs { get; set; }
        public DbSet<ProcInfo> ProcInfos { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Reserve> Reserved { get; set; }

        private static SQLiteConnection CreateConnection(string path)
        {
            var builder = (SQLiteConnectionStringBuilder)SQLiteProviderFactory.Instance.CreateConnectionStringBuilder();
            if (builder == null)
                return null;

            builder.DataSource = path;
            builder.FailIfMissing = false;
            return new SQLiteConnection(builder.ToString());
        }

        public SysGuardDbContext(string path) : base(CreateConnection(path), false)
        {
            if (!File.Exists(path))
            {
                Notification.Send("DEBUG", $"Database file '{path}' not found");
                var obj = Properties.Resources.ResourceManager.GetObject(path);
                if (obj == null)
                    throw new InvalidOperationException($"Cannot find file or resource '{path}'.");
                Notification.Send("DEBUG", $"Database file '{path}' restored from resources");
                File.WriteAllBytes(path, obj as byte[]);
            }
        }

        public SysGuardDbContext() : this ("SysGuard.db") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SysFile>().ToTable(nameof(SysFiles));
            modelBuilder.Entity<SusArg>().ToTable(nameof(SusArgs));
            modelBuilder.Entity<ProcInfo>().ToTable(nameof(ProcInfos));
            modelBuilder.Entity<Log>().ToTable(nameof(Logs));
            modelBuilder.Entity<Reserve>().ToTable(nameof(Reserved)).HasKey(r => r.Table);
        }
    }
}
