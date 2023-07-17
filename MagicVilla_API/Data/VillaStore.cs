using System;
using MagicVilla_API.Models.DTO;

namespace MagicVilla_API.Data
{
    public static class VillaStore
    {
        public static List<VillaDTO> villas = new List<VillaDTO>
        {
            new VillaDTO { Id = 1, Name = "Vista a la playa", Occupants = 10, SquareMeter = 100 },
            new VillaDTO { Id = 2, Name = "Vista al malecon", Occupants = 3, SquareMeter = 50 },
        };
    }
}

