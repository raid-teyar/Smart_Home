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
    serviceBuilder.AddService<AuthorizationService>();

    serviceBuilder.AddService<HistoryService>();

    serviceBuilder.AddService<DeviceService>();

    serviceBuilder.AddService<DoorService>();

    serviceBuilder.AddService<DoorRequestService>();

    serviceBuilder.AddServiceEndpoint<AuthorizationService, ISecuritySystemAuthorization>(
        new NetTcpBinding(), "net.tcp://localhost:5000/Authorization");

    serviceBuilder.AddServiceEndpoint<HistoryService, ISecuritySystemHistory>(
        new NetTcpBinding(), "net.tcp://localhost:5000/History");

    serviceBuilder.AddServiceEndpoint<DeviceService, ISecuritySystemDevice>(
        new NetTcpBinding(), "net.tcp://localhost:5000/Device");

    serviceBuilder.AddServiceEndpoint<DoorService, ISecuritySystemDoor>(
        new NetTcpBinding(), "net.tcp://localhost:5000/Door");

    serviceBuilder.AddServiceEndpoint<DoorRequestService, ISecuritySystemDoorRequest>(
        new NetTcpBinding(), "net.tcp://localhost:5000/DoorRequest");
});

app.Run();
