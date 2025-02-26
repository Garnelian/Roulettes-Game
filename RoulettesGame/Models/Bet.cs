using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
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

        public string? Description { get; set; }
        [Required]
        public string? User { get; set; }
        public int? Number { get; set; }
        [RegularExpression(@"^(Rojo|rojo|negro|Negro)$", ErrorMessage = "El color debe ser rojo o negro.")]
        public string Color { get; set; }
        public decimal Amount { get; set; }
        public decimal? AmountWon { get; set; }
        [Required]
        public int RoundId { get; set; }
        [ForeignKey("RoundId")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Round Round { get; set; }
        public bool Active { get; set; } = true;
        public DateTime? CreatedAt { get; set; }

        public DateTime ModificatedAt { get; set; } = DateTime.Now;
    }
}
