using System.ComponentModel.DataAnnotations;

namespace SecurityHelper.Options
{
    public class JwtOptions
    {
        [Required]
        public string Secret { get; set; } = null!;
        public string Audience { get; set; } = null!;
        [Range(0, 30000)]
        public int TimeoutInSeconds { get; set; } = 60;
    }
}
