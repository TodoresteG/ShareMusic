using System.ComponentModel.DataAnnotations;

namespace ShareMusic.Models.Songs
{
    public class AddSongInputModel
    {
        [Required]
        [StringLength(50, ErrorMessage = "{0} have to be between {2} and {1} symbols", MinimumLength = 2)]
        public string Artist { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "{0} have to be between {2} and {1} symbols", MinimumLength = 2)]
        public string Song { get; set; }
    }
}
