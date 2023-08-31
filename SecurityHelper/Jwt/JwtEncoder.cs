﻿using JWT.Algorithms;
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
        private readonly JwtOptions _jwtOptions;
        private readonly IJwtAlgorithm _jwtAlgorithm;

        public JwtEncoder(IOptions<JwtOptions> jwtOptions, IJwtAlgorithm jwtAlgorithm)
        {
            _jwtOptions = jwtOptions.Value;
            _jwtAlgorithm = jwtAlgorithm;
        }

        public string Encode(string audience, int? timeoutInSeconds)
            => JwtBuilder.Create()
                .WithAlgorithm(_jwtAlgorithm)
                .WithSecret(_jwtOptions.Secret)
                .IssuedAt(DateTime.UtcNow)
                .ExpirationTime(DateTime.UtcNow.AddSeconds(timeoutInSeconds ?? _jwtOptions.TimeoutInSeconds))
                .Audience(audience)
                .Id(Guid.NewGuid())
                .Encode();
    }
}
