using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalTwitterApi.Models
{
    public class Connection
    {
        [Key]
        [Required]
        public long ConnectionId { get; set; }
        
        [ForeignKey("Player")]
        [Required]
        public long PlayerId { get; set; }
        
        [Required]
        public long FollowerId { get; set; }
        
        [StringLength(20)]
        [Required]
        public string ConnectionType { get; set; }
        
        public Player Player { get; set; }
        
        [Required]
        public long CreatedAt { get; set; }
    }

    public class ConnectionGetProfileDto
    {
        [Key]
        [Required]
        public long ConnectionId { get; set; }
        
        [ForeignKey("Player")]
        [Required]
        public long PlayerId { get; set; }
        
        [Required]
        public long FollowerId { get; set; }
        
        [StringLength(20)]
        [Required]
        public string ConnectionType { get; set; }
        
        public PlayerGetDto Player { get; set; }
        
        [Required]
        public long CreatedAt { get; set; }
    }
    
    public class ConnectionGetDto
    {
        [Key]
        [Required]
        public long ConnectionId { get; set; }
        
        [ForeignKey("Player")]
        [Required]
        public long PlayerId { get; set; }
        
        [Required]
        public long FollowerId { get; set; }
        
        [StringLength(20)]
        [Required]
        public string ConnectionType { get; set; }

        [Required]
        public long CreatedAt { get; set; }
    }
}