namespace SecurityHelper.Jwt
{
    public class JwtDecodeResult
    {
        public JwtToken TokenData { get; set; } = null!;
        public JwtTokenValidationState TokenValidationState { get; set; }
    }
}
