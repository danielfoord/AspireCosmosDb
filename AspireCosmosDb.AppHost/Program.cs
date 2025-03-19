using Microsoft.Extensions.Hosting;
using Projects;

#pragma warning disable ASPIRECOSMOSDB001

var builder = DistributedApplication.CreateBuilder(args);

var cosmos = builder.AddAzureCosmosDB("cosmos-db");
var db = cosmos.AddCosmosDatabase("db");
db.AddContainer("Entry", "/Id");

if (builder.Environment.IsDevelopment())
{
    cosmos.RunAsPreviewEmulator(emu => 
    {
        emu.WithDataVolume();
        emu.WithDataExplorer();
        emu.WithGatewayPort(8081);
    });
}

builder.AddProject<AspireCosmosDb_Api>("web")
       .WithReference(cosmos)
       .WaitFor(cosmos);

builder.Build().Run();
