using System.ComponentModel.DataAnnotations;

namespace NZWalks.API.Models.DTO
{
    public class AddRegionRequestDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Max Length has to be of 3 charactries")]
        [MaxLength(3,ErrorMessage ="Max Length has to be of 3 charactries")]
        public string Code { get; set; }
        [Required]
        [MaxLength(100, ErrorMessage = "Max Length has to be of 100 charactries")]
        public string Name { get; set; }
        public string? RegionImageUrl { get; set; }

    }
}
