using System.Text.Json.Serialization;
using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;
using Microsoft.Extensions.Options;

namespace JmfJwtHelper
{
    public class JwtPayload
    {
        [JsonPropertyName("aud")]
        public string? Audience { get; set; }
    }

    public enum JwtTokenValidationState
    {
        SucessfullyDecoded,
        InvalidToken,
        TokenExpired,
        SignatureValidationFailed,
        WrongAudience,
        UnexpectedError
    }

    public class JwtDecodeResult
    {
        public JwtPayload TokenData { get; set; } = null!;
        public JwtTokenValidationState TokenValidationState { get; set; }
    }

    public class JwtHelper : IJwtHelper
    {
        private readonly JwtOptions jwtOptions;
        private readonly IJwtAlgorithm jwtAlgorithm;

        public JwtHelper(IOptions<JwtOptions> jwtOptions, IJwtAlgorithm jwtAlgorithm)
        {
            this.jwtOptions = jwtOptions.Value;
            this.jwtAlgorithm = jwtAlgorithm;
        }

        public JwtDecodeResult Decode(string token)
        {
            try
            {
                var decodedToken = JwtBuilder.Create()
                .WithAlgorithm(jwtAlgorithm)
                .WithSecret(jwtOptions.Secret)
                .MustVerifySignature()
                .Decode<JwtPayload>(token);

                return new JwtDecodeResult
                {
                    TokenData = decodedToken,
                    TokenValidationState = ValidateTokenClaims(decodedToken)
                };
            }
            catch (TokenExpiredException)
            {
                return new JwtDecodeResult
                {
                    TokenValidationState = JwtTokenValidationState.TokenExpired
                };
            }
            catch (SignatureVerificationException)
            {
                return new JwtDecodeResult
                {
                    TokenValidationState = JwtTokenValidationState.SignatureValidationFailed
                };
            }
            catch (ArgumentOutOfRangeException)
            {
                return new JwtDecodeResult
                {
                    TokenValidationState = JwtTokenValidationState.InvalidToken
                };
            }
            catch (ArgumentNullException)
            {
                return new JwtDecodeResult
                {
                    TokenValidationState = JwtTokenValidationState.InvalidToken
                };
            }
        }

        private JwtTokenValidationState ValidateTokenClaims(JwtPayload payload)
        {
            return payload.Audience == jwtOptions.Audience
                        ? JwtTokenValidationState.SucessfullyDecoded
                        : JwtTokenValidationState.WrongAudience;
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

        //private ES256Algorithm GetEncodingAlgorithm()
        //{
        //    ReadOnlySpan<byte> keyAsSpan = Convert.FromBase64String(JwtOptions[EcdsaPrivateKey]);
        //    var privateKey = ECDsa.Create();
        //    privateKey.ImportECPrivateKey(keyAsSpan, out _);

        //    keyAsSpan = Convert.FromBase64String(JwtOptions[EcdsaPublicKey]);
        //    var publicKey = ECDsa.Create();
        //    publicKey.ImportSubjectPublicKeyInfo(keyAsSpan, out _);

        //    return new ES256Algorithm(publicKey, privateKey);
        //}
    }
}