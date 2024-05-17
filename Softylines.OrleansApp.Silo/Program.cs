 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Orleans.Configuration;
using System.Net;
using Softylines.OrleansApp.Silo;
using System.Net.Sockets;

try
{
    await StartSilo(args);
    return 0;
}
catch (Exception ex)
{
    Console.WriteLine(ex);
    return 1;
}

static async Task<IHost> StartSilo(string[ ] args)
{
  

    string sqlServerConnectionString = "Server=192.168.30.35;Database=OrleansDb;User Id=sa;Password=Anis2004#;";
     var name = Dns.GetHostName(); // get container id
    var ip = Dns.GetHostEntry(name).AddressList.FirstOrDefault(x => x.AddressFamily == AddressFamily.InterNetwork);

    int siloPort = 11111;
    int gatewayPort = 30000;
    var builder = Host.CreateDefaultBuilder()
        .UseOrleans((context, silo) =>
        {
            silo
                .UseAdoNetClustering(options =>
                {
                    options.Invariant = "System.Data.SqlClient";
                    options.ConnectionString = sqlServerConnectionString;
                })
                .Configure<ClusterOptions>(options =>
                {
                    options.ClusterId = "aminedev";
                    options.ServiceId = "aniss";
                }) 
                .ConfigureEndpoints(siloPort: siloPort, gatewayPort: gatewayPort)

                // .ConfigureEndpoints(default, default,  default, listenOnAnyHostAddress: true)
                .AddAdoNetGrainStorage(
                        name: "GrainStore",
                        configureOptions: options =>
                        {
                            options.ConnectionString = sqlServerConnectionString;
                        }) 
                .UseDashboard(options =>
                {
                    options.Username = "username";
                    options.Password = "password";
                    options.Host = "localhost";
                    options.Port = 9000;
                    options.HostSelf = true;
                    options.CounterUpdateIntervalMs = 1000;
                }) 
                 .ConfigureLogging(logging =>
                {
                    logging
                        .AddConsole();
                }); 
        });

    var app = builder.Build();
    await app.RunAsync();
}

