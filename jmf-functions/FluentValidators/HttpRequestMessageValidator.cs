using System.Linq;
using System.Net.Http;
using FluentValidation;
using JWT;
using SecurityHelper.Validations;

namespace JmfFunctions.FluentValidators
{
    public class HttpRequestMessageValidator : AbstractValidator<HttpRequestMessage>
    {
        public HttpRequestMessageValidator(IXssValidation xssValidation, IJwtDecoder jwtDecoder)
        {

            ClassLevelCascadeMode = CascadeMode.Stop;
            
            RuleFor(o => o.RequestUri)
                .Cascade(CascadeMode.Stop)
                .Must(uri => xssValidation.IsInputSafe(uri.ToString()))
                .WithMessage(o => $"XssValidation Failed on {nameof(o.RequestUri)}");
            RuleFor(o => o.Headers)
                .Cascade(CascadeMode.Stop)
                .Must(headers => headers.Select(x => x.Value).All(v => xssValidation.IsInputSafe(string.Join(string.Empty, v))))
                .WithMessage(o => $"XssValidation Failed on {nameof(o.Headers)}");

            When(o => o.Method != HttpMethod.Get, () => {
                RuleFor(o => o.Content)
                .Cascade(CascadeMode.Stop)
                .Must(x =>
                {
                    var contenType = string.Empty;
                    if (x?.Headers?.ContentType != null)
                        contenType = x.Headers.ContentType.ToString();

                    return contenType.Split(';').Any(c => c.Trim().ToLowerInvariant() == "application/json");
                })
                .WithMessage(o => $"Content Type missing or not supported")
                .MustAsync(async (x, cancellation) => xssValidation.IsInputSafe(await x.ReadAsStringAsync(cancellation)))
                .WithMessage(o => $"XssValidation Failed on {o.Content}");

                RuleFor(o => o.Headers)
                .Cascade(CascadeMode.Stop)
                .Must(x =>
                {

                    return false;
                });
            });
        }


    }
}
