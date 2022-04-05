using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using MinimalTwitterApi.Models.Content;

namespace MinimalTwitterApi.Models
{
    [Index(nameof(Username), IsUnique = true)]
    public class Player
    {
        [Key]
        [Required]
        public long PlayerId { get; set; }
        
        [StringLength(25)]
        [Required]
        public string Username { get; set; }
        
        [StringLength(50)]
        [Required]
        public string Fullname { get; set; }
        
        [Required]
        public string Password { get; set; }
        
        [Required]
        public string ImageUrl { get; set; }
        
        [StringLength(150)]
        public string Bio { get; set; }

        [Required]
        public long CreatedAt { get; set; }
        
        public List<Tweet> Tweets { get; set; }
        
        public List<Content.Content> Contents { get; set; }
    }

    public class PlayerCreateDto
    {
        [Required]
        [StringLength(25, MinimumLength = 6)]
        public string Username { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Fullname { get; set; }
        
        [Required]
        public string Password { get; set; }
    }

    public class PlayerGetDto
    {
        [Key]
        [Required]
        public long PlayerId { get; set; }
        
        [StringLength(25)]
        [Required]
        public string Username { get; set; }
        
        [StringLength(50)]
        [Required]
        public string Fullname { get; set; }

        [Required]
        public string ImageUrl { get; set; }
        
        [StringLength(150)]
        public string Bio { get; set; }

        [Required]
        public long CreatedAt { get; set; }
    }

    public class PlayerLoginDto
    {
        [Required]
        [StringLength(25, MinimumLength = 6)]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class PlayerLoginResponseDto
    {
        [Required]
        public string Token { get; set; }
        
        [Required]
        public PlayerGetDto Player { get; set; }
    }
}