using System.Data.Entity;
using System.Data.SQLite;
using System.Data.SQLite.EF6;

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

        public SysGuardDbContext(string path) : base(CreateConnection(path), false) { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SysFile>().ToTable(nameof(SysFiles));
            modelBuilder.Entity<SusArg>().ToTable(nameof(SusArgs));
            modelBuilder.Entity<ProcInfo>().ToTable(nameof(ProcInfos));
            modelBuilder.Entity<Log>().ToTable(nameof(Logs));
            modelBuilder.Entity<Reserve>().ToTable(nameof(Reserved));
        }
    }
}
