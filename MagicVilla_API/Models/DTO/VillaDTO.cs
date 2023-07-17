using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace MagicVilla_API.Models.DTO
{
    public class VillaDTO
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        [JsonProperty(PropertyName = "name")]
        public required string Name { get; set; }

        public int Occupants { get; set; }
        public int SquareMeter { get; set; }
    }
}

