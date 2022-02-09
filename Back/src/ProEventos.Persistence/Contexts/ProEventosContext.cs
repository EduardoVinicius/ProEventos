using Microsoft.EntityFrameworkCore;
using ProEventos.Domain;

namespace ProEventos.Persistence.Contexts
{
    public class ProEventosContext : DbContext
    {
        public ProEventosContext(DbContextOptions<ProEventosContext> options) : base(options)
        {

        }

        public DbSet<Batch> Batches { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<SpeakerEvent> SpeakersEvents { get; set; }
        public DbSet<SocialNetwork> SocialNetworks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SpeakerEvent>()
                        .HasKey(se => new {se.EventId, se.SpeakerId});
            
            modelBuilder.Entity<Event>()
                        .HasMany(e => e.SocialNetworks)
                        .WithOne(sn => sn.Event)
                        .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<Speaker>()
                        .HasMany(e => e.SocialNetworks)
                        .WithOne(sn => sn.Speaker)
                        .OnDelete(DeleteBehavior.Cascade);
        }
    }
}