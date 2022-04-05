using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MinimalTwitterApi.Models.Content
{
    public class Content
    {
        [Key]
        [Required]
        public long ContentId { get; set; }
        
        [ForeignKey("Player")]
        [Required]
        public long PlayerId { get; set; }
        
        [ForeignKey("Tweet")]
        [Required]
        public long TweetId { get; set; }
        
        [Required]
        [StringLength(15)]
        public string ContentType { get; set; }
        
        [StringLength(200)]
        public string Message { get; set; }
        
        public string ImageUrl { get; set; }
        
        [Required]
        public long CreatedAt { get; set; }
        
        public Player Player { get; set; }
        
        public Tweet Tweet { get; set; }
    }

    public class ContentCreateDto
    {
        [Required]
        [StringLength(200)]
        public string Message { get; set; }
        
        public string ImageUrl { get; set; }
    }
    
    public class ContentQueryDto
    {
        [Key]
        [Required]
        public long ContentId { get; set; }

        [Required]
        [StringLength(15)]
        public string ContentType { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Message { get; set; }
        
        [Required]
        public string ImageUrl { get; set; }
        
        [Required]
        public long CreatedAt { get; set; }
        
        public Player Player { get; set; }
        
        public Tweet Tweet { get; set; }
        
        public long LikesCount { get; set; }
        
        public long RepliesCount { get; set; }
    }

    public class ContentGetDto
    {
        [Key]
        [Required]
        public long ContentId { get; set; }

        [Required]
        [StringLength(15)]
        public string ContentType { get; set; }
        
        [Required]
        [StringLength(200)]
        public string Message { get; set; }
        
        [Required]
        public string ImageUrl { get; set; }
        
        [Required]
        public long CreatedAt { get; set; }
        
        public PlayerGetDto Player { get; set; }
        
        public TweetGetDto Tweet { get; set; }
        
        public long LikesCount { get; set; }
        
        public long RepliesCount { get; set; }
    }
}