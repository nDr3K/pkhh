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
        public DbSet<TypeGame> TypeGames { get; set; }
        public DbSet<Ability> Abilities { get; set; }
        public DbSet<AbilityName> AbilityNames { get; set; }
        public DbSet<AbilityGame> AbilityGames { get; set; }
        public DbSet<Stat> Stats { get; set; }
		public DbSet<Nature> Natures { get; set; }
		public DbSet<Item> Items { get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<Move> Moves { get; set; }
		public DbSet<MoveName> MoveNames { get; set; }
        public DbSet<MoveGame> MoveGames { get; set; }
        public DbSet<MoveLearningMethod> MoveLearningMethods { get; set; }
		public DbSet<MoveLearning> MoveLearning { get; set; }
		public DbSet<Pokemon> Pokemon { get; set; }
		public DbSet<Form> Forms { get; set; }
		public DbSet<PokemonAbility> PokemonAbilities { get; set; }
		public DbSet<PokemonInstance> PokemonInstances { get; set; }
		public DbSet<Team> Teams { get; set; }
		public DbSet<TeamMember> TeamMembers { get; set; }
		public DbSet<Box> Boxes { get; set; }
		public DbSet<BoxSlot> BoxSlots { get; set; }

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

			// Pokemon Type references
			modelBuilder.Entity<Pokemon>()
				.HasOne(p => p.Type1)
				.WithMany()
				.HasForeignKey(p => p.Type1Id)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Pokemon>()
				.HasOne(p => p.Type2)
				.WithMany()
				.HasForeignKey(p => p.Type2Id)
				.OnDelete(DeleteBehavior.Restrict)
				.IsRequired(false); // Type2 can be null

			// Form Type references
			modelBuilder.Entity<Form>()
				.HasOne(f => f.Type1)
				.WithMany()
				.HasForeignKey(f => f.Type1Id)
				.OnDelete(DeleteBehavior.Restrict);

			modelBuilder.Entity<Form>()
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

			base.OnModelCreating(modelBuilder);
		}
	}
}