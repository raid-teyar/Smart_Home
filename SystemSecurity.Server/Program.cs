using SecuritySystem.Server.Services;
using SmartHome.Contracts.Contracts;

var builder = WebApplication.CreateBuilder();

// register the contract implementation
builder.Services.AddTransient<Authorization>();

// Add services to the container.
builder.Services.AddServiceModelServices();

// bind the app to a port
builder.WebHost.UseNetTcp(5000);

var app = builder.Build();

// configure CoreWCF endpoints
app.UseServiceModel(serviceBuilder =>
{
    serviceBuilder.AddService<Authorization>();

    serviceBuilder.AddServiceEndpoint<Authorization, ISecurityServiceAuthorization>(
        new NetTcpBinding(), "net.tcp://localhost:5000/Authorization");
});

app.Run();
