using Victoralm.MAE.API.Context;
using Victoralm.MAE.API.Models;
using Victoralm.MAE.API.UoW.Implementations;
using Victoralm.MAE.API.UoW.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("Total",
        builder => builder.AllowAnyOrigin()
                                        .AllowAnyMethod()
                                        .AllowAnyHeader());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PostgreContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddGraphQLServer().AddQueryType<Query>()
                                   .AddProjections()
                                   .AddFiltering()
                                   .AddSorting();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGraphQL("/graphql");

app.Run();
