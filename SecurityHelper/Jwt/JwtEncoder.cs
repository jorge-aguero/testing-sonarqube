using JWT.Algorithms;
using JWT.Builder;
using Microsoft.Extensions.Options;
using SecurityHelper.Options;

namespace SecurityHelper.Jwt
{
    public interface IJwtEncoder
    {
        string Encode(string audience, int? timeoutInSeconds);
    }

    public class JwtEncoder : IJwtEncoder
    {
        private readonly JwtOptions jwtOptions;
        private readonly IJwtAlgorithm jwtAlgorithm;

        public JwtEncoder(IOptions<JwtOptions> jwtOptions, IJwtAlgorithm jwtAlgorithm)
        {
            this.jwtOptions = jwtOptions.Value;
            this.jwtAlgorithm = jwtAlgorithm;
        }

        public string Encode(string audience, int? timeoutInSeconds)
            => JwtBuilder.Create()
                .WithAlgorithm(jwtAlgorithm)
                .WithSecret(jwtOptions.Secret)
                .IssuedAt(DateTime.UtcNow)
                .ExpirationTime(DateTime.UtcNow.AddSeconds(timeoutInSeconds ?? jwtOptions.TimeoutInSeconds))
                .Audience(audience)
                .Id(Guid.NewGuid())
                .Encode();
    }
}
