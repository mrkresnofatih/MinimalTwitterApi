using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalTwitterApi.Models.Content
{
    public class Tweet
    {
        [Key]
        [Required]
        public long TweetId { get; set; }
        
        [ForeignKey("Player")]
        [Required]
        public long PlayerId { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Message { get; set; }
        
        public string ImageUrl { get; set; }
        
        [Required]
        public long CreatedAt { get; set; }
        
        public Player Player { get; set; }
        
        public List<Content> Contents { get; set; }
    }

    public class TweetGetDto
    {
        [Key]
        [Required]
        public long TweetId { get; set; }

        [Required]
        [StringLength(200)]
        public string Message { get; set; }
        
        public string ImageUrl { get; set; }
        
        [Required]
        public long CreatedAt { get; set; }
        
        public PlayerGetDto Player { get; set; }
    }
}