using Microsoft.EntityFrameworkCore;
using PokeSaveRomManager.Data.Domain;
using Type = PokeSaveRomManager.Data.Domain.Type;

namespace PokeSaveRomManager.Data
{
	public class PokemonDbContext: DbContext
	{
		public PokemonDbContext(DbContextOptions<PokemonDbContext> options) : base(options) 
		{ 
		}

		public DbSet<User> Users { get; set; }
		public DbSet<Game> Games { get; set; }
		public DbSet<Type> Types { get; set; }
        public DbSet<GameType> GameTypes { get; set; }
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<AbilityName> AbilityNames { get; set; }
        public DbSet<GameAbility> GameAbilities { get; set; }
        public DbSet<Stat> Stats { get; set; }
		public DbSet<Nature> Natures { get; set; }
		public DbSet<Item> Items { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Move> Moves { get; set; }
		public DbSet<MoveName> MoveNames { get; set; }
        public DbSet<GameMove> GameMoves { get; set; }
        public DbSet<MoveLearningMethod> MoveLearningMethods { get; set; }
		public DbSet<PokemonMove> PokemonMove { get; set; }
		public DbSet<Pokemon> Pokemon { get; set; }
		public DbSet<PokemonForm> PokemonForms { get; set; }
		public DbSet<PokemonAbility> PokemonAbilities { get; set; }
		public DbSet<Save> Saves { get; set; }
        public DbSet<PokemonInstance> PokemonInstances { get; set; }
		public DbSet<SaveTeam> SaveTeams { get; set; }
		public DbSet<SaveTeamMember> SaveTeamMembers { get; set; }
		public DbSet<SaveBox> SaveBoxes { get; set; }
		public DbSet<SaveBoxSlot> SaveBoxSlots { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			// Configure relationships and constraints
            // Nature references to Stats
			modelBuilder.Entity<Nature>()
				.HasOne(n => n.IncreasedStat)
				.WithMany()
				.HasForeignKey(n => n.IncreasedStatId)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Nature>()
				.HasOne(n => n.DecreasedStat)
				.WithMany()
				.HasForeignKey(n => n.DecreasedStatId)
				.OnDelete(DeleteBehavior.Restrict);

			// Form Type references
			modelBuilder.Entity<PokemonForm>()
				.HasOne(f => f.Type1)
				.WithMany()
				.HasForeignKey(f => f.Type1Id)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<PokemonForm>()
				.HasOne(f => f.Type2)
				.WithMany()
				.HasForeignKey(f => f.Type2Id)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(false); // Type2 can be null

			// Pokemon Instance move references
			modelBuilder.Entity<PokemonInstance>()
				.HasOne(pi => pi.Move1)
				.WithMany()
				.HasForeignKey(pi => pi.Move1Id)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(false);

			modelBuilder.Entity<PokemonInstance>()
				.HasOne(pi => pi.Move2)
				.WithMany()
				.HasForeignKey(pi => pi.Move2Id)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(false);

			modelBuilder.Entity<PokemonInstance>()
				.HasOne(pi => pi.Move3)
				.WithMany()
				.HasForeignKey(pi => pi.Move3Id)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(false);

			modelBuilder.Entity<PokemonInstance>()
				.HasOne(pi => pi.Move4)
				.WithMany()
				.HasForeignKey(pi => pi.Move4Id)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(false);

			// Check constraint for gender
			modelBuilder.Entity<PokemonInstance>()
				.Property(pi => pi.Gender)
				.HasMaxLength(10)
				.HasConversion<string>();

            modelBuilder.Entity<AbilityName>()
				.HasMany(an => an.Abilities)
				.WithOne(a => a.Name)
				.HasForeignKey(a => a.NameId)
				.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<MoveName>()
                .HasMany(mn => mn.Moves)
                .WithOne(m => m.Name)
                .HasForeignKey(m => m.NameId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
				.HasIndex(u => u.Auth0Id)
				.IsUnique();

            // Cascade delete for related entities of Save
            modelBuilder.Entity<Save>()
			   .Property(s => s.Badges)
			   .HasConversion<byte>();
            modelBuilder.Entity<Save>()
                .HasMany(s => s.Boxes)
                .WithOne(b => b.Save)
                .HasForeignKey(b => b.SaveId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Save>()
                .HasOne(s => s.Party)
                .WithOne(p => p.Save)
                .HasForeignKey<SaveTeam>(p => p.SaveId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Save>()
                .HasMany(s => s.PokemonInstances)
                .WithOne(pi => pi.Save)
                .HasForeignKey(pi => pi.SaveId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SaveBox>()
                .HasMany(sb => sb.Slots)
                .WithOne(s => s.Box)
                .HasForeignKey(s => s.BoxId)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<SaveTeam>()
                .HasMany(st => st.Members)
                .WithOne(m => m.Party)
                .HasForeignKey(m => m.PartyId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
		}
	}
}