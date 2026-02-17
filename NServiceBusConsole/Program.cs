using NServiceBusConsole;

var endpointConfiguration = new EndpointConfiguration("Endpoint1");
endpointConfiguration.UseSerialization<SystemJsonSerializer>();
        
var routingSettings = endpointConfiguration.UseTransport(new LearningTransport());
routingSettings.RouteToEndpoint(typeof(MyCommand), "Endpoint1");
        
var endpointInstance = await Endpoint.Start(endpointConfiguration);


await new CommandTests().Execute(endpointInstance);
await new EventTests().Execute(endpointInstance);

Console.WriteLine("Press any key to exit.");
Console.ReadKey();

await endpointInstance.Stop();

