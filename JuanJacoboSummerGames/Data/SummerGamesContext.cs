using JuanJacoboSummerGames.Models;
using Microsoft.EntityFrameworkCore;

namespace JuanJacoboSummerGames.Data
{
    public class SummerGamesContext : DbContext
    {
        //To give access to IHttpContextAccessor for Audit Data with IAuditable
        private readonly IHttpContextAccessor _httpContextAccessor;

        //Property to hold the UserName value
        public string UserName
        {
            get; private set;
        }

        public SummerGamesContext(DbContextOptions<SummerGamesContext> options,
            IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
            if (_httpContextAccessor.HttpContext != null)
            {
                //We have a HttpContext, but there might not be anyone Authenticated
                UserName = _httpContextAccessor.HttpContext?.User.Identity.Name;
                UserName ??= "Unknown";
            }
            else
            {
                //No HttpContext so seeding data
                UserName = "Seed Data";
            }
        }
        //Dependency Injection
        public SummerGamesContext(DbContextOptions<SummerGamesContext> options)  : base(options)
        {
        
        }

        //Creating DbSets
        public DbSet<Athlete> Athletes { get; set;}
        public DbSet<Contingent> Contingents { get; set;}
        public DbSet<Sport> Sports { get; set;}

        //Adding unique constraints and avoiding cascading delete on 1:m relationships
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Add unique index to Contingent Code
            modelBuilder.Entity<Contingent>()
                .HasIndex(c => c.Code)
                .IsUnique();

            //Add unique index to Sport Code
            modelBuilder.Entity<Sport>()
                .HasIndex(c => c.Code) 
                .IsUnique();

            //Add unique index to Athlete Code
            modelBuilder.Entity<Athlete>()
                .HasIndex(a => a.AthleteCode)
                .IsUnique();

            //For contingent
            //Preventing cascade delete of a Contingent with Athletes
            modelBuilder.Entity<Contingent>()
                .HasMany(c => c.Athletes)
                .WithOne(c => c.Contingent)
                .OnDelete(DeleteBehavior.Restrict);
            //For Sport
            //Preventing cascade delete of a Sport with Athletes
            modelBuilder.Entity<Sport>()
                .HasMany(c => c.Athletes)
                .WithOne(c => c.Sport)
                .OnDelete(DeleteBehavior.Restrict);
        }
        //Overrading Save changes methods
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            OnBeforeSaving();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            OnBeforeSaving();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }
        //Tracking who add/update a record and the time the action was done
        private void OnBeforeSaving()
        {
            var entries = ChangeTracker.Entries();
            foreach (var entry in entries)
            {
                if (entry.Entity is IAuditable trackable)
                {
                    var now = DateTime.UtcNow;
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            trackable.UpdatedOn = now;
                            trackable.UpdatedBy = UserName;
                            break;

                        case EntityState.Added:
                            trackable.CreatedOn = now;
                            trackable.CreatedBy = UserName;
                            trackable.UpdatedOn = now;
                            trackable.UpdatedBy = UserName;
                            break;
                    }
                }
            }
        }

    }
}
