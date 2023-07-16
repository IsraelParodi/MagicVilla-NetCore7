using System;
namespace MagicVilla_API.Models
{
    public class Villa
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreateDate { get; set; }
    }
}

