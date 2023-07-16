using System;
using MagicVilla_API.Models.DTO;

namespace MagicVilla_API.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villas = new List<VillaDTO>
        {
            new VillaDTO { Id = 1, Name = "Vista a la playa" },
            new VillaDTO { Id = 2, Name = "Vista al malecon" },
        };
    }
}

