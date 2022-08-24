namespace Store.Domain.Entities;

public sealed class Parameter
{
    public Parameter(string key, string value)
    {
        Key = key;
        Value = value;
    }

    public string Key { get; private set; }
    public string Value { get; private set; }
}