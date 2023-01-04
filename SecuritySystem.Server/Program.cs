using SecuritySystem.Server.Services;
using SmartHome.Contracts.Contracts;

var builder = WebApplication.CreateBuilder();

// Add services to the container.
builder.Services.AddServiceModelServices();

// bind the app to a port
builder.WebHost.UseNetTcp(5000);

var app = builder.Build();

// configure CoreWCF endpoints
app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<AuthorizationService>((options) =>
    {
        options.DebugBehavior.IncludeExceptionDetailInFaults = true;
    });

    serviceBuilder.AddService<HistoryService>((options) =>
    {
        options.DebugBehavior.IncludeExceptionDetailInFaults = true;
    });

    serviceBuilder.AddService<DeviceService>((options) =>
    {
        options.DebugBehavior.IncludeExceptionDetailInFaults = true;
    });

    serviceBuilder.AddService<DoorService>((options) =>
    {
        options.DebugBehavior.IncludeExceptionDetailInFaults = true;
    });

    serviceBuilder.AddServiceEndpoint<AuthorizationService, ISecuritySystemAuthorization>(
        new NetTcpBinding(), "net.tcp://localhost:5000/Authorization");

    serviceBuilder.AddServiceEndpoint<HistoryService, ISecuritySystemHistory>(
        new NetTcpBinding(), "net.tcp://localhost:5000/History");

    serviceBuilder.AddServiceEndpoint<DeviceService, ISecuritySystemDevice>(
        new NetTcpBinding(), "net.tcp://localhost:5000/Device");

    serviceBuilder.AddServiceEndpoint<DoorService, ISecuritySystemDoor>(
        new NetTcpBinding(), "net.tcp://localhost:5000/Door");
});

app.Run();
