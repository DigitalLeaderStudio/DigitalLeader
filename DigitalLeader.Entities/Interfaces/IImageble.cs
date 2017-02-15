
namespace DigitalLeader.Entities.Interfaces
{
	public interface IImageble
	{
		int? ImageId { get; set; }

		File Image { get; set; }
	}
}
