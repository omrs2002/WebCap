using DotNetCore.CAP.Dashboard.NodeDiscovery;
using Savorboard.CAP.InMemoryMessageQueue;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddCap(x =>
{
    x.UseInMemoryStorage();
    x.UseInMemoryMessageQueue();
    x.UseDashboard();
    // Register to Consul
    //x.UseDiscovery(d =>
    //{
    //    d.DiscoveryServerHostName = "localhost";
    //    d.DiscoveryServerPort = 8500;
    //    d.CurrentNodeHostName = "localhost";
    //    d.CurrentNodePort = 5800;
    //    d.NodeId = "1";
    //    d.NodeName = "CAP No.1 Node";
    //});

});




var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => options.SerializeAsV2 = true);
    
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "CAP API");
    });
    //app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
