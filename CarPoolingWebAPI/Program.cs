using CarPoolingWebAPI.Context;
using CarPoolingWebAPI.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Web.Http;
using System.Web.Http.Cors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
); 
builder.Services.AddDbContext<CarPoolingDbContext>(OptionsBuilder => OptionsBuilder.UseSqlServer("Server=localhost;Database=CarPoolingDataBase;Trusted_Connection=True;Trust Server Certificate=True;"));
//builder.Services.AddDbContext<CarPoolingDbContext>(OptionsBuilder => OptionsBuilder.UseSqlServer("Server=localhost;Database=CarPoolingDataBase;Trusted_Connection=True;Trust Server Certificate=True;MultipleActiveResultSets=true;"));
builder.Services.AddScoped<IOfferedRides, OfferedRides>();
builder.Services.AddScoped<ILoginCredentials, LoginCredentials>();
builder.Services.AddScoped<ICities, Cities>();
builder.Services.AddScoped<IStops, Stops>();
builder.Services.AddScoped<IBookRides, BookRides>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
var app = builder.Build();
app.UseCors(builder =>
{
    builder
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();