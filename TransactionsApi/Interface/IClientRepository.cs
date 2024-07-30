namespace TransactionsApi.Interface
{
    public interface IClientRepository
    {
        bool ClientExists(Guid clientId);
    }
}
