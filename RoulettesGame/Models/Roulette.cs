using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RoulettesGame.Models
{
    public class Roulette
    {
        [Key]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        public List<Round>? Rounds { get; set; }
        public bool Active { get; set; } = false;
        public DateTime? CreatedAt { get; set; }

        public DateTime ModificatedAt { get; set; } = DateTime.Now;
    }
}
