namespace EmailAPI.Contracts
{
    public interface IAzureConsumerBus
    {
        Task Start();
        Task Stop();
    }
}
