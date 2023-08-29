namespace SecurityHelper.Jwt
{
    public enum JwtTokenValidationState
    {
        SucessfullyDecoded,
        InvalidToken,
        TokenExpired,
        SignatureValidationFailed,
        WrongAudience,
        UnexpectedError
    }
}
