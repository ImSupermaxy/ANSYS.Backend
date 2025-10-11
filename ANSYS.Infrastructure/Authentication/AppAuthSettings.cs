using ANSYS.Domain.Shared.ApiConfig;

namespace ANSYS.Infrastructure.Authentication
{
    public sealed class AppAuthSettings : IAppAuthSettings
    {
        public string Segredo { get; init; }
        public int TempoExpiracao { get; init; }
        public string Sistema { get; init; }
        public string ValidoEm { get; init; }
    }
}
