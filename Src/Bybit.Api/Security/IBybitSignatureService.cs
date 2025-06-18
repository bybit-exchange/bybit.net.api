namespace bybit.net.api
{
    public interface IBybitSignatureService
    {
        string GeneratePostSignature(IDictionary<string, object> parameters);
        string GenerateGetSignature(IDictionary<string, object> parameters);
    }
}
