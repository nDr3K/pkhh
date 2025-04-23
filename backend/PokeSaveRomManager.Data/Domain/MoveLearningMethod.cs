using System.ComponentModel.DataAnnotations;

namespace PokeSaveRomManager.Data.Domain
{
	public class MoveLearningMethod
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }

		// Navigation properties
		public ICollection<MoveLearning> MoveLearning { get; set; }
	}
}