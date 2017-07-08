using WordsTrainerWeb.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;

namespace WordsTrainerWeb.db
{
    public class WordsContext : DbContext
    {

        static WordsContext wordContext;
        static bool isDisposed;
        public WordsContext() : base("WordsContext")
        {
        }

        public DbSet<Word> Words { get; set; }
        public DbSet<UserCl> Users { get; set; }
        public DbSet<UserWord> UserWords { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<PartOfSpeach> Parts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<UserSettings> Settings { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            Database.SetInitializer<WordsContext>(new CreateDatabaseIfNotExists<WordsContext>());
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<UserWord>()
            .HasRequired<Word>(s => s.word)
            .WithMany(s => s.userWords);

            modelBuilder.Entity<Word>()
                .HasMany<UserWord>(s => s.userWords);

            modelBuilder.Entity<UserCl>()
                .HasMany<Language>(s => s.languages)
                .WithMany(u => u.users);

            modelBuilder.Entity<UserCl>()
                .HasMany<UserWord>(s => s.usersWords);

            modelBuilder.Entity<UserCl>().HasRequired<UserSettings>(x => x.settings);


            modelBuilder.Entity<IdentityUserLogin>().HasKey<string>(l => l.UserId);
            modelBuilder.Entity<IdentityRole>().HasKey<string>(r => r.Id);
            modelBuilder.Entity<IdentityUserRole>().HasKey(r => new { r.RoleId, r.UserId });

            base.OnModelCreating(modelBuilder);
        }

        public static WordsContext Create()
        {
            wordContext = new WordsContext();
            isDisposed = false;
            return wordContext;
        }

        public static WordsContext Get()
        {
            if (wordContext == null || isDisposed)
            {
                Create();
                isDisposed = false;
            }
            return wordContext;
        }
        protected override void Dispose(bool disposing)
        {
            isDisposed = true;
            base.Dispose(disposing);
        }
    }
}