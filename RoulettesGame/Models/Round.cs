﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RoulettesGame.Models
{
    public class Round
    {
        [Key]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int Id { get; set; }
        [MaxLength(500)]
        public string? Description { get; set; }
        [Required]
        public int RoulletteId { get; set; }

        public int? ResultNumber { get; set; }

        [ForeignKey("RoulletteId")]
        public Roulette Roullette { get; set; }
        public List<Bet>? Bets { get; set; }
        public bool Active { get; set; } = true;
        public DateTime? CreatedAt { get; set; }

        public DateTime ModificatedAt { get; set; } = DateTime.Now;
    }
}
