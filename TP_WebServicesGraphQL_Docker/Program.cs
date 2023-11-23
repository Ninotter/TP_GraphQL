using Microsoft.EntityFrameworkCore;
using TP_WebServicesGraphQL_Docker.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<ApiContext>(options =>
    //options.UseInMemoryDatabase("MovieDb")
    options.UseMySQL(builder.Configuration.GetConnectionString("mysql"))
    );
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    .AddProjections()
    .AddFiltering()
    .AddSorting();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGraphQL("/graphql");

app.UseHttpsRedirection();

app.Run();