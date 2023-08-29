using JWT.Algorithms;
using JWT.Builder;
using JWT.Exceptions;
using Microsoft.Extensions.Options;
using SecurityHelper.Options;

namespace SecurityHelper.Jwt
{
    public interface IJwtDecoder
    {
        JwtDecodeResult Decode(string token);
    }

    public class JwtDecoder : IJwtDecoder
    {
        private readonly JwtOptions jwtOptions;
        private readonly IJwtAlgorithm jwtAlgorithm;

        public JwtDecoder(IOptions<JwtOptions> jwtOptions, IJwtAlgorithm jwtAlgorithm)
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
                .Decode<JwtToken>(token);

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

        private JwtTokenValidationState ValidateTokenClaims(JwtToken payload)
        {
            return payload.Audience == jwtOptions.Audience
                        ? JwtTokenValidationState.SucessfullyDecoded
                        : JwtTokenValidationState.WrongAudience;
        }

    }
}
