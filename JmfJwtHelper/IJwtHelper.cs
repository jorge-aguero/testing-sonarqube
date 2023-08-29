namespace JmfJwtHelper
{
    public interface IJwtHelper
    {
        JwtDecodeResult Decode(string token);
        string Encode(string audience, int? timeoutInSeconds);
    }
}