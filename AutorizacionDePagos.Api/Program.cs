using AutorizacionDePagos.Api.Application.ApprovedAuthorization;
using AutorizacionDePagos.Api.Application.BackgroundServices.ReverseAuthorization;
using AutorizacionDePagos.Api.Application.Services.ApprovedAuthorization;
using AutorizacionDePagos.Api.Application.Services.Authorization;
using AutorizacionDePagos.Api.Application.Services.ExternalClientService;
using AutorizacionDePagos.Api.Application.Services.ProcessorPayment;
using AutorizacionDePagos.Api.Application.Services.ReverseAuthorization;
using AutorizacionDePagos.Api.AutoMapperProfiles;
using AutorizacionDePagos.Api.Domain;
using AutorizacionDePagos.Api.Infrastructure;
using AutorizacionDePagos.Api.Interfaces;
using AutorizacionDePagos.Api.Repositories;
using EasyNetQ;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));

var rabbitConnectionString = builder.Configuration.GetValue<string>("RabbitMqConnectionString");
var bus = RabbitHutch.CreateBus(rabbitConnectionString);

builder.Services.AddSingleton(bus);

builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IHttpClientService, HttpClientService>();

builder.Services.AddScoped<IProcessorPaymentService, ProcessorPaymentService>();

builder.Services.AddAutoMapper(typeof(AutorizacionProfile));
builder.Services.AddAutoMapper(typeof(AutorizacionAprobadaProfile));

builder.Services.AddScoped(typeof(IRepository<Autorizacion>), typeof(Repository<Autorizacion>));
builder.Services.AddScoped(typeof(IRepository<Cliente>), typeof(Repository<Cliente>));
builder.Services.AddScoped(typeof(IRepository<AutorizacionAprobada>), typeof(Repository<AutorizacionAprobada>));
builder.Services.AddScoped<ITipoAutorizacionRepository, TipoAutorizacionRepository>();
builder.Services.AddScoped<IEstadoRepository, EstadoRepository>();

builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<IApprovedAuthorizationService, ApprovedAuthorizationService>();

builder.Services.AddScoped<IAutorizacionRepository, AutorizacionRepository>();

builder.Services.AddScoped<IReverseAuthorizationService, ReverseAuthorizationService>();

builder.Services.AddHostedService<ApprovedAuthorizathionHandler>();
builder.Services.AddHostedService<ReverseAuthorizationHandler>();

var app = builder.Build();

PrepDB.PrepPopulation(app);

app.UseSwagger();
app.UseSwaggerUI();
// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    
//}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
