using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApiTienda;
using WebApiTienda.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<PurchaseContext>( options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

//Mapper Conf
var mapperConfig = new MapperConfiguration(m =>
{
    m.AddProfile(new ProductMappingProfile());
});

IMapper mapper = mapperConfig.CreateMapper();

builder.Services.AddSingleton(mapper);
builder.Services.AddMvc();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder => {
    builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.UseAuthorization();

app.MapControllers();

app.Run();
