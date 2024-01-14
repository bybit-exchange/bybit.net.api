using System.Runtime.Serialization;

namespace bybit.net.api;

public enum ProductType
{
    Undefined, // indicates parsing error

    [EnumMember(Value = "spot")]
    Spot,

    [EnumMember(Value = "linear")]
    Linear,

    [EnumMember(Value = "inverse")]
    Inverse,
    
    [EnumMember(Value = "option")]
    Option,
}