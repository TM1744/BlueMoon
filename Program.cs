using BlueMoon.Context;
using BlueMoon.Repositories;
using BlueMoon.Repositories.Interfaces;
using BlueMoon.Services;
using BlueMoon.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using FluentValidation.AspNetCore;
using BlueMoon.Validations;
using BlueMoon.DTO;

var builder = WebApplication.CreateBuilder(args);

// Configuração do contexto do banco de dados
builder.Services.AddDbContext<MySqlDataBaseContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

// Configuração de repositórios
builder.Services.AddScoped(typeof(IRepositorio<>), typeof(Repositorio<>));
builder.Services.AddScoped<IProdutoRepositorio, ProdutoRepositorio>();
builder.Services.AddScoped<IPessoaRepositorio, PessoaRepositorio>();
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IVendaRepositorio, VendaRepositorio>();

// Configuração de serviços
builder.Services.AddScoped<IProdutoService, ProdutoService>();
builder.Services.AddScoped<IPessoaService, PessoaService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IVendaService, VendaService>();


// Configuração do validadores
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<ProdutoCreateDTOValidator>();
    });

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost5500", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();
app.UseCors("AllowLocalhost5500");
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
