namespace NServiceBusConsole;

public class EventTests
{
    record MyEvent(int Id, string Text) : IEvent;
    
    class Subscriber1 : IHandleMessages<MyEvent>
    {
        public Task Handle(MyEvent message, IMessageHandlerContext context)
        {
            Console.WriteLine($"Subscriber1: - received event: {message.Id}: {message.Text}");
            return Task.CompletedTask;
        }
    }
    
    class Subscriber2 : IHandleMessages<MyEvent>
    {
        public Task Handle(MyEvent message, IMessageHandlerContext context)
        {
            Console.WriteLine($"Subscriber2: - received event: {message.Id}: {message.Text}");
            return Task.CompletedTask;
        }
    }
    
    public async Task Execute(IEndpointInstance endpointInstance)
    {
        for (var i = 1; i <= 10; i++)
        {
            var @event = new MyEvent(i, $"Event {i}");

            await endpointInstance.Publish(@event);
        }
    }
}