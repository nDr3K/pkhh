using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Domain
{
	public class PokemonInstance
	{
		[Key]
		public int Id { get; set; }
		public int SaveId { get; set; }
		public int PokemonId { get; set; }
		public int FormId { get; set; }
		public string Nickname { get; set; }
		public GenderType Gender { get; set; }
		public int Level { get; set; }
		public bool Shiny { get; set; } = false;
		public int? NatureId { get; set; }
		public int? HeldItemId { get; set; }
		public int? AbilityId { get; set; }
		public int? Move1Id { get; set; }
		public int? Move2Id { get; set; }
		public int? Move3Id { get; set; }
		public int? Move4Id { get; set; }
		public int? IVHP { get; set; }
		public int? IVAttack { get; set; }
		public int? IVDefense { get; set; }
		public int? IVSpecial { get; set; }
        public int? IVSpAttack { get; set; }
		public int? IVSpDefense { get; set; }
		public int? IVSpeed { get; set; }
		public int? EVHP { get; set; }
		public int? EVAttack { get; set; }
		public int? EVDefense { get; set; }
        public int? EVSpecial { get; set; }
        public int? EVSpAttack { get; set; }
		public int? EVSpDefense { get; set; }
		public int? EVSpeed { get; set; }

        // Navigation properties
        [ForeignKey("SaveId")]
        public Save Save { get; set; }
		[ForeignKey("PokemonId")]
		public Pokemon Pokemon { get; set; }
		[ForeignKey("FormId")]
		public PokemonForm Form { get; set; }
		[ForeignKey("NatureId")]
		public Nature Nature { get; set; }
		[ForeignKey("HeldItemId")]
		public Item HeldItem { get; set; }
		[ForeignKey("AbilityId")]
		public Ability Ability { get; set; }
		[ForeignKey("Move1Id")]
		public Move Move1 { get; set; }
		[ForeignKey("Move2Id")]
		public Move Move2 { get; set; }
		[ForeignKey("Move3Id")]
		public Move Move3 { get; set; }
		[ForeignKey("Move4Id")]
		public Move Move4 { get; set; }
		public ICollection<SaveTeamMember> TeamMembers { get; set; }
		public ICollection<SaveBoxSlot> BoxSlots { get; set; }
	}
}