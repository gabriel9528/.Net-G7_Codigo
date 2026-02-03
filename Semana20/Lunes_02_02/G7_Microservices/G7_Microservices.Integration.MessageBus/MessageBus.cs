using Azure.Messaging.ServiceBus;
using Newtonsoft.Json;
using System.Text;

namespace G7_Microservices.Integration.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private string connectionString = "";

        public async Task PublishMessage(object message, string topic_queue_name)
        {
            await using var client = new ServiceBusClient(connectionString);

            ServiceBusSender busSender = client.CreateSender(topic_queue_name);
            var jsonMessage = JsonConvert.SerializeObject(message);

            ServiceBusMessage finalMessage = new ServiceBusMessage(Encoding.UTF8.GetBytes(jsonMessage))
            {
                CorrelationId = Guid.NewGuid().ToString(),
            };

            await busSender.SendMessageAsync(finalMessage);

            await client.DisposeAsync();
        }
    }
}
