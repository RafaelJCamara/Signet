using FastEndpoints;
using MongoDB.Driver;
using Scalar.AspNetCore;
using Signet.Api.Common.UseCases;
using Signet.Api.Features.Containers.CreateContainer.Endpoint;
using Signet.Api.Features.Containers.CreateContainer.UseCase;
using Signet.Api.Features.Schemas.Endpoints.GetAllSchemas;
using Signet.Api.Features.Schemas.Extensions;
using Signet.Api.Features.Schemas.UseCases.GetAllSchemas;
using Signet.Api.Features.Validation.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<IUseCaseVoid<CreateContainerDto>, CreateContainerUseCase>();

builder.Services.AddScoped<IUseCase<GetAllSchemasEndpointRequestDto, IEnumerable<GetAllSchemasEndpointResponseDto>>, GetAllSchemasUseCase>();

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
//TODO: add fluent validations
//TODO: make connection strings and so on come from config
//TODO: add code documentation
//TODO: add authentication
//TODO: have base route for containers so schemas can derive from
//TODO: add static code analysis and build error enforcing
//TODO: organize program.cs
//TODO: add use case extension to auto register use cases
//TODO: improve read paths, as they don't need all of the overhead of using repositories (do not use repositories in read-only paths)
//TODO: find a better alternative for passing IDs into the create domain objects
