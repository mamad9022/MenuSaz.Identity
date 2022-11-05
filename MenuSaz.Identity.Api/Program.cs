using MenuSaz.Identity.Api.Fillter;
using MenuSaz.Identity.Api.Middleware;
using MenuSaz.Identity.Application;
using MenuSaz.Identity.Infrastructure;
using MenuSaz.Identity.Infrastructure.Extensions;
using MenuSaz.Identity.Persistence;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string _policyName = "CorsPolicy";

builder.Services.AddControllers(options =>
{
    options.Filters.Add<EnvelopFillter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: _policyName, builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

var app = builder.Build();

DbContextExtensions.Seed(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(_policyName);

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseExceptionMiddleware();

app.MapControllers();

app.Run();
