using Administration.Server.Services;
using SmartHome.Contracts.Contracts;
using CoreWCF;
using CoreWCF.Configuration;

var builder = WebApplication.CreateBuilder();

// Add services to the container.
builder.Services.AddServiceModelServices();

// bind the app to a port
builder.WebHost.UseNetTcp(5001);

var app = builder.Build();


var behavior = new ServiceBehaviorAttribute();
behavior.InstanceContextMode = InstanceContextMode.PerCall;
behavior.ConcurrencyMode = ConcurrencyMode.Single;
behavior.IncludeExceptionDetailInFaults = true; // Set IncludeExceptionDetailInFaults to true

// configure CoreWCF endpoints
app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<AuthorizationService>();

    serviceBuilder.AddService<UserService>();

    serviceBuilder.AddService<DeviceService>();

    serviceBuilder.AddService<HistoryService>();

    serviceBuilder.AddService<RoomService>();

    serviceBuilder.AddServiceEndpoint<AuthorizationService, IAdministrationAuthorization>(
        new NetTcpBinding(), "net.tcp://localhost:5001/Authorization");

    serviceBuilder.AddServiceEndpoint<UserService, IAdministrationUser>(
        new NetTcpBinding(), "net.tcp://localhost:5001/User");

    serviceBuilder.AddServiceEndpoint<DeviceService, IAdministrationDevice>(
        new NetTcpBinding(), "net.tcp://localhost:5001/Device");

    serviceBuilder.AddServiceEndpoint<HistoryService, IAdministrationHistory>(
        new NetTcpBinding(), "net.tcp://localhost:5001/History");

    serviceBuilder.AddServiceEndpoint<RoomService, IAdministrationRoom>(
        new NetTcpBinding(), "net.tcp://localhost:5001/Room");

});

app.Run();
