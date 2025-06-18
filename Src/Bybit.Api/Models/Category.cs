namespace Bybit.Api.Models;

public struct Category
{
    private Category(string value)
    {
        Value = value;
    }

    public static Category SPOT { get => new("spot"); }
    public static Category LINEAR { get => new("linear"); }
    public static Category INVERSE { get => new("inverse"); }
    public static Category OPTION { get => new("option"); }
    public string Value { get; private set; }
    public static implicit operator string(Category enm) => enm.Value;
    public override readonly string ToString() => Value.ToString();
}