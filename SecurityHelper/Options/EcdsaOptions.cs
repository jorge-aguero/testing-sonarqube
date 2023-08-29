using System.ComponentModel.DataAnnotations;

namespace SecurityHelper.Options
{
    public class EcdsaOptions
    {
        [Required]
        public string PublicKey { get; set; } = null!;
        [Required]
        public string PrivateKey { get; set; } = null!;
    }
}
