using enocaDotNetChallenge.Core.IRepositories;
using enocaDotNetChallenge.Core.IServices;
using enocaDotNetChallenge.Core.IUnitOfWorks;
using enocaDotNetChallenge.Data;
using enocaDotNetChallenge.Data.Repositories;
using enocaDotNetChallenge.Data.UnitOfWorks;
using enocaDotNetChallenge.Filters;
using enocaDotNetChallenge.Middlewares;
using enocaDotNetChallenge.Service.MapProfile;
using enocaDotNetChallenge.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped(typeof(IService<>), typeof(Service<>));
builder.Services.AddScoped<IOrderService,OrderService>();
builder.Services.AddScoped(typeof(NotFoundFilter<>));

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();
