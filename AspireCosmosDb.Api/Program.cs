using AspireCosmosDb.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.AddCosmosDbContext<CosmosDbContext>("cosmos-db", "db");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/entries", async (CosmosDbContext dbContext) =>
{
    return await dbContext.Entries.ToListAsync();
})
.WithName("get_entries")
.WithOpenApi();

app.MapPost("/entries", async (
    CosmosDbContext dbContext, 
    [FromBody] Entry entry, 
    CancellationToken cancellationToken) =>
{
    // Add the entry to the database
    await dbContext.Entries.AddAsync(entry, cancellationToken);
    await dbContext.SaveChangesAsync(cancellationToken);

    // Return a 201 Created response with the entry
    return Results.Created($"/entries/{entry.Id}", entry);
})
.WithName("post_entries")
.WithOpenApi();

app.Run();