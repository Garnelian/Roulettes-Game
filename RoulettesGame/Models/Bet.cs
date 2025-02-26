using RoulettesGame.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RoulettesGame.Models
{
    public class Bet
    {
        [Key]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        [Required]
        [MaxLength(250)]
        public string? User { get; set; }
        [Required]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public BetType BetType { get; set; }
        public int? Number { get; set; }
        [RegularExpression(@"^(Red|Black)$", ErrorMessage = "El color debe ser Red o Black.")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ColorBet? ColorBet { get; set; }
        public decimal Amount { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public decimal? TotalEarnings { get; set; }
        [Required]
        public int RoundId { get; set; }
        [ForeignKey("RoundId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Round? Round { get; set; }
        public bool Active { get; set; } = true;
        public DateTime? CreatedAt { get; set; }

        public DateTime? ModificatedAt { get; set; } = DateTime.Now;
    }
}
