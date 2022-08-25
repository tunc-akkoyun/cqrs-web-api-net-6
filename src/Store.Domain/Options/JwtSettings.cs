namespace Store.Domain.Options;

public sealed record JwtSettings
{
    public const string GetSectionName = "JwtSettings";

    public string Secret { get; set; }
    public int ExpiryDays { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
}