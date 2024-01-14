namespace bybit.net.api;

public enum ContractType
{
    Undefined, // indicates parsing error
    InversePerpetual,
    LinearPerpetual,
    LinearFutures,
    InverseFutures,
}