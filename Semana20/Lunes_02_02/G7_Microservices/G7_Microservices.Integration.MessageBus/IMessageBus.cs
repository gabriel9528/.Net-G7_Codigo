namespace G7_Microservices.Integration.MessageBus
{
    public interface IMessageBus
    {
        Task PublishMessage(object message, string topic_queue_name);
    }
}
