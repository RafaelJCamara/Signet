using FastEndpoints;
using MongoDB.Driver;
using Scalar.AspNetCore;
using Signet.Api.Features.Schemas.Extensions;
using Signet.Api.Features.Validation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder
    .AddSchemaUseCases()
    .AddValidationUseCases()
    .AddSchemaInfrastructureExtensions();

builder.Services.AddFastEndpoints();

builder.Services.AddSingleton<IMongoClient>(sp => new MongoClient("mongodb://localhost:27017"));

builder.Services.AddScoped(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("schemaRegistry");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseFastEndpoints();

app.Run();


//TODO: Add backward/forward/full compatibility
//TODO: change REST resources to plural