using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StorageService.Models
{
    public class ResultHistory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public virtual User User { get; set; }
        [ForeignKey(nameof(User))]
        [StringLength(128)]
        public string UserId { get; set; }
        public double FromLat { get; set; }
        public double FromLong { get; set; }
        public double ToLat { get; set; }
        public double ToLong { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public double DistanceResult { get; set; }
    }
}