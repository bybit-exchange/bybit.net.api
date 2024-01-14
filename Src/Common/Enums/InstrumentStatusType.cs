namespace bybit.net.api;

public enum InstrumentStatusType
{
    Undefined, // indicates parsing error
    PreLaunch,
    Trading,
    Settling,
    Delivering,
    Closed,
}