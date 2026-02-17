namespace NServiceBusConsole;

record MyCommand(int Id, string Text) : ICommand;

class MyCommandHandler : IHandleMessages<MyCommand>
{
    public async Task Handle(MyCommand message, IMessageHandlerContext context)
    {
        Console.WriteLine($"Starting processing message: {message.Id}");
        var random = new Random();
        var delay = random.Next(1000, 2000);
        await Task.Delay(delay, context.CancellationToken);
        Console.WriteLine($"Finished processing message: {message.Id}: {message.Text}");
    }
}

class CommandTests
{
    public async Task Execute(IEndpointInstance endpointInstance)
    {
        for (var i = 1; i <= 10; i++)
        {
            var command = new MyCommand(i, $"Message {i}");

            await endpointInstance.Send(command);
        }
    }
}