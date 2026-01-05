using LearnIAEmbeddings.Configuration;
using LearnWithIA.API.UI.Configuration;
using LearnWithIA.Application;
using LearnWithIA.Infrastucture;
using LearnWithIA.Infrastucture.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddCustomOptions(builder.Configuration);
builder.Services.AddCustomHttpClient(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddUseCase();
await builder.Services.AddQDRand(new QDRandDatabase());
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "OpenAPI V1");
    });
    app.UseReDoc(options =>
    {
        options.SpecUrl("/openapi/v1.json");
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
