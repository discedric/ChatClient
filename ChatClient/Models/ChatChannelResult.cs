using System.Text.Json.Serialization;

namespace ChatClient.Models
{
	public class ChatChannelResult
	{
		public int Id { get; set; }
		public required string Name { get; set; }
		//als je het in de code iets anders wilt noemen gebruik je deze attribuut
		[JsonPropertyName("CreatedAt")]
		public DateTime CreatedAt { get; set; }
	}
}
