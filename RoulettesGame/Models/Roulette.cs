using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RoulettesGame.Models
{
    public class Roulette
    {
        [Key]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }
        public string? Description { get; set; }
        public List<Round>? Rounds { get; set; }
        public bool Active { get; set; } = false;
        public DateTime? CreatedAt { get; set; }

        public DateTime ModificatedAt { get; set; } = DateTime.Now;
    }
}
