using System.Runtime.Serialization;

namespace bybit.net.api;

public enum CopyTradingType
{
    Undefined, // indicates parsing error

    [EnumMember(Value = "none")]
    None,

    [EnumMember(Value = "both")]
    Both,

    [EnumMember(Value = "utaOnly")]
    UfaOnly,

    [EnumMember(Value = "normalOnly")]
    NormalOnly,
}