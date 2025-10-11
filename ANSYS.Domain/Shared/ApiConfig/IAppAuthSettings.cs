namespace ANSYS.Domain.Shared.ApiConfig
{
    public interface IAppAuthSettings
    {
        string Segredo { get; init; }
        int TempoExpiracao { get; init; }
        string Sistema { get; init; }
        string ValidoEm { get; init; }
    }
}
